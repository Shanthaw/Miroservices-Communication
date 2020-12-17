using AutoMapper;
using GloboTicket.Integration.MessagingBus;
using GloboTicket.Services.EventCatalog.Entities;
using GloboTicket.Services.EventCatalog.Messages;
using GloboTicket.Services.EventCatalog.Models;
using GloboTicket.Services.EventCatalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloboTicket.Services.EventCatalog.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        //private readonly IEventRepository eventRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IMessageBus messageBus;

        public EventController(IUnitOfWork unitOfWork, IMapper mapper, IMessageBus messageBus)
        {
            //this.eventRepository = eventRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.messageBus = messageBus;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.EventDto>>> Get(
            [FromQuery] Guid categoryId)
        {
            var result = await unitOfWork.EventRepository.GetEvents(categoryId);
            return Ok(mapper.Map<List<Models.EventDto>>(result));
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult<Models.EventDto>> GetById(Guid eventId)
        {
            var result = await unitOfWork.EventRepository.GetEventById(eventId);
            return Ok(mapper.Map<Models.EventDto>(result));
        }

        [HttpPost("eventupdate")]
        public async Task<ActionResult<EventUpdate>> Post(EventUpdate eventUpdate)
        {
            var eventToUpdate = await unitOfWork.EventRepository.GetEventById(eventUpdate.EventId);

            eventToUpdate.Name = eventUpdate.Name;
            eventToUpdate.Price = eventUpdate.Price;
            eventToUpdate.Date = eventUpdate.Date;
            // message property is only for the message to other microservices

            // structure message to be sent to service bus
            EventUpdatedMessage eventUpdatedMessage = new EventUpdatedMessage
            {
                Id = Guid.NewGuid(),
                CreationDateTime = DateTime.Now,
                EventId = eventUpdate.EventId,
                Name = eventUpdate.Name,
                Date = eventUpdate.Date,
                Price = eventUpdate.Price,
                Message = eventUpdate.Message
            };

            // serialize message for storage in database table text field
            var jsonMessage = JsonConvert.SerializeObject(eventUpdatedMessage);

            IntegrationEventLog logEntry = new IntegrationEventLog
            {
                IntegrationEventType = "TicketedEventChange",
                ServiceBusTopicName = "eventupdatedmessage",
                IntegrationEventBody = jsonMessage,
                State = "Pending"
            };

            unitOfWork.IntegrationEventLogRepository.AddEventLogEntry(logEntry);

            try
            {
                unitOfWork.Commit();

                // await messageBus.PublishMessage(eventUpdatedMessage, "eventupdatedmessage");
            }
            catch (Exception e)
            {
                unitOfWork.Rollback();

                Console.WriteLine(e);
                throw;
            }

            return Ok(eventUpdate);
        }

    }
}
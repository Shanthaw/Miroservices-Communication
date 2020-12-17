using GloboTicket.Web.Models;
using GloboTicket.Web.Models.Api;
using GloboTicket.Web.Models.View;
using GloboTicket.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace GloboTicket.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly Settings settings;

        public OrderController(Settings settings, IOrderService orderService)
        {
            this.settings = settings;
            this.orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            List<Order> orders = await orderService.GetOrdersForUser(settings.UserId);
            var numberOfMessages = (from order in orders
                            where order.Message != null
                            select order).Count();

            bool hasMessages = numberOfMessages > 0;

            var vm = new OrderListViewModel
            {
                HasMessage = hasMessages,
                Orders = orders
            };

            return View(vm);
        }

        public async Task<IActionResult> Detail(Guid orderId)
        {
            var ev = await orderService.GetOrderDetails(orderId);
            return View(ev);
        }

    }
}

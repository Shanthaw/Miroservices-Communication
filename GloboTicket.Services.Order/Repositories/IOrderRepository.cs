using GloboTicket.Services.Ordering.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GloboTicket.Services.Ordering.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersForUser(Guid userId);
        Task AddOrder(Order order);
        Task<Order> GetOrderById(Guid orderId);
        Task UpdateOrderEventInformation(Models.EventUpdate eventUpdate);
        Task UpdateOrderPaymentStatus(Guid orderId, bool paid);

    }
}
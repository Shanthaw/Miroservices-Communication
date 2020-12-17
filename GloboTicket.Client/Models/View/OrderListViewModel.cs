using GloboTicket.Web.Models.Api;
using System.Collections.Generic;

namespace GloboTicket.Web.Models.View
{
    public class OrderListViewModel
    {
        public bool HasMessage { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}

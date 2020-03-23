using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTelecomApp.Models
{
    public class OrdersBean
    {
        public IEnumerable<Orders> Orders { get; set; }
        public IEnumerable<string> Statuses { get; set; }
        public IEnumerable<string> Packages { get; set; }
        public OrdersBean(IEnumerable<Orders> orders)
        {
            Orders = orders;
        }
    }
}
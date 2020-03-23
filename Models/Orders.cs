using System;
using System.Collections.Generic;

namespace CTelecomApp.Models
{
    public partial class Orders
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public int StatusId { get; set; }
        public int PackId { get; set; }
        public string ClientPhone { get; set; }

        public virtual OrderStatus Status { get; set; }
    }
}

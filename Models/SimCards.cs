using System;
using System.Collections.Generic;

namespace CTelecomApp.Models
{
    public partial class SimCards
    {
        public SimCards()
        {
            SimServices = new HashSet<SimServices>();
        }

        public string SimId { get; set; }
        public string PhoneNumber { get; set; }
        public int TariffId { get; set; }
        public int StatusCode { get; set; }
        public string CustomerId { get; set; }
        public double Balance { get; set; }
        public DateTime? ActiveTill { get; set; }
        public DateTime? Activated { get; set; }
        public double MinutesAmount { get; set; }
        public int SmsAmount { get; set; }
        public double TrafficAmount { get; set; }

        public virtual Users Customer { get; set; }
        public virtual SimStatus StatusCodeNavigation { get; set; }
        public virtual Tariffs Tariff { get; set; }
        public virtual ICollection<SimServices> SimServices { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace CTelecomApp.Models
{
    public partial class Services
    {
        public Services()
        {
            SimServices = new HashSet<SimServices>();
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public double Price { get; set; }
        public int Term { get; set; }
        public int PackageType { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }

        public virtual PackageTypes PackageTypeNavigation { get; set; }
        public virtual ICollection<SimServices> SimServices { get; set; }
    }
}

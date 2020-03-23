using System;
using System.Collections.Generic;

namespace CTelecomApp.Models
{
    public partial class StarterPacks
    {
        public int PackId { get; set; }
        public int TariffId { get; set; }
        public double Price { get; set; }
        public DateTime Created { get; set; }
        public string PackName { get; set; }
        public string Description { get; set; }

        public virtual Tariffs Tariff { get; set; }
    }
}

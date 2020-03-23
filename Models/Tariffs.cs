using System;
using System.Collections.Generic;

namespace CTelecomApp.Models
{
    public partial class Tariffs
    {
        public Tariffs()
        {
            SimCards = new HashSet<SimCards>();
            StarterPacks = new HashSet<StarterPacks>();
        }

        public int TariffId { get; set; }
        public string TariffName { get; set; }
        public double Price { get; set; }
        public int Term { get; set; }
        public double MinutesPackage { get; set; }
        public int SmsPackage { get; set; }
        public double TrafficPackage { get; set; }
        public string Description { get; set; }

        public virtual ICollection<SimCards> SimCards { get; set; }
        public virtual ICollection<StarterPacks> StarterPacks { get; set; }
    }
}

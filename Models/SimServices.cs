using System;
using System.Collections.Generic;

namespace CTelecomApp.Models
{
    public partial class SimServices
    {
        public string SimId { get; set; }
        public int ServiceId { get; set; }
        public DateTime ActivationDate { get; set; }
        public int ActivationId { get; set; }
        public int StatusCode { get; set; }

        public virtual Services Service { get; set; }
        public virtual SimCards Sim { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace CTelecomApp.Models
{
    public partial class SimStatus
    {
        public SimStatus()
        {
            SimCards = new HashSet<SimCards>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<SimCards> SimCards { get; set; }
    }
}

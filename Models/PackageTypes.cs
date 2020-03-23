using System;
using System.Collections.Generic;

namespace CTelecomApp.Models
{
    public partial class PackageTypes
    {
        public PackageTypes()
        {
            Services = new HashSet<Services>();
        }

        public int TypeId { get; set; }
        public string TypeName { get; set; }

        public virtual ICollection<Services> Services { get; set; }
    }
}

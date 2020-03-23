using System;
using System.Collections.Generic;

namespace CTelecomApp.Models
{
    public partial class ContractStatus
    {
        public ContractStatus()
        {
            Users = new HashSet<Users>();
        }

        public int StatusId { get; set; }   
        public string StatusName { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}

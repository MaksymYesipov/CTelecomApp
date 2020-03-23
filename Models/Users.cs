using System;
using System.Collections.Generic;

namespace CTelecomApp.Models
{
    public partial class Users
    {
        public Users()
        {
            SimCards = new HashSet<SimCards>();
        }

        public string Surname { get; set; }
        public int ContractStatus { get; set; }
        public DateTime RegDate { get; set; }
        public string Name { get; set; }
        public string IdCard { get; set; }
        public DateTime? Birthday { get; set; }
        public double Balance { get; set; }
        public string Password { get; set; }

        public virtual ContractStatus ContractStatusNavigation { get; set; }
        public virtual ICollection<SimCards> SimCards { get; set; }
    }
}

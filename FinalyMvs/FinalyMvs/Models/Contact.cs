using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalyMvs.Models
{
    public class Contact
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public string Position { get; set; }

        public string Phone { get; set; }


        public int ClientsID { get; set; }

        public virtual Client Clients { get; set; }

    }
}

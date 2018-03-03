using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalyMvs.Models
{
    public class Tickets
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Problem { get; set; }

        public string State { get; set; }

        public int UsersID { get; set; }

        public int ClientsID { get; set; }

        public virtual Client Clients { get; set; }

        public virtual Users Users { get; set; }
    }
}

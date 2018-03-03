using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalyMvs.Models
{
    public class Meeting
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Type { get; set; }


        public int UsersID { get; set; }

        public int ClientsID { get; set; }

        public virtual Client Clients { get; set; }

        public virtual Users Users { get; set; }

    }
}

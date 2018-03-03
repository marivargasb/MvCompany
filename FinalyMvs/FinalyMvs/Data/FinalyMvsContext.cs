using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FinalyMvs.Models;

namespace FinalyMvs.Models
{
    public class FinalyMvsContext : DbContext
    {
        public FinalyMvsContext (DbContextOptions<FinalyMvsContext> options)
            : base(options)
        {
        }

        public DbSet<FinalyMvs.Models.Users> Users { get; set; }

        public DbSet<FinalyMvs.Models.Client> Client { get; set; }

        public DbSet<FinalyMvs.Models.Contact> Contact { get; set; }

        public DbSet<FinalyMvs.Models.Tickets> Tickets { get; set; }

        public DbSet<FinalyMvs.Models.Meeting> Meeting { get; set; }
    }
}

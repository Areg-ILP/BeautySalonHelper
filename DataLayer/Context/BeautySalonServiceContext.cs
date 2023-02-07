using BeautySalonService.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautySalonService.DataLayer.Context
{
    public class BeautySalonServiceDbContext : DbContext
    {
        public BeautySalonServiceDbContext(DbContextOptions<BeautySalonServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}


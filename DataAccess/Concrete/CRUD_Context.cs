using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class CRUD_Context : IdentityDbContext
    {
        public DbSet<Personal> Personals { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    //    public CRUD_Context(DbContextOptions<CRUD_Context> options)
    //: base(options)
    //    { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.
                UseSqlServer("Server=LAPTOP-80B8G1I7;Database=CRUD;Trusted_Connection=True;Connect Timeout=30;MultipleActiveResultSets=True;");

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Personal>();
        }
    }
}

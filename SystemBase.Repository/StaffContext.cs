using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SystemBase.Repository.Interfaces;
using SystemBase.Repository.Models;

namespace SystemBase.Repository
{
    public class StaffContext : DbContext
    {
        public StaffContext(DbContextOptions<StaffContext> options) : base(options)
        {
        }

        public DbSet<Staff> Users { get; set; }
    }
}

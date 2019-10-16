using ClinicApp.Entitties.Models;
using ClinicApp.Repo.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Repo
{
    public class ClinicAppContext : DbContext
    {
        public DbSet<Clinic> Clinices { get; set; }
        public DbSet<Department> Departmentes { get; set; }
        public ClinicAppContext() : base("DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClinicConfiguration());
            modelBuilder.Configurations.Add(new DepartmentConfiguration());
        }

    }
}

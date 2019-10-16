using ClinicApp.Entitties.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Repo.Configuration
{
    public class DepartmentConfiguration : EntityTypeConfiguration<Department>
    {
        public DepartmentConfiguration()
        {
            ToTable("Departments");
            Property(g => g.ArabicName).IsRequired().HasMaxLength(50);
            Property(g => g.EnglishName).IsRequired().HasMaxLength(50);
        }
    }
}

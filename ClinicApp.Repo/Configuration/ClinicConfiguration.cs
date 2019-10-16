using ClinicApp.Entitties.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Repo.Configuration
{
    public class ClinicConfiguration : EntityTypeConfiguration<Clinic>
    {
        public ClinicConfiguration()
        {
            ToTable("Clinics");
            Property(g => g.ArabicName).IsRequired().HasMaxLength(50);
            Property(g => g.EnglishName).IsRequired().HasMaxLength(50);
            Property(g => g.DepartmentId).IsRequired();
            Property(g => g.PhoneNumber);
            Property(g => g.Status).IsRequired();
            Property(g => g.Title);
        }
    }
}

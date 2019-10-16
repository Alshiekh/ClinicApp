namespace ClinicApp.Repo.Migrations
{
    using ClinicApp.Entitties.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ClinicApp.Repo.ClinicAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ClinicApp.Repo.ClinicAppContext context)
        {
            //  This method will be called after migrating to the latest version.

            GetDepartments().ForEach(d => context.Departmentes.Add(d));
            //GetClinics().ForEach(g => context.Clinices.Add(g));

            context.SaveChanges();
        }
       
        private static List<Department> GetDepartments()
        {
            return new List<Department>
            {
                new Department {
                    EnglishName = "Dep1",
                    ArabicName = "Dep1"
                },
                new Department {
                    EnglishName = "Dep2",
                    ArabicName = "Dep2"
                },
                new Department {
                    EnglishName = "Dep3",
                    ArabicName = "Dep3"
                }
            };
        }
        private static List<Clinic> GetClinics()
        {
            return new List<Clinic>
            {
                new Clinic {
                    ArabicName = "Clinic One",
                    EnglishName = "Clinic One",
                    DepartmentId = 1 ,
                    PhoneNumber = "+966546506584",
                    Status = false,
                    Title = "Clinic ONE"
                }
            };
        }
    }
    
}

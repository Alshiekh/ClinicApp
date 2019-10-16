using ClinicApp.Entitties.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Repo
{
    public class ClinicSeed : DropCreateDatabaseIfModelChanges<ClinicAppContext>
    {

        protected override void Seed(ClinicAppContext context)
        {
            //GetDepartments().ForEach(d => context.Departmentes.Add(d));
            //GetClinicStatuses().ForEach(cs => context.ClinicStatuses.Add(cs));
            //GetClinics().ForEach(g => context.Clinices.Add(g));

            //context.SaveChanges();
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

using ClinicApp.Entitties.Models;
using Store.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Repo.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
         // New Feature Not Found In BASE 
        public Department GetDepartmentByName(string departmentName)
        {
            var department = this.DbContext.Departmentes.Where(c => c.EnglishName == departmentName).FirstOrDefault();

            return department;
        }
        // Ovveride Update Feature 
        public override void Update(Department entity)
        {
            //entity.EnglishName = DateTime.Now;
            base.Update(entity);
        }
    }
    public interface IDepartmentRepository : IRepository<Department>
    {
        Department GetDepartmentByName(string departmentName);
    }
}

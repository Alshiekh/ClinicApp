using ClinicApp.Entitties.Models;
using Store.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Repo.Repositories
{
    public class ClinicRepository : RepositoryBase<Clinic>, IClinicRepository
    {
        public ClinicRepository(IDbFactory dbFactory)
          : base(dbFactory) { }

        public IEnumerable<Clinic> GetAllClinic()
        {
            var clinic = this.DbContext.Clinices.Include("Department").ToList();
            return clinic;
        }
    }
    public interface IClinicRepository : IRepository<Clinic>
    {
         IEnumerable<Clinic> GetAllClinic();
    }

}

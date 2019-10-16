using ClinicApp.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        ClinicAppContext dbContext;

        public ClinicAppContext Init()
        {
            return dbContext ?? (dbContext = new ClinicAppContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}

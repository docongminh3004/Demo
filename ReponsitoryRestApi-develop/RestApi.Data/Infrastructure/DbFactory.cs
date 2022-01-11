

using Microsoft.EntityFrameworkCore;
using RestApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
       private  AppDBContext dbContext;
        DbContextOptions<AppDBContext> _options;


        public DbFactory(DbContextOptions<AppDBContext> options)
        {
            _options = options;
        }

        public AppDBContext Init()
        {
            return dbContext ?? (dbContext = new AppDBContext(_options));
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}

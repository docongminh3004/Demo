

using RestApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
       AppDBContext Init();
    }
}

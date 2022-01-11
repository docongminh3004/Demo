using RestApi.Data.Entities;
using RestApi.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Data.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>//hàm này kế thừa được các phương thức của IRepository rồi thì chỉ cần gọi ra
    {
        void SaveChanges();
    }
    public class DepartmentRepository: RepositoryBase<Department>, IDepartmentRepository//định RepositoryBase là kiểu Department thì Bên lớp cha(RepositoryBase ) sẽ nhận kiểu Department (T -> Department)

    {
        public DepartmentRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}
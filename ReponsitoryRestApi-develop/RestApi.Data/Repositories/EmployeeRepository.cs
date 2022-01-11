using RestApi.Data.Entities;
using RestApi.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Data.Repositories
{
    public interface IEmployeeReponsitory : IRepository<Employee>//hàm này kế thừa được các phương thức của IRepository rồi thì chỉ cần gọi ra
    {
        void SaveChanges();
    }
    public class EmployeeRepository:RepositoryBase<Employee>, IEmployeeReponsitory//định RepositoryBase là kiểu EMployee thì Bên lớp cha(RepositoryBase ) sẽ nhận kiểu Employee (T -> Employee)
    {
        public EmployeeRepository(IDbFactory dbFactory):base(dbFactory)
        {

        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}

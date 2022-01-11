using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Data.Entities
{
    public class AppDBContext:DbContext
    {


        public AppDBContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }//thuộc tính này đại diện cho 1 bảng 1 database,chứa department
        public DbSet<Employee> Employees { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //        "Server=ADMIN;Database=eShop;Trusted_Connection=True;");
        //}

    }
}

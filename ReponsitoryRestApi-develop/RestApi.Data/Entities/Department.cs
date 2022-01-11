using RestApi.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Data.Entities
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [NotEmpty]
        [MaxLength(255)]
        [PropertyName("Tên phòng ban")]
        public string DepartmemtName { get; set; }
        [MaxLength(255)]
        [NotEmpty]
        [propMaxLength(50)]
        public string Description { get; set; }
        public ICollection<Employee> Employees { get; set; }
        //nếu employee chưa có thì có ds rỗng
        public Department()
        {
            Employees = new HashSet<Employee>();
        }
    }
}

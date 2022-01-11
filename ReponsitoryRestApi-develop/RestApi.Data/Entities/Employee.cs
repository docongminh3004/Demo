using RestApi.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Data.Entities
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [PropertyName("Mã nhân viên")]
        [NotEmpty]
        [MaxLength(20)]
     
        public string EmployeeCode { get; set; }
        [PropertyName("Tên nhân viên")]
        [MaxLength(255)]
        [NotEmpty]
        [propMaxLength(50)]
        public string EmployeeName { get; set; }
        [PropertyName("Ngày sinh")]
        [CheckDate]
        public DateTime DateOfBirth { get; set; }
        [PropertyName("Phòng ban")]
        [NotEmpty]
        public int? DepartmentId { get; set; }
        [ForeignKey("Department")]
 
      
        public string Address { get; set; }

        [PropertyName("Điện thoại")]
        [propMaxLength(12)]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal? Salary { get; set; }
    }
}

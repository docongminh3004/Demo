using RestApi.Data.Attributes;
using RestApi.Data.Entities;
using RestApi.Data.Exceptions;
using RestApi.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Services
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetAll();
        object GetById(int departmentId);
        int? Add(Department department);
        void Update(int departmentId, Department department);
        void Delete(int department);
    }
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentReponsitory;
        public DepartmentService(IDepartmentRepository departmentReponsitory)
        {
            _departmentReponsitory = departmentReponsitory;
        }
        public IEnumerable<Department> GetAll()
        {
            return _departmentReponsitory.GetAll();
        }
        public object GetById(int departmentId)
        {
            return _departmentReponsitory.GetById(departmentId);
        }
        public int? Add(Department department)
        {

            var isValid = ValidateObject(department);
            if (isValid == true)
            {
                _departmentReponsitory.Add(department);
                _departmentReponsitory.SaveChanges();
            }
            return null;
        }

        bool ValidateObject(Department department)
        {
            List<string> errorMsg = new List<string>();

            //các thông tin bắt buộc nhập
            //1.kiểm tra tất cả các property của đối tượng.
            var properties = typeof(Department).GetProperties();
            foreach (var item in properties)
            {
                // 1.1 lấy giá trị
                var propValue = item.GetValue(department);//ktra gtri o day
                // 1.2 lấy tên hiển thị
                var propNameDisplay = item.Name;
                // 1.7 Lấy ra các propertyName
                var propertyNames = item.GetCustomAttributes(typeof(PropertyName), true);
                // 1.4 Check notempty
                var propNotEmptys = item.GetCustomAttributes(typeof(NotEmpty), true);//kiểm tra bên att notempty

     

                if (propertyNames.Length > 0)
                {
                    // Thay đổi giá trị cũ của entity gán bằng PropertyName được đặt
                    propNameDisplay = ((PropertyName)propertyNames[0]).Name;//lấy dc tên  vd(mã nhân viên)
                }

                if (propNotEmptys.Length > 0)
                {
                    //3.nếu thông tin bắt buộc nhập thì cảnh báo/đánh dấu trạng thái không hợp lệ.
                    //trim bỏ các khoảng cách 2 đầu
                    if (propValue == null || string.IsNullOrEmpty(propValue.ToString().Trim()))
                    {
                        errorMsg.Add($"{propNameDisplay} không được phép để trống.");
                        // throw new Exception($"Thông tin {propNameDisplay} không được phép để trống.");
                    }
                }
   

    
            }
            // bắt lỗi sử dụng Exception Handling Middleware .
            if (errorMsg.Count > 0)
            {
                throw new HttpResponseException(errorMsg);
            }
            return true;
        }

        public void Update(int departmentId, Department department)
        {
            var isValid = ValidateObject(department);
            if (isValid == true)
            {
                _departmentReponsitory.Update(departmentId, department);
                _departmentReponsitory.SaveChanges();
            }

        }
        public void Delete(int departmentId)
        {
            var tbl = _departmentReponsitory.GetById(departmentId);
            _departmentReponsitory.Delete(tbl);
            _departmentReponsitory.SaveChanges();
        }

        //public void Update(int employeeId, Employee employee)
        //{

        //        _employeeReponsitory.Update(employeeId, employee);
        //        _employeeReponsitory.SaveChanges();


        //}

        //public void Delete(int employeeId)
        //{
        //    var tbl = _employeeReponsitory.GetById(employeeId);
        //    _employeeReponsitory.Delete(tbl);
        //    _employeeReponsitory.SaveChanges();
        //}
    }
}
    



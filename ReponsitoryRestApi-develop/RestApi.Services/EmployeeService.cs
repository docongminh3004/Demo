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
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll();
        object GetById(int employeeId);
        int? Add(Employee employee);
        void Update(int employeeId, Employee employee);
        void Delete(int employeeId);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeReponsitory _employeeReponsitory;
        public EmployeeService(IEmployeeReponsitory employeeReponsitory)
        {
            _employeeReponsitory = employeeReponsitory;
        }
        public IEnumerable<Employee> GetAll()
        {
            return  _employeeReponsitory.GetAll();
        }
        public object GetById(int employeeId)
        {
           return _employeeReponsitory.GetById(employeeId);
        }
        public int? Add(Employee employee)
        {

            var isValid = ValidateObject(employee);
            if (isValid==true)
            {
                _employeeReponsitory.Add(employee);
                _employeeReponsitory.SaveChanges();
            }
            return null;
        }
        bool ValidateObject(Employee employee)
        {
            List<string> errorMsg = new List<string>();

            //các thông tin bắt buộc nhập
            //1.kiểm tra tất cả các property của đối tượng.
            var properties = typeof(Employee).GetProperties();
            foreach (var item in properties)
            {
                // 1.1 lấy giá trị
                var propValue = item.GetValue(employee);//ktra gtri o day
                 // 1.2 lấy tên hiển thị
                var propNameDisplay = item.Name;
                // 1.7 Lấy ra các propertyName
                var propertyNames = item.GetCustomAttributes(typeof(PropertyName), true);
                // 1.4 Check notempty
                var propNotEmptys = item.GetCustomAttributes(typeof(NotEmpty), true);//kiểm tra bên att notempty

                // 1.3 Lấy ra độ dài của kí tự của property
                var propertyMaxLength = item.GetCustomAttributes(typeof(propMaxLength), true);

                // 1.5.check ngày sinh không thể lớn hơn ngày hiện tại
                var checkDate = item.GetCustomAttributes(typeof(CheckDate), true);

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
                if (propertyMaxLength.Length > 0)
                {
                    //lấy ra độ dài của ký tự
                    var length = ((propMaxLength)propertyMaxLength[0]).Length;
                    //3. nếu thông tin bắt buộc nhập hiển thị cảnh báo hoặc đánh giấu trang thái không hợp lệ
                    if (propValue != null && ((string)propValue).Trim().Length > length)
                    {
                        //errorMsg.Add($"Thông tin {propNameDisplay} không được phép dài quá {length} kí tự.");
                        errorMsg.Add($"{propNameDisplay} không được dài quá {length} kí tự");
                    }
                }

                if (checkDate.Length > 0)
                {
                    if (propValue != null)
                    {
                        //nếu ngày sinh lớn hơn hiện tại
                        if ((DateTime)propValue > DateTime.Now)
                        {
                            errorMsg.Add($"{propNameDisplay} không thể lớn hơn ngày hiện tại.");
                        }
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
        public void Update(int employeeId,Employee employee)
        {
            var isValid = ValidateObject(employee);
            if (isValid == true)
            {
                _employeeReponsitory.Update(employeeId, employee);
                _employeeReponsitory.SaveChanges();
            }
          
        }

        public void Delete(int employeeId)
        {
            var tbl = _employeeReponsitory.GetById(employeeId);
            _employeeReponsitory.Delete(tbl);
            _employeeReponsitory.SaveChanges();
        }
    }
}

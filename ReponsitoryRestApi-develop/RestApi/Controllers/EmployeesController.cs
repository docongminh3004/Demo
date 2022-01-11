using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi.Data.Entities;
using RestApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;//khai báo sevis
        public EmployeesController(IEmployeeService employeeService)// truyền dữ liệu _employeeService vào contructer,
        {
            _employeeService = employeeService;

        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_employeeService.GetAll());
        }
        [HttpGet("{employeeId}")]
        public IActionResult GetById(int employeeId)
        {
           var entities = _employeeService.GetById(employeeId);
            return StatusCode (200, entities);
        }
        [HttpPost]
        public IActionResult Add([FromBody] Employee employee)
        {
             var entities= _employeeService.Add(employee);
            return StatusCode(201,entities);
            
            
        }
        [HttpPut("{employeeId}")]
        public IActionResult Update(int employeeId, Employee employee)
        {
           
              _employeeService.Update(employeeId,employee);
                return StatusCode(200, 1);
            
           
        }
        [HttpDelete("{employeeId}")]
        public IActionResult Delete(int employeeId)
        {
            _employeeService.Delete(employeeId);
            return StatusCode(200, 1);
        }
    }
}

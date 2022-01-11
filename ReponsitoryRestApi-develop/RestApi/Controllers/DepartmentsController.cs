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
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;//khai báo sevis
        public DepartmentsController(IDepartmentService departmentService)// truyền dữ liệu _departmentService vào contructer,
        {
            _departmentService = departmentService;

        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_departmentService.GetAll());
        }
        [HttpGet("{departmentId}")]
        public IActionResult GetById(int departmentId)
        {
            var entities = _departmentService.GetById(departmentId);
            return StatusCode(200, entities);
        }
        [HttpPost]
        public IActionResult Add([FromBody] Department department)
        {
            var entities = _departmentService.Add(department);
            return StatusCode(201, entities);


        }
        [HttpPut("{departmentId}")]
        public IActionResult Update(int departmentId, Department department)
        {

            _departmentService.Update(departmentId, department);
            return StatusCode(200, 1);


        }
        [HttpDelete("{departmentId}")]
        public IActionResult Delete(int departmentId)
        {
            _departmentService.Delete(departmentId);
            return StatusCode(200, 1);
        }
    }
}

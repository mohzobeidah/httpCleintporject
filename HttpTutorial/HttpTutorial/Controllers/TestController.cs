using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace HttpTutorial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController:ControllerBase
    {
        [HttpGet("GetStudents")]
        public List<Student> GetStudents()
        {
            var std = new Student().GetStudents();
            return std;
        }
    }
}
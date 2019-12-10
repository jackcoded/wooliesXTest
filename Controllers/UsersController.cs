using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wooliesXTest.Models;

namespace wooliesXTest.Controllers
{
    [Route("api/answers")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/Users
        [HttpGet("user")]
        public User Get()
        {
            return new User { Name = "Jack", Token = Guid.NewGuid() };
        }
    }
}

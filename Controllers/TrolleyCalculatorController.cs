using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wooliesXTest.Services;
using wooliesXTest.Models;
namespace wooliesXTest.Controllers
{
    [Route("api/trolleyTotal")]
    [ApiController]
    public class TrolleyCalculatorController : ControllerBase
    {
        private readonly ITrolleyCalculatorServices _trolleyCalculatorServices;
        public TrolleyCalculatorController(ITrolleyCalculatorServices trolleyCalculatorServices)
        {
            _trolleyCalculatorServices = trolleyCalculatorServices;
        }
        // POST: api/trolleyTotal
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Trolley request)
        {
           var total = await _trolleyCalculatorServices.GetTotal(request);
           return Ok(total);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppSkiii.Model;
using WebAppSkiii.Service.Interface;

namespace WebAppSkiii.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkiController : ControllerBase
    {
        
        private readonly ISkiService skiService;
        private readonly ILogger<SkiController> logger;

        public SkiController(ISkiService skiService,ILogger<SkiController> logger)
        {
            this.skiService = skiService;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<Ski> Get([FromQuery] int length, [FromQuery] int age, [FromQuery] Style style)
        {
            return await skiService.GetSki(length, age, style);
        }
    }
}

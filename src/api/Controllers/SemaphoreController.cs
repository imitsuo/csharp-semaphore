using System.Collections.Generic;
using api.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/v1/Semaphore")]    
    [ApiController]
    public class SemaphoreController : ControllerBase
    {
        private readonly ISemaphoreService _service;

        public SemaphoreController(ISemaphoreService semaphoreService)
        {
            _service = semaphoreService;
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(_service.GetResourcesLocked());
        }
        
        [HttpPost]
        public ActionResult Post([FromBody] string value)
        {
            if (string.IsNullOrEmpty(value))
                return BadRequest("value is Empty");
            
            if (_service.Lock(value))
                return Ok(value);
            
            return BadRequest($"Value: [{value}] already locked");
        }
       

        [HttpDelete("{value}")]
        public ActionResult Delete(string value)
        {
            if (string.IsNullOrEmpty(value))
                return BadRequest("value is Empty");
            
            if (_service.Release(value))
                return Ok(value);
            
            return BadRequest($"Value: [{value}] already released");
        }
    }
}

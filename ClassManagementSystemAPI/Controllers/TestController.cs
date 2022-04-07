using ClassManagementSystemAPI.Data;
using ClassManagementSystemAPI.Models;
using ClassManagementSystemAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ClassManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TestServices _testServices;

        public TestController(TestServices testServices)
        {
            _testServices = testServices;
        }
            
        [HttpGet]
        public async Task<ActionResult<Test>> Get()
        {
            var test = await _testServices.GetAsync();
            return Ok(test);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Test>> Get(string id)
        {
            var book = await _testServices.GetAsync(id);
            if (book == null)
            {
                return BadRequest("Null");
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Test>> Post(Test test)
        {
            await _testServices.CreateAsync(test);
            return CreatedAtAction(nameof(Get), new { id = test.Id }, test);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult<Test>> Update(string id, Test test)
        {
            var book = await _testServices.GetAsync(id);
            if (book == null)
            {
                return BadRequest();
            }

            test.Id = book.Id;
            await _testServices.UpdateAsync(id, test);
            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult<Test>> Delete(string id)
        {
            var book = await _testServices.GetAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            await _testServices.RemoveAsync(id);
            return Ok();
        }
    }
}

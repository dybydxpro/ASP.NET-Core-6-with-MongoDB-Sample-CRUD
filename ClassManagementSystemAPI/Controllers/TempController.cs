using ClassManagementSystemAPI.Models;
using ClassManagementSystemAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempController : ControllerBase
    {
        private readonly TempServices _tempServices;

        public TempController(TempServices tempServices)
        {
            _tempServices = tempServices;
        }

        [HttpGet]
        public async Task<ActionResult<Temp>> Get()
        {
            var test = await _tempServices.GetAsync();
            return Ok(test);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Temp>> Get(string id)
        {
            var data = await _tempServices.GetAsync(id);
            if (data == null)
            {
                return BadRequest("Null");
            }

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<Temp>> Post(Temp temp)
        {
            await _tempServices.CreateAsync(temp);
            return CreatedAtAction(nameof(Get), new { id = temp.Id }, temp);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult<Temp>> Update(string id, Temp temp)
        {
            var data = await _tempServices.GetAsync(id);
            if (data == null)
            {
                return BadRequest();
            }

            temp.Id = data.Id;
            await _tempServices.UpdateAsync(id, temp);
            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult<Temp>> Delete(string id)
        {
            var data = await _tempServices.GetAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            await _tempServices.RemoveAsync(id);
            return Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FamiliesWebAPI.Data;
using FamiliesWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamiliesWebAPI.Controllers
{
   [ApiController]
    [Route("[controller]")]
    public class ChildrenController : ControllerBase
    {
        private readonly IChildrenService childrenService;

        public ChildrenController(IChildrenService childrenService)
        {
            this.childrenService = childrenService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IList<Child>>> GetChildren([FromQuery] int? familyId)
        {
            try
            {
                IList<Child> children = await childrenService.GetChildrenAsync();
                if (familyId != null)
                {
                    children = await childrenService.GetFamilyChildrenAsync(familyId);
                }
                return Ok(children);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<Child>> AddChild([FromQuery] int familyId, [FromBody] Child child)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Child added = await childrenService.AddChildAsync(familyId, child);
                return Created($"/{added.Id}", added);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult<Child>> UpdateChild([FromQuery] int familyId, [FromBody] Child child)
        {
            try
            {
                Child updatedChild = await childrenService.UpdateChildAsync(familyId, child);
                return Ok(updatedChild);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteAdult([FromQuery] int familyId, [FromRoute] int id)
        {
            try
            {
                await childrenService.RemoveChildAsync(familyId, id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
    }
}
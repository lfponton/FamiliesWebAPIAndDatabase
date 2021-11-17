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
    public class AdultsController : ControllerBase
    {
        private readonly IAdultsService adultsService;

        public AdultsController(IAdultsService adultsService)
        {
            this.adultsService = adultsService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IList<Adult>>> GetAdults([FromQuery] int? familyId)
        {
            try
            {
                IList<Adult> adults = await adultsService.GetAdultsAsync();
                if (familyId != null)
                {
                    adults = await adultsService.GetFamilyAdultsAsync(familyId);
                }
                return Ok(adults);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<Adult>> AddAdult([FromQuery] int familyId, [FromBody] Adult adult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Adult added = await adultsService.AddAdultAsync(familyId, adult);
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
        public async Task<ActionResult<Adult>> UpdateAdult([FromQuery] int familyId, [FromBody] Adult adult)
        {
            try
            {
                Adult updatedAdult = await adultsService.UpdateAdultAsync(familyId, adult);
                return Ok(updatedAdult);
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
                await adultsService.RemoveAdultAsync(familyId, id);
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
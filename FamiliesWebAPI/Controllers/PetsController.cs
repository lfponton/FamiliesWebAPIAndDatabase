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
    public class PetsController : ControllerBase
    {
        private readonly IPetsService petsService;

        public PetsController(IPetsService petsService)
        {
            this.petsService = petsService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IList<Pet>>> GetPets([FromQuery] int? familyId)
        {
            try
            {
                IList<Pet> pets = await petsService.GetPetsAsync();
                if (familyId != null)
                {
                    pets = await petsService.GetFamilyPetsAsync(familyId);
                }
                return Ok(pets);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<Pet>> AddPet([FromQuery] int familyId, [FromBody] Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Pet added = await petsService.AddPetAsync(familyId, pet);
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
        public async Task<ActionResult<Adult>> UpdatePet([FromQuery] int familyId, [FromBody] Pet pet)
        {
            try
            {
                Pet updatedPet = await petsService.UpdatePetAsync(familyId, pet);
                return Ok(updatedPet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeletePet([FromQuery] int familyId, [FromRoute] int id)
        {
            try
            {
                await petsService.RemovePetAsync(familyId, id);
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
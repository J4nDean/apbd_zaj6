using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using cwiczenia4.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalDbService _service;

        public AnimalsController(IAnimalDbService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAnimals(string orderBy = "name")
        {
            var animals = _service.GetAnimals(orderBy);
            return Ok(animals);
        }

        [HttpPost]
        public IActionResult CreateAnimal(Animal newAnimal)
        {
            _service.AddAnimal(newAnimal);
            return CreatedAtAction(nameof(GetAnimals), new { id = newAnimal.IdAnimal }, newAnimal);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAnimal(int id, Animal updatedAnimal)
        {
            if (id != updatedAnimal.IdAnimal)
            {
                return BadRequest();
            }

            _service.UpdateAnimal(updatedAnimal);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAnimal(int id)
        {
            _service.DeleteAnimal(id);
            return NoContent();
        }
    }
}
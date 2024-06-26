﻿using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalsDbService _service;

        public AnimalsController(IAnimalsDbService service)
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
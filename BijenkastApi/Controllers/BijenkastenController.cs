using BijenkastApi.DTOs;
using BijenkastApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BijenkastApi.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BijenkastenController : ControllerBase
    {
        private readonly IBijenkastRepository _bijenkastRepository;

        public BijenkastenController(IBijenkastRepository context)
        {
            _bijenkastRepository = context;
        }

        [HttpGet]
        public IEnumerable<Bijenkast> GetBijenkasten()
        {
            return _bijenkastRepository.GetAll().OrderBy(r => r.Name);
        }

        [HttpGet("{id}")]
        public ActionResult<Bijenkast> GetBijenkast(int id)
        {
            Bijenkast bijenkast = _bijenkastRepository.GetBy(id);
            if (bijenkast == null) return NotFound();
            return bijenkast;
        }

        [HttpPost]
        public ActionResult<Bijenkast> PostBijenkast(BijenkastDTO bijenkast)
        {
            Bijenkast aanTeMakenBijenkast = new Bijenkast() { Name = bijenkast.Name };
            _bijenkastRepository.Add(aanTeMakenBijenkast);
            _bijenkastRepository.SaveChanges();
            return CreatedAtAction(nameof(GetBijenkast), new { id = aanTeMakenBijenkast.Id }, aanTeMakenBijenkast);
        }

        // PUT: api/Bijenkasten/1
        [HttpPut("{id}")]
        public IActionResult PutRecipe(int id, Bijenkast bijenkast)
        {
            if (id != bijenkast.Id)
            {
                return BadRequest();
            }
            _bijenkastRepository.Update(bijenkast);
            _bijenkastRepository.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Bijenkasten/5
        [HttpDelete("{id}")]
        public ActionResult<Bijenkast> DeleteRecipe(int id)
        {
            Bijenkast bijenkast = _bijenkastRepository.GetBy(id);
            if (bijenkast == null)
            {
                return NotFound();
            }
            _bijenkastRepository.Delete(bijenkast);
            _bijenkastRepository.SaveChanges();
            return bijenkast;
        }
    }
}
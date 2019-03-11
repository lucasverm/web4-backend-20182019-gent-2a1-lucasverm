using BijenkastApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BijenkastApi.Controllers
{
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
        public ActionResult<Bijenkast> PostBijenkast(Bijenkast bijenkast)
        {
            _bijenkastRepository.Add(bijenkast);
            _bijenkastRepository.SaveChanges();
            return CreatedAtAction(nameof(GetBijenkast), new { id = bijenkast.Id }, bijenkast;
        }
    }
}
using BijenkastApi.DTOs;
using BijenkastApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BijenkastApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BijenkastenController : ControllerBase
    {
        private readonly IBijenkastRepository _bijenkastRepository;
        private readonly IImkerRepository _imkerRepository;

        public BijenkastenController(IBijenkastRepository context, IImkerRepository imkerRepository)
        {
            _bijenkastRepository = context;
            _imkerRepository = imkerRepository;
        }

        ///<summary>
        /// Geeft alle bijenkasten terug
        /// </summary>
        ///<returns>De Bijenkasten</returns>
        [HttpGet]
        public IEnumerable<Bijenkast> GetBijenkasten(int imkerId)
        {
            return _bijenkastRepository.GetAll(imkerId).OrderBy(r => r.Name);
        }

        ///<summary>
        /// Geeft 1 specifieke bijenkast terug dmv een id
        /// </summary>
        ///<param name="id">het id van de bijenkast</param>
        ///<returns>De bijenkast met opgegeven id</returns>
        [HttpGet("{id}")]
        public ActionResult<Bijenkast> GetBijenkast(int id)
        {
            Bijenkast bijenkast = _bijenkastRepository.GetBy(id);
            if (bijenkast == null) return NotFound();
            return bijenkast;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Bijenkast> PostBijenkast(BijenkastDTO bijenkast)
        {
            Bijenkast aanTeMakenBijenkast = new Bijenkast() { Name = bijenkast.Name };
            _bijenkastRepository.Add(aanTeMakenBijenkast);
            _bijenkastRepository.SaveChanges();
            return CreatedAtAction(nameof(GetBijenkast), new { id = aanTeMakenBijenkast.Id }, aanTeMakenBijenkast);
        }

        // PUT: api/Bijenkasten/1
        [HttpPut("{id}")]
        public IActionResult PutBijenkast(int id, Bijenkast bijenkast)
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
        public ActionResult<Bijenkast> DeleteBijenkast(int id)
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
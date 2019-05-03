using BijenkastApi.Data;
using BijenkastApi.DTOs;
using BijenkastApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BijenkastApi.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class TestController : ControllerBase
    {
        private readonly IBijenkastRepository _bijenkastRepository;
        private readonly IImkerRepository _imkerRepository;
        private readonly BijenkastContext _context;

        public TestController(BijenkastContext context, IBijenkastRepository BijenkastRepository, IImkerRepository imkerRepository)
        {
            _context = context;
            _bijenkastRepository = BijenkastRepository;
            _imkerRepository = imkerRepository;
        }

        ///<summary>
        /// Geeft alle bijenkasten terug
        /// </summary>
        ///<returns>De Bijenkasten</returns>
        [HttpGet]
        public IEnumerable<Bijenkast> GetBijenkasten(int imkerId)
        {
            return _bijenkastRepository.GetAll(imkerId).OrderBy(r => r.naam);
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
            Bijenkast aanTeMakenBijenkast = new Bijenkast(bijenkast.naam,
            bijenkast.type, bijenkast.aantalhoningkamers, bijenkast.aantalbroedkamers, bijenkast.aantalramenperkamer, bijenkast.bijenras,
            bijenkast.moergeboortedag, bijenkast.moergeboortemaand, bijenkast.moergeboortejaar,
            bijenkast.moergemerkt, bijenkast.moergeknipt,
 bijenkast.moerbevrucht,
 bijenkast.aanmaakdag, bijenkast.aanmaakmaand, bijenkast.aanmaakjaar


            );
            Imker imker = _imkerRepository.GetBy(bijenkast.imkerId);
            if(imker == null) { return BadRequest(); }
            imker.bijenkasten.Add(aanTeMakenBijenkast);
            _bijenkastRepository.Add(aanTeMakenBijenkast);
            _imkerRepository.Update(imker);
            _bijenkastRepository.SaveChanges();
            _imkerRepository.SaveChanges();
            return CreatedAtAction(nameof(GetBijenkast), new { id = aanTeMakenBijenkast.id }, aanTeMakenBijenkast);
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
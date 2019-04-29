using BijenkastApi.Data;
using BijenkastApi.DTOs;
using BijenkastApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return _bijenkastRepository.GetAll(imkerId).OrderBy(r => r.Name);
        }

        [HttpPut]
        public IEnumerable<Imker> GetImkers()
        {
            return _context.Imkers.Include(t=> t.bijenkasten).ToList();
        }
    }
}
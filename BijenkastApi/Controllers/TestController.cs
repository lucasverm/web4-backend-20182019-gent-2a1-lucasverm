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

        public TestController(IBijenkastRepository context, IImkerRepository imkerRepository)
        {
            _bijenkastRepository = context;
            _imkerRepository = imkerRepository;
        }

        ///<summary>
        /// Geeft alle bijenkasten terug
        /// </summary>
        ///<returns>De Bijenkasten</returns>
        [HttpGet]
        public IEnumerable<Bijenkast> GetBijenkasten()
        {
            return _bijenkastRepository.GetAll().OrderBy(r => r.Name);
        }
    }
}
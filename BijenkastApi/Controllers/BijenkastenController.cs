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
    }
}
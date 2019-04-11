using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace BijenkastApi.Models
{
    public class Imker
    {
        #region Properties

        //add extra properties if needed
        public int ImkerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        #endregion Properties

        #region Constructors

        public Imker()
        {
        }

        #endregion Constructors
    }
}
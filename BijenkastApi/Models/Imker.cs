using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BijenkastApi.Models
{
    public class Imker
    {
        #region Properties

        public int ImkerId { get; set; }

        public string voornaam { get; set; }

        public string achternaam { get; set; }

        public string email { get; set; }

        public bool facebookimker { get; set; }

        public List<Bijenkast> bijenkasten { get; set; }

        #endregion Properties

        #region Constructors

        public Imker()
        {
            bijenkasten = new List<Bijenkast>();
        }

        #endregion Constructors
    }
}
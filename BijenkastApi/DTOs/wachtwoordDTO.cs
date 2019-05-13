using BijenkastApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BijenkastApi.DTOs
{
    public class WachtwoordDTO
    {
        [Required]
        public string wachtwoord { get; set; }
        [Required]
        public string wachtwoordbevestiging { get; set; }
    }
}
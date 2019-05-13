using BijenkastApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BijenkastApi.DTOs
{
    public class ImkerDTO
    {
        [Required]
        public string voornaam { get; set; }
        public string achternaam { get; set; }
        public string email { get; set; }
    }
}
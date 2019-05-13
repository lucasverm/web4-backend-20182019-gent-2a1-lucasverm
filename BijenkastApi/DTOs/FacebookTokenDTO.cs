using BijenkastApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BijenkastApi.DTOs
{
    public class FacebookTokenDTO
    {
        public string token { get; set; }
    }
}
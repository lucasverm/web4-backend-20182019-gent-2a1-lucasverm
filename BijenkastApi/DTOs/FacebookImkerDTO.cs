using BijenkastApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BijenkastApi.DTOs
{
    public class FacebookImkerDTO
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("first_name")]
        public string voornaam { get; set; }
        [JsonProperty("last_name")]
        public string achternaam { get; set; }
        [JsonProperty("username")]
        public string gebruikersnaam { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
    }
}
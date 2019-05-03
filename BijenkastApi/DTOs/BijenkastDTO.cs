using System;
using System.ComponentModel.DataAnnotations;

namespace BijenkastApi.DTOs
{
    public class BijenkastDTO
    {
        //public int id { get; set; }

        [Required]
        public string naam { get; set; }
        public string type { get; set; }
        public int aantalhoningkamers { get; set; }
        public int aantalbroedkamers { get; set; }
        public int aantalramenperkamer { get; set; }
        public string bijenras { get; set; }
        public int moergeboortedag { get; set; }
        public int moergeboortemaand { get; set; }
        public int moergeboortejaar { get; set; }
        public Boolean moergemerkt { get; set; }
        public Boolean moergeknipt { get; set; }
        public Boolean moerbevrucht { get; set; }
        public int aanmaakdag { get; set; }
        public int aanmaakmaand { get; set; }
        public int aanmaakjaar { get; set; }
        public int imkerId { get; set; }
    }
}
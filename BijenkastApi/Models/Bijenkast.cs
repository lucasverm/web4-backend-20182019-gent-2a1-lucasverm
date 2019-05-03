﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BijenkastApi.Models
{
    public class Bijenkast
    {
        #region Properties
        public int id { get; set; }

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

        [ForeignKey("Imker")]
        public int imkerId { get; set; }



        #endregion Properties

        #region Constructors

        public Bijenkast()
        {

        }

        public Bijenkast(string naam, string type, int aantalhoningkamers, int aantalbroedkamers, int aantalramenperkamer, string bijenras, int moergeboortedag, int moergeboortemaand, int moergeboortejaar, bool moergemerkt, bool moergeknipt, bool moerbevrucht, int aanmaakdag, int aanmaakmaand, int aanmaakjaar)
        {
            this.naam = naam;
            this.type = type;
            this.aantalhoningkamers = aantalhoningkamers;
            this.aantalbroedkamers = aantalbroedkamers;
            this.aantalramenperkamer = aantalramenperkamer;
            this.bijenras = bijenras;
            this.moergeboortedag = moergeboortedag;
            this.moergeboortemaand = moergeboortemaand;
            this.moergeboortejaar = moergeboortejaar;
            this.moergemerkt = moergemerkt;
            this.moergeknipt = moergeknipt;
            this.moerbevrucht = moerbevrucht;
            this.aanmaakdag = aanmaakdag;
            this.aanmaakmaand = aanmaakmaand;
            this.aanmaakjaar = aanmaakjaar;
        }





        #endregion Constructors
    }
}
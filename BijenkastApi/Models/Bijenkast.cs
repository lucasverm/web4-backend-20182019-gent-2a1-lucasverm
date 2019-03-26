using System;
using System.ComponentModel.DataAnnotations;

namespace BijenkastApi.Models
{
    public class Bijenkast
    {
        #region Properties

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime Created { get; set; }

        #endregion Properties

        #region Constructors

        public Bijenkast()
        {
            Created = DateTime.Now;
        }

        public Bijenkast(string name) : this()
        {
            Name = name;
        }

        #endregion Constructors
    }
}
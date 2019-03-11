using System;

namespace BijenkastApi.Models
{
    public class Moer
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public DateTime Geboortedag { get; set; }

        public Boolean geknipt { get; set; }

        public Boolean gemerkt { get; set; }

        // public Bijenkast bijenkast { get; set; }
    }
}
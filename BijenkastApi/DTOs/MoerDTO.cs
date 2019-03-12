using System;

namespace BijenkastApi.DTOs
{
    public class MoerDTO
    {
        public String Name { get; set; }

        public DateTime Geboortedag { get; set; }

        public Boolean geknipt { get; set; }

        public Boolean gemerkt { get; set; }
    }
}
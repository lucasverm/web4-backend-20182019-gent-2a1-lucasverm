using System.ComponentModel.DataAnnotations;

namespace BijenkastApi.DTOs
{
    public class BijenkastDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
namespace Selu383.SP25.Api
{
    public class Theater
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Name { get; set; }    

        [Required]
        public string Address { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int SeatCount { get; set; }
    }
}

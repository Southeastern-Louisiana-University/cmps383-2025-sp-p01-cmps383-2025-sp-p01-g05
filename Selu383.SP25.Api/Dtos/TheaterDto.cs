using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Selu383.SP25.Api.Dtos
{
    public class TheaterDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        [DefaultValue(1)]
        public int SeatCount { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace LAB5_RPBDIS.Models
{
    public class TrainType
    {
        public int TrainTypeId { get; set; }

        [Display(Name = "Тип поезда")]
        public string? TypeName { get; set; }

        public List<Train> Trains { get; set; } = new();
    }
}

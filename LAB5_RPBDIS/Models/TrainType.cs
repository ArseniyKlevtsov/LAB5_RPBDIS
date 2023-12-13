using System.ComponentModel.DataAnnotations;

namespace RailwayTrafficSolution.Models
{
    public class TrainType
    {
        public int TrainTypeId { get; set; }

        [Display(Name = "Тип поезда")]
        public string? TypeName { get; set; }

        public List<Train> Trains { get; set; } = new();
    }
}

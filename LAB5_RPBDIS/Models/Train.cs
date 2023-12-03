using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LAB5_RPBDIS.Models
{
    public class Train
    {
        public int TrainId { get; set; }

        [Display(Name = "Номер поезда")]
        public string? TrainNumber { get; set;}

        [Display(Name = "Дистанция всего пути")]
        public float? DistanceInKm { get; set; }

        [Display(Name = "Является брэндированным?")]
        public bool? IsBrandedTrain { get; set; }

        [Display(Name = "Тип поезда")]
        public int TrainTypeId { get; set; }

        [Display(Name = "Тип поезда")]
        public TrainType? TrainType { get; set; }

        public List<Stop> Stops { get; set; } = new();
        public List<Schedule> Schedules { get; set; } = new();

        public List<Employee> Employees { get; set; } = new();
        public List<TrainStaff> TrainStaffs { get; set; } = new();
    }
}

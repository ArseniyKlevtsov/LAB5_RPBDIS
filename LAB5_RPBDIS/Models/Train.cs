using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LAB5_RPBDIS.Models
{
    public class Train
    {
        public int TrainId { get; set; }
        public string? TrainNumber { get; set;}
        public float? DistanceInKm { get; set; }
        public bool? IsBrandedTrain { get; set; }

        public int TrainTypeId { get; set; }
        public TrainType? TrainType { get; set; }

        public List<Stop> Stops { get; set; } = new();
        public List<Schedule> Schedules { get; set; } = new();

        public List<Employee> Employees { get; set; } = new();
        public List<TrainStaff> TrainStaffs { get; set; } = new();
    }
}

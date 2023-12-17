using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RailwayTrafficSolution.Models
{
    [Display(Name = "Сотрудник")]
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Display(Name = "Имя сотрудника")]
        public string? EmployeeName { get; set; }

        [Display(Name = "Возраст")]
        public int? age { get; set; }

        [Display(Name = "Опыт работы")]
        public float? WorkExperience { get; set; }

        [Display(Name = "Должность")]
        public int PositionID { get; set; }
        [Display(Name = "Должность")]
        public Position? Position { get; set; }

        public List<Train> Trains { get; set; } = new();
        public List<TrainStaff> TrainStaffs { get; set; } = new();
    }
}

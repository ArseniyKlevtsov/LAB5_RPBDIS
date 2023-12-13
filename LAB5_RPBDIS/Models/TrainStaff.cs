using System.ComponentModel.DataAnnotations;

namespace RailwayTrafficSolution.Models
{
    public class TrainStaff
    {
        public int TrainStaffId { get; set; }

        [Display(Name = "День недели")]
        public int DayOfWeek { get; set; }

        [Display(Name = "Поезд")]
        public int TrainId { get; set; }

        [Display(Name = "Поезд")]
        public Train? Train { get; set; }

        [Display(Name = "Сотрудник")]
        public int EmployeeId { get; set; }
        [Display(Name = "Сотрудник")]
        public Employee? Employee { get; set; }

    }
}

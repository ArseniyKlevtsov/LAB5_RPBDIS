using Microsoft.AspNetCore.Mvc.Rendering;
using RailwayTrafficSolution.Models;

namespace RailwayTrafficSolution.ViewModels.TrainStuffViewModels
{
    public class TrainStuffFilterViewModel
    {
        public SelectList Trains { get; }
        public SelectList Employees { get; }
        public int SelectedTrain { get; }
        public int SelectedEmployee { get; }
        public TrainStuffFilterViewModel(List<Train> trains, int train, List<Employee> employees, int employee)
        {
            trains.Insert(0, new Train { TrainNumber = "Все", TrainId = 0 });
            employees.Insert(0, new Employee { EmployeeName = "Все", EmployeeId = 0 });
            Trains = new SelectList(trains, "TrainId", "TrainNumber", train);
            Employees = new SelectList(employees, "EmployeeId", "EmployeeName", employee);
            SelectedTrain = train;
            SelectedEmployee = employee;
        }
    }
}

using RailwayTrafficSolution.Models;

namespace RailwayTrafficSolution.ViewModels.EmployeeViewModels
{
    public class EmployeeIndexViewModel
    {
        public IEnumerable<Employee> Employees { get; }
        public PageViewModel PageViewModel { get; }
        public EmployeeFilterViewModel FilterViewModel { get; }
        public EmployeeSortViewModel SortViewModel { get; }
        public EmployeeIndexViewModel(IEnumerable<Employee> employees, PageViewModel pageViewModel,
            EmployeeFilterViewModel filterViewModel, EmployeeSortViewModel sortViewModel)
        {
            Employees = employees;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            SortViewModel = sortViewModel;
        }
    }
}

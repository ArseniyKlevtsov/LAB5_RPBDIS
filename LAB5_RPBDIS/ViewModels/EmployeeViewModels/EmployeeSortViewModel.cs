using RailwayTrafficSolution.Data;

namespace RailwayTrafficSolution.ViewModels.EmployeeViewModels
{
    public class EmployeeSortViewModel
    {
        public SortState NameSort { get; } 
        public SortState AgeSort { get; }    
        public SortState WorkExperienceSort { get; }   
        public SortState Current { get; }    

        public EmployeeSortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            AgeSort = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;
            WorkExperienceSort = sortOrder == SortState.WorkExperienceAsc ? SortState.WorkExperienceDesc : SortState.WorkExperienceAsc;
            Current = sortOrder;
        }
    }
}

using RailwayTrafficSolution.Data;

namespace RailwayTrafficSolution.ViewModels.PositionViewModels
{
    public class PositionSortViewModel
    {
        public SortState NameSort { get; }
        public SortState SalarySort { get; } 
        public SortState Current { get; }    

        public PositionSortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            SalarySort = sortOrder == SortState.SalaryAsc ? SortState.SalaryDesc : SortState.SalaryAsc;
            Current = sortOrder;
        }
    }
}

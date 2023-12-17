using RailwayTrafficSolution.Data;

namespace RailwayTrafficSolution.ViewModels.PositionViewModels
{
    public class PositionSortViewModel
    {
        public SortState SalarySort { get; } 
        public SortState Current { get; }    

        public PositionSortViewModel(SortState sortOrder)
        {
            SalarySort = sortOrder == SortState.SalaryAsc ? SortState.SalaryDesc : SortState.SalaryAsc;
            Current = sortOrder;
        }
    }
}

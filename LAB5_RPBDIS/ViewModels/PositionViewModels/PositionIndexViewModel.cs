using RailwayTrafficSolution.Models;
using RailwayTrafficSolution.ViewModels.EmployeeViewModels;

namespace RailwayTrafficSolution.ViewModels.PositionViewModels
{
    public class PositionIndexViewModel
    {
        
        public IEnumerable<Position> Positions { get; }
        public PageViewModel PageViewModel { get; }
        public PositionFilterViewModel FilterViewModel { get; }
        public PositionSortViewModel SortViewModel { get; }
        public PositionIndexViewModel(IEnumerable<Position> positions, PageViewModel pageViewModel,
            PositionFilterViewModel filterViewModel, PositionSortViewModel sortViewModel)
        {
            Positions = positions;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            SortViewModel = sortViewModel;
        }
    }
}

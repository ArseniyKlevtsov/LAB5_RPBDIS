using RailwayTrafficSolution.Models;

namespace RailwayTrafficSolution.ViewModels.StopViewModels
{
    public class StopIndexViewModel
    {
        public IEnumerable<Stop> Stops { get; }
        public PageViewModel PageViewModel { get; }
        public StopFilterViewModel FilterViewModel { get; }
        public StopSortViewModel SortViewModel { get; }
        public StopIndexViewModel(IEnumerable<Stop> stops, PageViewModel pageViewModel,
            StopFilterViewModel filterViewModel, StopSortViewModel sortViewModel)
        {
            Stops = stops;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            SortViewModel = sortViewModel;
        }
    }
}

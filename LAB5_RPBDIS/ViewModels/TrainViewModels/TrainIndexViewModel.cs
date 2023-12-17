using RailwayTrafficSolution.Models;
using RailwayTrafficSolution.ViewModels.TrainTypeViewModels;

namespace RailwayTrafficSolution.ViewModels.TrainViewModels
{
    public class TrainIndexViewModel
    {
        public IEnumerable<Train> Trains { get; }
        public PageViewModel PageViewModel { get; }
        public TrainFilterViewModel FilterViewModel { get; }
        public TrainSortViewModel SortViewModel { get; }
        public TrainIndexViewModel(IEnumerable<Train> trains, PageViewModel pageViewModel,
            TrainFilterViewModel filterViewModel, TrainSortViewModel sortViewModel)
        {
            Trains = trains;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            SortViewModel = sortViewModel;
        }
    }
}

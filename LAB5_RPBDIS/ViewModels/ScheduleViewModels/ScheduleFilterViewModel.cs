using Microsoft.AspNetCore.Mvc.Rendering;
using RailwayTrafficSolution.Models;

namespace RailwayTrafficSolution.ViewModels.ScheduleViewModels
{
    public class ScheduleFilterViewModel
    {
        public SelectList Trains { get; }
        public SelectList Stops { get; }
        public int SelectedTrain { get; }
        public int SelectedStop{ get; }
        public ScheduleFilterViewModel(List<Train> trains, int train, List<Stop> stops, int stop)
        {
            trains.Insert(0, new Train { TrainNumber = "Все", TrainId = 0 });
            stops.Insert(0, new Stop { Name = "Все", StopId = 0 });
            Trains = new SelectList(trains, "TrainId", "TrainNumber", train);
            Stops = new SelectList(stops, "StopId", "Name", stop);
            SelectedTrain = train;
            SelectedStop = stop;
        }
    }
}

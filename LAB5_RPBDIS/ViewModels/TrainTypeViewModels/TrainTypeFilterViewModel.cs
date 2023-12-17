using Microsoft.AspNetCore.Mvc.Rendering;
using RailwayTrafficSolution.Models;

namespace RailwayTrafficSolution.ViewModels.TrainTypeViewModels
{
    public class TrainTypeFilterViewModel
    {
        public string SelectedName { get; }

        public TrainTypeFilterViewModel(string name)
        {
            SelectedName = name;
        }
    }
}

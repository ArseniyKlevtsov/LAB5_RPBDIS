using Microsoft.AspNetCore.Mvc.Rendering;

namespace RailwayTrafficSolution.ViewModels.PositionViewModels
{
    public class PositionFilterViewModel
    {
        public string SelectedName { get; }
        public PositionFilterViewModel(string name)
        {
            SelectedName = name;
        }
    }
}

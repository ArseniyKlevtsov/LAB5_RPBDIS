using Microsoft.AspNetCore.Mvc.Rendering;
using RailwayTrafficSolution.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RailwayTrafficSolution.ViewModels.EmployeeViewModels
{
    public class EmployeeFilterViewModel
    {
        public SelectList Positions { get; } 
        public int SelectedPosition { get; } 
        public string SelectedName { get; } 
        public EmployeeFilterViewModel(List<Position> positions, int position, string name)
        {
            positions.Insert(0, new Position { PositionName = "Все", PositionId = 0 });
            Positions = new SelectList(positions, "PositionId", "PositionName", position);
            SelectedPosition = position;
            SelectedName = name;
        }
    }
}

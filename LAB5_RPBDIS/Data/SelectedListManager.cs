using Microsoft.AspNetCore.Mvc.Rendering;

namespace RailwayTrafficSolution.Data
{
    /// <summary>
    /// Класс для создания объектов SelectedList
    /// </summary>
    public class SelectedListManager
    {
        /// <summary>
        /// Возвращает SelectedList с 3 опциями типа <see cref="StringBool"/>: {0; "Все"},{1; "Да"},{2, "Нет"}
        /// </summary>
        /// <param name="selectedValue"> Id опции, которая должна быть выбрана</param>
        /// <returns>SelectedList с 3 опциями типа <see cref="StringBool"/>: {0; "Все"},{1; "Да"},{2, "Нет"}, где будет выбрана опция с id
        /// = selectedValue </returns>
        public SelectList GetBoolSelectedList(int selectedValue)
        {

            List<StringBool> values = new List<StringBool>()
            {
                new StringBool(0,"Все"),
                new StringBool(1,"Да"),
                new StringBool(2,"Нет")
            };

            return new SelectList(values, "Id", "Value", selectedValue);
        }
    }

    public class StringBool
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public StringBool(int id, string value) { Id = id; Value = value; }

    }
}

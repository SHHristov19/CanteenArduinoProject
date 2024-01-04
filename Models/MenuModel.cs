using CanteenArduinoProject.Data;

namespace CanteenArduinoProject.Models
{
    public class MenuModel
    {
        public List<Menu> menuForTheDay { get; set; }

        public Dictionary<string, string> dayOfTheWeek { get; set; }

        public string dateOfTheDay { get; set; }
    }
}

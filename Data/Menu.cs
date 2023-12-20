using System;
using System.Collections.Generic;

namespace CanteenArduinoProject.Data;

public partial class Menu
{
    public string MenuId { get; set; } = null!;

    public string? Appetizer { get; set; }

    public string? Main { get; set; }

    public string? Dessert { get; set; }

    public string? Day { get; set; }
}

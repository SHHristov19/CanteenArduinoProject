using System;
using System.Collections.Generic;

namespace CanteenArduinoProject.Data;

public partial class Usermenu
{
    public string UserId { get; set; } = null!;

    public string MenuId { get; set; } = null!;

    public DateTime? Date { get; set; }

    public virtual Menu Menu { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

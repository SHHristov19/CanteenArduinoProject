using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CanteenArduinoProject.Data;

public partial class User
{
    public string Id { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? FirstName { get; set; }

    public string LastName { get; set; } = null!;
}

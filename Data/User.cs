using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CanteenArduinoProject.Data;

public partial class User
{
    public string Id { get; set; } = null!;

    [Required(ErrorMessage = "Необходимо е да въведете потребителско име")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Необходимо е да въведете парола")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Необходимо е да въведете име")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Необходимо е да въведете фамилия")]
    public string LastName { get; set; } = null!;
}

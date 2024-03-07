using System.ComponentModel.DataAnnotations;

namespace BakeryLabb.Classes;

public class UserInformation
{
    public int Id { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    [Required]
    public string? Address { get; set; }

    [Required]
    public string? ZipCode { get; set; }

    [Required]
    public string? City { get; set; }
}

using System.ComponentModel.DataAnnotations;
using API;
public class RegisterDTO
{
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    [StringLength(8, MinimumLength =4)]
    public string Password { get; set; } = string.Empty;
}
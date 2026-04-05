using System.ComponentModel.DataAnnotations;

namespace Presentation.WebApp.Models.Authentication;

public class RegisterEmailForm
{
    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress(ErrorMessage = "Email address must be valid")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email Address", Prompt = "Enter Email Address")]
    public string Email { get; set; } = null!;

    [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms and conditions")]
    public bool TermsAndConditions { get; set; }
    public string? ErrorMessage { get; set; }
}

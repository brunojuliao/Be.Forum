using System.ComponentModel.DataAnnotations;

namespace Be.Forum.MVC.Models.ManageViewModels {
  public class IndexViewModel {
    public string Username { get; set; }

    public bool IsEmailConfirmed { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
    [DataType(DataType.Text)]
    public string Nickname { get; set; }

    public string StatusMessage { get; set; }
  }
}

using Microsoft.AspNetCore.Identity;

namespace Be.Forum.MVC.Models {
  // Add profile data for application users by adding properties to the ApplicationUser class
  public class ApplicationUser : IdentityUser {
    //
    // Summary:
    //     Gets or sets the NIckname for the user.
    public string Nickname { get; set; }
  }
}

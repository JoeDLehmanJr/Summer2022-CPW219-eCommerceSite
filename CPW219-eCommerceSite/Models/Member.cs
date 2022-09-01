using System.ComponentModel.DataAnnotations;

namespace CPW219_eCommerceSite.Models
{
    /// <summary>
    /// Represents a single member of the site
    /// </summary>
    public class Member
    {
        // The Key annotation is to mark this field as the primary field of the database.
        [Key]
        public int MemberId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? Phone { get; set; }

        public string? Username { get; set; }  
    }

    /// <summary>
    /// Represents the two required fields for the registration of the single member of the site
    /// </summary>
    public class RegisterViewModel 
    {
        // data annotations are used to validate the data and set limits on the data such as
        // limiting the length of the email address to 100 and marking the field as an email address
        // and the fields is required
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        // Here we are using the data annotation to set the fields as required and to set the
        // field as an email address comparer. Then we set the label to the field as confirm Email.
        [Required]
        [Compare(nameof(Email))]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }

        // Here we are using the data annotation to set the fields as required and to set the
        // length of the password to between 6 and 75 characters. Then we changed the field's
        // textBoxs mask field to an * by using the DataType.Password annotation.
        [Required]
        [StringLength(75, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Here we are using the data annotation to set the fields as required, and uses the
        // compare annotation to make sure that what is typed in this fields box is the same
        // as the Password field.
        [Required]
        [Compare(nameof(ConfirmPassword))]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        // data annotations are used to validate the data and set limits on the data such as
        // limiting the length of the email address to 100 characters and marking the field as an email address
        // and the fields is required
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        // Here the annotations marked the field as required, set the
        // length of the password to between 6 and 75 characters, and 
        //  we changed the field's textBoxs mask field to an * by
        //  using the DataType.Password annotation.
        [Required]
        [StringLength(75, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
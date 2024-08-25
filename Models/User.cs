using System.ComponentModel.DataAnnotations;

namespace asp.netDay2.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "user name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(12, 60, ErrorMessage = "age must be between 12 and 60")]
        public int Age { get; set; }
        [Required(AllowEmptyStrings = true)]
        [EmailAddress(ErrorMessage = "it doesn't match the email format")] // @.....com
        public string Email { get; set; }

        [MinLength(8)]
        //[RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$")]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [MaxLength(11)]
        public string Phone { get; set; }

        [Url] // HTTPS
        public string Website { get; set; }

       
    }
}

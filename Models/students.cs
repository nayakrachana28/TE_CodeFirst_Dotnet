using System.ComponentModel.DataAnnotations;

namespace TE_CodeFirst_Dotnet.Models
{
    public class students
    {
        [Key]
        public int StudentId { get; set; }


        [Required(ErrorMessage ="Enter Your Name")]
        public string StudentName { get; set; }


        [Required(ErrorMessage = "Enter Your Age")]
        [Range(21,30,ErrorMessage ="Your are not Eligible, the Age shouls be between 21 to 30")]
        public int StudentAge { get; set; }


        [DataType(DataType.PhoneNumber)]
        public string StudentMobile { get; set; }


        [DataType(DataType.EmailAddress)]
        public string StudentEmail { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Enter the Password")]
        public string StudentPassword { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter the Confirm Password")]
        [Compare("StudentPassword",ErrorMessage ="Password does not Match")]
        public string StudentConfirmPassword { get; set; }  
    }
}

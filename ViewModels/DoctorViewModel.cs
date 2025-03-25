using System.ComponentModel.DataAnnotations;

namespace easymed_mvc.ViewModels
{
    public class DoctorViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Specialty")]
        public string Specialty { get; set; }
    }
}
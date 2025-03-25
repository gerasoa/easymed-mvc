using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace easymed_mvc.ViewModels
{
    public class DoctorEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Specialty")]
        public string Specialty { get; set; }

        [Required]
        [Display(Name = "Status")]
        public DoctorStatus Status { get; set; }

        public IEnumerable<SelectListItem> StatusList { get; set; }
    }

     public class DoctorViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Specialty")]
        public string Specialty { get; set; }

        // [Required]
        // [Display(Name = "Status")]
        // public DoctorStatus Status { get; set; }

        // public IEnumerable<SelectListItem> StatusList { get; set; }
    }
}
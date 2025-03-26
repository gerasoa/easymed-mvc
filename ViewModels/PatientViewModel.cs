using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace easymed_mvc.ViewModels
{
    public class PatientViewModel
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public string FullName { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        // public string UserId { get; set; }
    }
}
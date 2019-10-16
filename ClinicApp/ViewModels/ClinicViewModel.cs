using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClinicApp.ViewModels
{
    public class ClinicViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Insert Arabic Name")]
        public string ArabicName { get; set; }
        [Required(ErrorMessage = "Please Insert English Name")]
        public string EnglishName { get; set; }
        public string Title { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
        //[Required(ErrorMessage = "Please Choose Department")]
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
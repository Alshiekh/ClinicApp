using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClinicApp.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Insert Arabic Name")]
        public string ArabicName { get; set; }
        [Required(ErrorMessage = "Please Insert English Name")]
        public string EnglishName { get; set; }
    }
}
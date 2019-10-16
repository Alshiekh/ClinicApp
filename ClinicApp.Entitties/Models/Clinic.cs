using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Entitties.Models
{
    public class Clinic
    {
        public int Id { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string  Title { get; set; }
        public string  PhoneNumber { get; set; }
        public bool   Status  { get; set; }
        public int?  DepartmentId { get; set; }
        public virtual Department Department { get; set; }

    }
}

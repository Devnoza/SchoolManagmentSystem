using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagmentSystem
{
    public class AddSubjectModel
    {
        [Required(ErrorMessage = "შეავსეთ ახალი საგანი")]
        public string Name { get; set; }

        [Required(ErrorMessage = "შეავსეთ კურსი")]
        public string Course { get; set; }

        [Required(ErrorMessage = "შეავსეთ მასწავლებელი")]
        public string Teacher { get; set; }
    }
}
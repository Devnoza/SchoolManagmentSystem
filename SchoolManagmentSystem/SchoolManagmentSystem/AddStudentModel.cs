using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagmentSystem
{
    public class AddStudentModel
    {
        [Required(ErrorMessage = "შეავსეთ სტუდენტის მომხ. სახელი")]
        public string Name { get; set; }

        [Required(ErrorMessage = "შეავსეთ კურსი")]
        public string Course { get; set; }
    }
}
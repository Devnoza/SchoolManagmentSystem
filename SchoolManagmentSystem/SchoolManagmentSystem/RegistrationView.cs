using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagmentSystem
{
    public class RegistrationView
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "შეავსეთ სახელი")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "შეავსეთ გვარი")]
        public string LastName { get; set; }

        [Required]
        public bool Gender { get; set; }
        [Required(ErrorMessage = "შეავსეთ მომხმარებელი")]
        public string Username { get; set; }
        [Required(ErrorMessage = "შეავსეთ პაროლი")]
        public string Password { get; set; }
        [Required(ErrorMessage = "შეავსეთ იმეილი")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "შეავსეთ წოდება")]
        public int Role { get; set; }

    }
}
using DBModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagmentSystem.Helper
{
    public class RegistrationObject
    {
        public List<SelectListItem> RoleList { get; set; }

        public RegistrationObject()
        {
            RoleList = new List<SelectListItem>();
            using (Context context = new Context())
            {
                foreach (var i in context.Roles)
                {
                    RoleList.Add(new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
                }
            }
        }
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(200)]
        public string Password { get; set; }

        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        public int RoleId { get; set; }
        public int? PersonId { get; set; }
        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(200)]
        public string LastName { get; set; }

        public bool Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public virtual Person Person { get; set; }

        public virtual Role Role { get; set; }
    }

}
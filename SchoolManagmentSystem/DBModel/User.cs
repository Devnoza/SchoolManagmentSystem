namespace DBModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "შეავსეთ მომხმარებელი")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "შეავსეთ პაროლი")]
        [StringLength(200)]
        public string Password { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        public int RoleId { get; set; }

        public int? PersonId { get; set; }

        public virtual Person Person { get; set; }

        public virtual Role Role { get; set; }
    }
}

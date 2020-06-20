namespace miniForo.Models.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Password")]
    public partial class Password
    {
        [Column("password")]
        [Required]
        [StringLength(50)]
        public string password1 { get; set; }

        [Required]
        [StringLength(20)]
        public string userId { get; set; }

        public int id { get; set; }

        public virtual User User { get; set; }
    }
}

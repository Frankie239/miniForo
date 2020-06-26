namespace miniForo.Models.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Security.Permissions;

    [Table("Entry")]
    public partial class Entry
    {
        [Required]
        public string text { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        [Required]
        [StringLength(20)]
        public string userId { get; set; }

        [Required]
        [StringLength(20)]
        public string title { get; set; }

        public int id { get; set; }

        public virtual User User { get; set; }

       
    }
}

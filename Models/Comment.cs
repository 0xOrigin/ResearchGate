using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Research_Gate.Models
{
    [Table("Comment")]
    public partial class Comment
    {
        [Key]
        private int Comment_id { get; set; }
        [Required]
        public int Author_id { get; set; }
        [Required]
        public int Paper_id { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Column("Comment")]
        [Required]
        public string Content { get; set; }

        public virtual Author Author { get; set; }

        public virtual Paper Paper { get; set; }
    }
}

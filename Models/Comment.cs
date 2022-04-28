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
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Author_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Paper_id { get; set; }

        public DateTime Time { get; set; }

        [Column("Comment")]
        [Required]
        public string Comment1 { get; set; }

        public virtual Author Author { get; set; }

        public virtual Paper Paper { get; set; }
    }
}

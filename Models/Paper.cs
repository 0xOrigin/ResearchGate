using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Research_Gate.Models
{

    [Table("Paper")]
    public partial class Paper
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Paper()
        {
            Comments = new HashSet<Comment>();
            Dislike = new HashSet<Author>();
            Like = new HashSet<Author>();
            Participation = new HashSet<Author>();
        }

        [Key]
        public int Paper_id { get; set; }

        [Required]
        [StringLength(300)]
        public string File { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Paper Title")]
        public string Title { get; set; }

        [Required]
        public DateTime Publish_date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Author> Dislike { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Author> Like { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Author> Participation { get; set; }
    }
}

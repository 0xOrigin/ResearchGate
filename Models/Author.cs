using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Research_Gate.Models
{
    [Table("Author")]
    public partial class Author
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Author()
        {
            Comments = new HashSet<Comment>();
            Dislike = new HashSet<Paper>();
            Like = new HashSet<Paper>();
            Participation = new HashSet<Paper>();
        }

        [Key]
        public int Author_id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "First name")]
        public string Fname { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Last name")]
        public string Lname { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Institution")]
        public string University { get; set; }

        [Required]
        [StringLength(20)]
        public string Department { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(11)]
        [Phone]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "Image")]
        public string Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paper> Dislike { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paper> Like { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Paper> Participation { get; set; }
    }
}

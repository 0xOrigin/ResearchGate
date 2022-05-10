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
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid E-mail address.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        [Required]
        [StringLength(11)]
        [Phone]
        [RegularExpression("[0-9]+", ErrorMessage = "Please enter a valid phone number.")]
        public string Mobile { get; set; }

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

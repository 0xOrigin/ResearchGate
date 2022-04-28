namespace Research_Gate.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ResearchgateDBContext : DbContext
    {
        public ResearchgateDBContext()
            : base("name=ResearchgateDBContext")
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Paper> Papers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(e => e.Dislike)
                .WithMany(e => e.Dislike)
                .Map(m => m.ToTable("Dislike").MapLeftKey("Author_id").MapRightKey("Paper_id"));

            modelBuilder.Entity<Author>()
                .HasMany(e => e.Like)
                .WithMany(e => e.Like)
                .Map(m => m.ToTable("Like").MapLeftKey("Author_id").MapRightKey("Paper_id"));

            modelBuilder.Entity<Author>()
                .HasMany(e => e.Participation)
                .WithMany(e => e.Participation)
                .Map(m => m.ToTable("Participation").MapLeftKey("Author_id").MapRightKey("Paper_id"));
        }
    }
}

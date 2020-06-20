namespace miniForo.Models.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BlogContext : DbContext
    {
        public BlogContext()
            : base("name=BlogContext")
        {
        }

        public virtual DbSet<Entry> Entry { get; set; }
        public virtual DbSet<Password> Password { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>()
                .Property(e => e.text)
                .IsUnicode(false);

            modelBuilder.Entity<Entry>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Entry>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Entry)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.userId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Password)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.userId)
                .WillCascadeOnDelete(false);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RS.Core.Domain.Entities;

namespace RS.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            #region Tables
            modelBuilder.Entity<Post>().ToTable("Posts");
            modelBuilder.Entity<Friendship>().ToTable("Friendships");
            modelBuilder.Entity<Comment>().ToTable("Comments");
            #endregion

            #region Primary Keys
            modelBuilder.Entity<Post>().HasKey(p => p.Id);
            modelBuilder.Entity<Friendship>().HasKey(f => f.Id);
            modelBuilder.Entity<Comment>().HasKey(c => c.Id);
            #endregion

            #region RelationShip
            modelBuilder.Entity<Comment>()
               .HasOne(c => c.Post)
               .WithMany(p => p.Comments)
               .HasForeignKey(c => c.PostID);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(cp => cp.Replies)
                .HasForeignKey(c => c.ParentCommentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostID);


            #region Property Configuration

            #region Post
            modelBuilder.Entity<Post>().
                Property(p => p.PublicationType)
                .IsRequired();

            modelBuilder.Entity<Post>().
               Property(p => p.PostDate)
               .IsRequired();
            #endregion

            #region Friend
            modelBuilder.Entity<Friendship>().
                Property(f => f.User_Id1)
                .IsRequired();

            modelBuilder.Entity<Friendship>().
               Property(f => f.User_Id2)
               .IsRequired();
            #endregion

            #region Comment
            modelBuilder.Entity<Comment>().
               Property(c => c.PostID)
               .IsRequired();

            modelBuilder.Entity<Comment>().
               Property(c => c.UserID)
               .IsRequired();
            #endregion
            #endregion
            #endregion
        }
    }
}

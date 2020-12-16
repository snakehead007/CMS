using CMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            ConfigureCourse(modelBuilder.Entity<Course>());
            ConfigureSubject(modelBuilder.Entity<Subject>());
            ConfigureUser(modelBuilder.Entity<User>());
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<User> Users { get; set; }
        
        private void ConfigureCourse(EntityTypeBuilder<Course> course)
        {
            //dbo.Courses
            course.ToTable("Courses").HasKey(x => x.CourseId);
            course.Property(x => x.CourseId).UseIdentityColumn();
            course.HasIndex(x => x.Name);
            course.HasMany(x => x.Subjects);
        }
        private void ConfigureSubject(EntityTypeBuilder<Subject> subject)
        {
            //dbo.Subject
            subject.ToTable("Subjects").HasKey(x => x.SubjectId);
            subject.Property(x => x.SubjectId).UseIdentityColumn();
        }

        private void ConfigureUser(EntityTypeBuilder<User> user)
        {
            //dbo.Users
            user.ToTable("Users").HasKey(x => x.Username);
            user.Property(x => x.Username).HasMaxLength(20);
            user.Property(x => x.PasswordHash).HasMaxLength(64);
            user.Property(x => x.Salt).HasMaxLength(32);
        }

    }
}

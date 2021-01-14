using CMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CMS.Data
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<AttachmentVersion> AttachmentVersions { get; set; }
        public virtual DbSet<CourseArchive> CourseArchive { get; set; }
        public virtual DbSet<SubjectArchive> SubjectArchive { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureCourse(modelBuilder.Entity<Course>());
            ConfigureSubject(modelBuilder.Entity<Subject>());
            ConfigureUser(modelBuilder.Entity<User>());
            ConfigureAttachment(modelBuilder.Entity<Attachment>());
            ConfigureAttachmentVersion(modelBuilder.Entity<AttachmentVersion>());
            ConfigureCourseArchive(modelBuilder.Entity<CourseArchive>());
            ConfigureSubjectArchive(modelBuilder.Entity<SubjectArchive>());
        }
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
            subject.HasMany(x => x.Attachments);
        }

        private void ConfigureUser(EntityTypeBuilder<User> user)
        {
            //dbo.Users
            user.ToTable("Users").HasKey(x => x.Username);
            user.Property(x => x.Username).HasMaxLength(20);
            user.Property(x => x.PasswordHash).HasMaxLength(64);
            user.Property(x => x.Salt).HasMaxLength(32);
        }
        private void ConfigureAttachment(EntityTypeBuilder<Attachment> attachment)
        {
            // dbo.Attachments
            attachment.ToTable("Attachments").HasKey(x => x.AttachmentId);
            attachment.Property(x => x.AttachmentId).UseIdentityColumn();
            attachment.HasOne(x => x.CurrentVersion);
            attachment.HasMany(x => x.Versions);
        }

        private void ConfigureAttachmentVersion(EntityTypeBuilder<AttachmentVersion> attachmentVersion)
        {
            // dbo.AttachmentVersion
            attachmentVersion.ToTable("AttachmentVersions").HasKey(x => x.AttachmentVersionId);
            attachmentVersion.Property(x => x.AttachmentVersionId).UseIdentityColumn();
        }
        private void ConfigureCourseArchive(EntityTypeBuilder<CourseArchive> course)
        {
            course.ToTable("CourseArchive").HasKey(x => x.CourseId);
            course.Property(x => x.CourseId).ValueGeneratedNever();
            course.HasMany(x => x.Subjects);
        }

        private void ConfigureSubjectArchive(EntityTypeBuilder<SubjectArchive> subject)
        {
            subject.ToTable("SubjectArchive").HasKey(x => x.SubjectId);
            subject.Property(x => x.SubjectId).ValueGeneratedNever();
        }
    }
}

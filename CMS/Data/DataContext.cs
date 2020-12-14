using CMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            ConfigureCourse(modelBuilder.Entity<Course>());
            ConfigureSubject(modelBuilder.Entity<Subject>());
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        
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
    }
}

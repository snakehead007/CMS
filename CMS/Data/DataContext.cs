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
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Course>().Property(x => x.CourseId).UseIdentityColumn().ValueGeneratedOnAdd();
            modelBuilder.Entity<Course>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Course>().Property(x => x.Code).IsRequired();
        }

        public virtual DbSet<Course> Courses { get; set; }

        private void ConfigureCourse(EntityTypeBuilder<Course> course)
        {
            //dbo.Courses
            course.ToTable("Courses").HasKey(x => x.CourseId);
            course.Property(x => x.CourseId).UseIdentityColumn();
            course.HasIndex(x => x.Name);
        }
    }
}

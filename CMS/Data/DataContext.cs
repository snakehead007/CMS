using CMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Data
{
    public class DataContext : DbContext
    {
        
        public virtual DbSet<Course> Courses { get; set; }
    }
}

using JWTAuthWebApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthWebApi
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Standards> Standards { get; set; }

        public SchoolContext() : base()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<Student>().Property<DateTime>("UpdatedDate");
            modelBuilder.Entity<Student>().Property<int>("CreatedBy");
            modelBuilder.Entity<Student>().Property<int>("UpdatedBy");


            modelBuilder.Entity<Grade>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<Grade>().Property<DateTime>("UpdatedDate");
            modelBuilder.Entity<Grade>().Property<int>("CreatedBy");
            modelBuilder.Entity<Grade>().Property<int>("UpdatedBy");


            modelBuilder.Entity<Course>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<Course>().Property<DateTime>("UpdatedDate");
            modelBuilder.Entity<Course>().Property<int>("CreatedBy");
            modelBuilder.Entity<Course>().Property<int>("UpdatedBy");


            modelBuilder.Entity<Standards>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<Standards>().Property<DateTime>("UpdatedDate");
            modelBuilder.Entity<Standards>().Property<int>("CreatedBy");
            modelBuilder.Entity<Standards>().Property<int>("UpdatedBy");

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Server=OM69\SQL2019;Database=SchoolDB;Trusted_Connection=True;");
        }
    }
}

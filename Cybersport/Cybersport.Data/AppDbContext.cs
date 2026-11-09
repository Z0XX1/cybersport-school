using System;
using Cybersport.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cybersport.Data;

public class AppDbContext : DbContext
{
    public DbSet<Students> Students => Set<Students>();
    public DbSet<Coaches> Coaches => Set<Coaches>();
    public DbSet<Lessons> Lessons => Set<Lessons>();

    public static string DbPath
    {
        get
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            Directory.CreateDirectory(folder);
            return Path.Combine(folder, "cyberboost.db");
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lessons>()
            .HasOne(lesson => lesson.Student)
            .WithMany(student => student.Lessons)
            .HasForeignKey(lesson => lesson.StudentId);

        modelBuilder.Entity<Lessons>()
            .HasOne(lesson => lesson.Coach)
            .WithMany(coach => coach.Lessons)
            .HasForeignKey(lesson => lesson.CoachId);
    }
}
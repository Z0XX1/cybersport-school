using System;
using Cybersport.Data.Models;

namespace Cybersport.Data;

public static class DbSeed
{
    public static void Seed(AppDbContext db)
    {
        if (db.Students.Any() ||
            db.Coaches.Any() ||
            db.Lessons.Any())
        {
            return;
        }

        var students = new List<Students>
        {
            new Students
            {
                FullName = "Губин Илья",
                Nickname = "Kaser",
                MainGame = "CS2",
                Rank = "8 LVL FACEIT",
                EnrollmentDate = new DateTime(2026, 11, 1)
            },
            new Students
            {
                FullName = "Анна Тротликова",
                Nickname = "Anchouu",
                MainGame = "Valorant",
                Rank = "Diamond 1",
                EnrollmentDate = new DateTime(2026, 11, 3)
            },
            new Students
            {
                FullName = "Владислав Новиков",
                MainGame = "Dota 2",
                Rank = "Divine",
                EnrollmentDate = new DateTime(2026, 11, 5)
            },
            new Students
            {
                FullName = "Сергей Мастеров",
                Nickname = "qCold",
                MainGame = "CS2",
                EnrollmentDate = new DateTime(2026, 11, 7)
            }
        };

        var coaches = new List<Coaches>
        {
            new Coaches
            {
                FullName = "Сергей Шаваев",
                Game = "CS2",
                ExperienceYears = 5,
                IsActive = true
            },
            new Coaches
            {
                FullName = "Джейкоб Харрис",
                Game = "Valorant",
                ExperienceYears = 4,
                IsActive = true
            },
            new Coaches
            {
                FullName = "Михаил Агатов",
                Game = "Dota 2",
                ExperienceYears = 3,
                IsActive = true
            }
        };

        db.Students.AddRange(students);
        db.Coaches.AddRange(coaches);
        db.SaveChanges();

        db.Lessons.AddRange(
            new Lessons
            {
                StudentId = students[0].Id,
                CoachId = coaches[0].Id,
                StartDate = new DateTime(2026, 11, 9, 15, 0, 0),
                DurationMinutes = 60,
                IsCompleted = false
            },
            new Lessons
            {
                StudentId = students[1].Id,
                CoachId = coaches[1].Id,
                StartDate = new DateTime(2026, 11, 9, 17, 0, 0),
                DurationMinutes = 90,
                IsCompleted = false
            },
            new Lessons
            {
                StudentId = students[2].Id,
                CoachId = coaches[2].Id,
                StartDate = new DateTime(2026, 11, 10, 16, 0, 0),
                DurationMinutes = 60,
                IsCompleted = false
            },
            new Lessons
            {
                StudentId = students[3].Id,
                CoachId = coaches[0].Id,
                StartDate = new DateTime(2026, 11, 11, 18, 0, 0),
                DurationMinutes = 60,
                IsCompleted = false
            },
            new Lessons
            {
                StudentId = students[0].Id,
                CoachId = coaches[1].Id,
                StartDate = new DateTime(2026, 11, 12, 14, 0, 0),
                DurationMinutes = 45,
                IsCompleted = false
            }
        );
        db.SaveChanges();
    }
}
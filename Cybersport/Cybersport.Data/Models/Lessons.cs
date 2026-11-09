using System;

namespace Cybersport.Data.Models;

public class Lessons
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CoachId { get; set; }
    public DateTime StartDate { get; set; }
    public int DurationMinutes { get; set; }
    public bool IsCompleted { get; set; }

    public Students Student { get; set; } = null!;
    public Coaches Coach { get; set; } = null!;

    public override string ToString()
    {
        return $"{StartDate:dd.MM.yyyy HH:mm}: " + $"Игрок {Student.FullName} - Тренер {Coach.FullName}, " +
               $"{DurationMinutes} минут, " + $"проведено: {(IsCompleted ? "да" : "нет")}";
    }
}
using System;

namespace Cybersport.Data.Models;

public class Coaches
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Game { get; set; } = "";
    public int ExperienceYears { get; set; }
    public bool IsActive { get; set; }

    public List<Lessons> Lessons { get; set; } = new();

    public override string ToString()
    {
        return $"{FullName}, {Game}, " + $"Опыт: {ExperienceYears} лет, " + $"Работает: {(IsActive ? "да" : "нет")}";
    }
}
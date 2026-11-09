using System;

namespace Cybersport.Data.Models;

public class Students
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string? Nickname { get; set; }
    public string MainGame { get; set; } = "";
    public string? Rank { get; set; }
    public DateTime EnrollmentDate { get; set; }

    public List<Lessons> Lessons { get; set; } = new();

    public override string ToString()
    {
        return $"{FullName} ({Nickname ?? "без ника"}), " + $"{MainGame}, ранг/эло: {Rank ?? "не указан"}";
    }
}
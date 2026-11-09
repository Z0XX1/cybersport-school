using System;
using System.Text;
using Cybersport.Data;
using Microsoft.EntityFrameworkCore;

Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine($"Путь к БД: {AppDbContext.DbPath}");

using var db = new AppDbContext();
db.Database.EnsureCreated();
DbSeed.Seed(db);

Console.WriteLine("\nУченики:");
foreach (var student in db.Students.OrderBy(x => x.Id))
{
    Console.WriteLine(student);
}

Console.WriteLine("\nТренеры:");
foreach (var coach in db.Coaches.OrderBy(x => x.Id))
{
    Console.WriteLine(coach);
}

Console.WriteLine("\nЗанятия:");
var lessons = db.Lessons.Include(x => x.Student).Include(x => x.Coach).OrderBy(x => x.StartDate);
foreach (var lesson in lessons)
{
    Console.WriteLine(lesson);
}

Console.WriteLine($"\nВсего учеников: {db.Students.Count()}");
Console.WriteLine($"Всего тренеров: {db.Coaches.Count()}");
Console.WriteLine($"Всего занятий: {db.Lessons.Count()}");
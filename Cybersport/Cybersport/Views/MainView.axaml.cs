using System.Linq;
using Avalonia.Controls;
using Cybersport.Data;
using Microsoft.EntityFrameworkCore;

namespace Cybersport.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        using var db = new AppDbContext();
        db.Database.EnsureCreated();
        DbSeed.Seed(db);

        StudentsList.ItemsSource = db.Students
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToList();

        CoachesList.ItemsSource = db.Coaches
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToList();

        LessonsList.ItemsSource = db.Lessons
            .AsNoTracking()
            .Include(x => x.Student)
            .Include(x => x.Coach)
            .OrderBy(x => x.StartDate)
            .ToList();
    }
}
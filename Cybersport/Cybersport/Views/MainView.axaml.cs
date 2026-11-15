using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Cybersport.Data;
using Cybersport.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cybersport.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        using (var db = new AppDbContext())
        {
            bool created = db.Database.EnsureCreated();
            if (created)
                DbSeed.Seed(db);
        }
        RefreshLists();
    }

    private void RefreshLists()
    {
        using var db = new AppDbContext();
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

    private bool ReadStudentInput(out Students student)
    {
        student = new Students();
        string name = StudentName.Text?.Trim() ?? "";
        string game = StudentGame.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(game))
        {
            StudentMessage.Text = "Заполните ФИО и игру";
            return false;
        }

        if (StudentDate.SelectedDate is not DateTime date)
        {
            StudentMessage.Text = "Выберите дату записи";
            return false;
        }

        student.FullName = name;
        student.MainGame = game;
        student.Nickname = string.IsNullOrWhiteSpace(StudentNickname.Text) ? null : StudentNickname.Text.Trim();
        student.Rank = string.IsNullOrWhiteSpace(StudentRank.Text) ? null : StudentRank.Text.Trim();
        student.EnrollmentDate = date.Date;

        return true;
    }

    private void AddStudentClick(object? sender, RoutedEventArgs e)
    {
        if (!ReadStudentInput(out Students student))
            return;

        try
        {
            using var db = new AppDbContext();
            db.Students.Add(student);
            db.SaveChanges();
            RefreshLists();
            ClearStudentForm();
            StudentMessage.Text = "Ученик добавлен";
        }
        catch (Exception ex)
        {
            StudentMessage.Text = "Ошибка: " + ex.Message;
        }
    }

    private void ClearStudentForm()
    {
        StudentsList.SelectedItem = null;
        StudentName.Text = "";
        StudentNickname.Text = "";
        StudentGame.Text = "";
        StudentRank.Text = "";
        StudentDate.SelectedDate = null;
    }

    private void SaveStudentClick(object? sender, RoutedEventArgs e)
    {
        if (StudentsList.SelectedItem is not Students selected)
        {
            StudentMessage.Text = "Сначала выберите ученика";
            return;
        }

        if (!ReadStudentInput(out Students input))
            return;

        try
        {
            using var db = new AppDbContext();

            var student = db.Students.Find(selected.Id);

            if (student == null)
            {
                StudentMessage.Text = "Ученик не найден";
                return;
            }

            student.FullName = input.FullName;
            student.Nickname = input.Nickname;
            student.MainGame = input.MainGame;
            student.Rank = input.Rank;
            student.EnrollmentDate = input.EnrollmentDate;
            db.SaveChanges();

            RefreshLists();
            ClearStudentForm();
            StudentMessage.Text = "Изменения сохранены";
        }
        catch (Exception ex)
        {
            StudentMessage.Text = "Ошибка: " + ex.Message;
        }
    }

    private void DeleteStudentClick(object? sender, RoutedEventArgs e)
    {
        if (StudentsList.SelectedItem is not Students selected)
        {
            StudentMessage.Text = "Сначала выберите ученика";
            return;
        }

        try
        {
            using var db = new AppDbContext();
            var student = db.Students.Find(selected.Id);

            if (student == null)
            {
                StudentMessage.Text = "Ученик не найден";
                return;
            }

            db.Lessons.RemoveRange(db.Lessons.Where(x => x.StudentId == student.Id));
            db.Students.Remove(student);
            db.SaveChanges();

            RefreshLists();
            ClearStudentForm();
            StudentMessage.Text = "Ученик и связанные с ним занятия удалены";
        }
        catch (Exception ex)
        {
            StudentMessage.Text = "Ошибка: " + ex.Message;
        }
    }

    private void StudentsListSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (StudentsList.SelectedItem is not Students student)
            return;

        StudentName.Text = student.FullName;
        StudentNickname.Text = student.Nickname;
        StudentGame.Text = student.MainGame;
        StudentRank.Text = student.Rank;
        StudentDate.SelectedDate = student.EnrollmentDate;
    }
}
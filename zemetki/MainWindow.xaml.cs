using zemetki.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace zemetki
{
    public partial class MainWindow : Window
    {
        private BindingList<NoteModel> noteModels;

        public MainWindow()
        {
            InitializeComponent();

            noteModels = FileManager.ReadFromFile("zametka.json");

            if (noteModels == null)
            {
                noteModels = new BindingList<NoteModel>();
            }

            Date.SelectedDate = DateTime.Now;
            ShowNotes();
        }

        private void ShowNotes()
        {
            BindingList<NoteModel> FoundNotes = new BindingList<NoteModel>();
            foreach (NoteModel note in noteModels)
            {
                if (note.date == Date.SelectedDate)
                {
                    FoundNotes.Add(note);
                }
                Notes.ItemsSource = FoundNotes;
            }
            FileManager.SaveToFile(noteModels, "zametka.json");
        }

        private void Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowNotes();
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            noteModels.Add(new NoteModel() { Text = "", Title = "", date = Date.SelectedDate.Value });

            ShowNotes();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (NoteModel note in noteModels)
            {
                if (note == Notes.SelectedItem)
                {
                    noteModels.Remove(note);
                    break;
                }
            }
            ShowNotes();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowNotes();
            Application.Current.MainWindow.Close();
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void FullScreenBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
        }
    }
}
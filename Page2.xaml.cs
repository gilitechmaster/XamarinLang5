using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using lang6.Model;

namespace lang6
{
    //Page2 문서 목록 화면
    public partial class Page2 : ContentPage
    {
        public Page2()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var notes = new List<Note>();
            var files = Directory.EnumerateFiles(App.FolderPath, "*.notes.txt");
            foreach (var filename in files)
            {
                notes.Add(new Note
                {
                    Filename = filename,
                    Text = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename)
                });
            }
            리스트뷰.ItemsSource = notes
                .OrderBy(d => d.Date)
                .ToList();

        }
        async void OnNoteAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page1
            {
                BindingContext = new Note()
            });
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new Page1
                {
                    BindingContext = e.SelectedItem as Note
                });
            }
        }
    }
}
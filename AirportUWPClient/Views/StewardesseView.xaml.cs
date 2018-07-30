using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AirportUWPClient.Models;
using AirportUWPClient.Services;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace AirportUWPClient.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class StewardesseView : Page
    {
        public ObservableCollection<Stewardesse> Stewardesses { get; set; }
        public Stewardesse SelectedStewardesse { get; set; }
        private readonly StewardessesDataLoadService _service;

        public StewardesseView()
        {
            this.InitializeComponent();

            Stewardesses = new ObservableCollection<Stewardesse>();
            SelectedStewardesse = new Stewardesse();
            _service = new StewardessesDataLoadService();
            StewardessesList.ItemsSource = Stewardesses;
            AddButton.Visibility = Visibility.Collapsed;
            SaveButton.Visibility = Visibility.Collapsed;
        }

        public async void LoadData(object sender, RoutedEventArgs e)
        {
            if (SaveButton.Visibility == Visibility.Visible)
            {
                SaveButton.Visibility = Visibility.Collapsed;
            }

            if (AddButton.Visibility == Visibility.Visible)
            {
                AddButton.Visibility = Visibility.Collapsed;
            }

            Stewardesses.Clear();
            foreach (var airplaneType in await _service.LoadData())
            {
                Stewardesses.Add(airplaneType);
            }
        }

        public void OnSelectedItem(object sender, RoutedEventArgs e)
        {
            SelectedStewardesse = GetSelected(sender, e);

            if (SelectedStewardesse != null)
            {
                TextId.Text = SelectedStewardesse.Id.ToString();
                TextFirstName.Text = SelectedStewardesse.FirstName;
                TextLastName.Text = SelectedStewardesse.LastName;
                TextBirthdate.Text = SelectedStewardesse.Birthdate.ToString();
            }
        }

        public void Create(object sender, RoutedEventArgs e)
        {
            if (TextId.IsReadOnly)
            {
                TextId.IsReadOnly = false;
            }

            TextId.Text = "";
            TextId.IsReadOnly = true;
            TextFirstName.Text = "";
            TextLastName.Text = "";
            TextBirthdate.Text = "";

            if (SaveButton.Visibility == Visibility.Visible)
            {
                SaveButton.Visibility = Visibility.Collapsed;
            }

            if (AddButton.Visibility == Visibility.Collapsed)
            {
                AddButton.Visibility = Visibility.Visible;
            }
        }

        public async void Add(object sender, RoutedEventArgs e)
        {
            SelectedStewardesse.Id = 0;
            SelectedStewardesse.FirstName = TextFirstName.Text;
            SelectedStewardesse.LastName = TextLastName.Text;
            SelectedStewardesse.Birthdate = DateTime.Parse(TextBirthdate.Text);

            await _service.Create(SelectedStewardesse);

            AddButton.Visibility = Visibility.Collapsed;
            TextId.IsReadOnly = false;

            // refresh listView
            Stewardesses.Clear();
            foreach (var airplaneType in await _service.LoadData())
            {
                Stewardesses.Add(airplaneType);
            }
        }

        public async void Delete(object sender, RoutedEventArgs e)
        {
            if (SaveButton.Visibility == Visibility.Visible)
            {
                SaveButton.Visibility = Visibility.Collapsed;
            }

            if (AddButton.Visibility == Visibility.Visible)
            {
                AddButton.Visibility = Visibility.Collapsed;
            }

            try
            {
                if (TextId.IsReadOnly)
                {
                    TextId.IsReadOnly = false;
                }

                await _service.Delete(SelectedStewardesse.Id.ToString());

                // refresh listView
                Stewardesses.Clear();
                foreach (var airplaneType in await _service.LoadData())
                {
                    Stewardesses.Add(airplaneType);
                }
            }
            catch (Exception)
            {

            }
        }

        public async void Update(object sender, RoutedEventArgs e)
        {
            TextId.IsReadOnly = true;

            if (SaveButton.Visibility == Visibility.Collapsed)
            {
                SaveButton.Visibility = Visibility.Visible;
            }

            if (AddButton.Visibility == Visibility.Visible)
            {
                AddButton.Visibility = Visibility.Collapsed;
            }
        }

        public async void Save(object sender, RoutedEventArgs e)
        {
            SelectedStewardesse.FirstName = TextFirstName.Text;
            SelectedStewardesse.LastName = TextLastName.Text;
            SelectedStewardesse.Birthdate = DateTime.Parse(TextBirthdate.Text);

            var id = SelectedStewardesse.Id;
            SelectedStewardesse.Id = 0;

            await _service.Update(id.ToString(), SelectedStewardesse);

            TextId.IsReadOnly = false;
            SaveButton.Visibility = Visibility.Collapsed;

            // refresh listView
            Stewardesses.Clear();
            foreach (var airplaneType in await _service.LoadData())
            {
                Stewardesses.Add(airplaneType);
            }
        }

        private Stewardesse GetSelected(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;
            return (Stewardesse)listView.SelectedItem;
        }
    }
}

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
    public sealed partial class PilotView : Page
    {
        public ObservableCollection<Pilot> Pilots { get; set; }
        public Pilot SelectedPilot { get; set; }
        private readonly PilotsDataLoadService _service;

        public PilotView()
        {
            this.InitializeComponent();

            Pilots = new ObservableCollection<Pilot>();
            SelectedPilot = new Pilot();
            _service = new PilotsDataLoadService();
            PilotsList.ItemsSource = Pilots;
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

            Pilots.Clear();
            foreach (var airplaneType in await _service.LoadData())
            {
                Pilots.Add(airplaneType);
            }
        }

        public void OnSelectedItem(object sender, RoutedEventArgs e)
        {
            SelectedPilot = GetSelected(sender, e);

            if (SelectedPilot != null)
            {
                TextId.Text = SelectedPilot.Id.ToString();
                TextFirstName.Text = SelectedPilot.FirstName;
                TextLastName.Text = SelectedPilot.LastName;
                TextBirthdate.Text = SelectedPilot.Birthdate.ToString();
                TextExperience.Text = SelectedPilot.Experience.ToString();
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
            TextExperience.Text = "";

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
            SelectedPilot.Id = 0;
            SelectedPilot.FirstName = TextFirstName.Text;
            SelectedPilot.LastName = TextLastName.Text;
            SelectedPilot.Birthdate = DateTime.Parse(TextBirthdate.Text);
            SelectedPilot.Experience = int.Parse(TextExperience.Text);

            await _service.Create(SelectedPilot);

            AddButton.Visibility = Visibility.Collapsed;
            TextId.IsReadOnly = false;

            // refresh listView
            Pilots.Clear();
            foreach (var airplaneType in await _service.LoadData())
            {
                Pilots.Add(airplaneType);
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

                await _service.Delete(SelectedPilot.Id.ToString());

                // refresh listView
                Pilots.Clear();
                foreach (var airplaneType in await _service.LoadData())
                {
                    Pilots.Add(airplaneType);
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
            SelectedPilot.FirstName = TextFirstName.Text;
            SelectedPilot.LastName = TextLastName.Text;
            SelectedPilot.Birthdate = DateTime.Parse(TextBirthdate.Text);
            SelectedPilot.Experience = int.Parse(TextExperience.Text);

            var id = SelectedPilot.Id;
            SelectedPilot.Id = 0;

            await _service.Update(id.ToString(), SelectedPilot);

            TextId.IsReadOnly = false;
            SaveButton.Visibility = Visibility.Collapsed;

            // refresh listView
            Pilots.Clear();
            foreach (var airplaneType in await _service.LoadData())
            {
                Pilots.Add(airplaneType);
            }
        }

        private Pilot GetSelected(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;
            return (Pilot)listView.SelectedItem;
        }
    }
}

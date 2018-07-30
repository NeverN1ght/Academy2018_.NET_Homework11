using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
using System.Threading;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace AirportUWPClient.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class AirplaneTypeView : Page
    {
        public ObservableCollection<AirplaneType> AirplaneTypes { get; set; }
        public AirplaneType SelectedAirplaneType { get; set; }
        private readonly AirplaneTypesDataLoadService _service;

        public AirplaneTypeView()
        {
            this.InitializeComponent();

            AirplaneTypes = new ObservableCollection<AirplaneType>();
            SelectedAirplaneType = new AirplaneType();
            _service = new AirplaneTypesDataLoadService();
            AirplaneTypesList.ItemsSource = AirplaneTypes;
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

            AirplaneTypes.Clear();
            foreach (var airplaneType in await _service.LoadData())
            {
                AirplaneTypes.Add(airplaneType);
            }
        }

        public void OnSelectedItem(object sender, RoutedEventArgs e)
        {
            SelectedAirplaneType = GetSelected(sender, e);

            if (SelectedAirplaneType != null)
            {
                TextId.Text = SelectedAirplaneType.Id.ToString();
                TextAirplaneModel.Text = SelectedAirplaneType.AirplaneModel;
                TextSeatsCount.Text = SelectedAirplaneType.SeatsCount.ToString();
                TextCarryingCapacity.Text = SelectedAirplaneType.CarryingCapacity.ToString();
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
            TextAirplaneModel.Text = "";
            TextSeatsCount.Text = "";
            TextCarryingCapacity.Text = "";

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
            SelectedAirplaneType.Id = 0;
            SelectedAirplaneType.AirplaneModel = TextAirplaneModel.Text;
            SelectedAirplaneType.SeatsCount = int.Parse(TextSeatsCount.Text);
            SelectedAirplaneType.CarryingCapacity = int.Parse(TextCarryingCapacity.Text);

            await _service.Create(SelectedAirplaneType);

            AddButton.Visibility = Visibility.Collapsed;
            TextId.IsReadOnly = false;

            // refresh listView
            AirplaneTypes.Clear();
            foreach (var airplaneType in await _service.LoadData())
            {
                AirplaneTypes.Add(airplaneType);
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

                await _service.Delete(SelectedAirplaneType.Id.ToString());

                // refresh listView
                AirplaneTypes.Clear();
                foreach (var airplaneType in await _service.LoadData())
                {
                    AirplaneTypes.Add(airplaneType);
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
            SelectedAirplaneType.AirplaneModel = TextAirplaneModel.Text;
            SelectedAirplaneType.SeatsCount = int.Parse(TextSeatsCount.Text);
            SelectedAirplaneType.CarryingCapacity = int.Parse(TextCarryingCapacity.Text);

            var id = SelectedAirplaneType.Id;
            SelectedAirplaneType.Id = 0;

            await _service.Update(id.ToString(), SelectedAirplaneType);

            TextId.IsReadOnly = false;
            SaveButton.Visibility = Visibility.Collapsed;

            // refresh listView
            AirplaneTypes.Clear();
            foreach (var airplaneType in await _service.LoadData())
            {
                AirplaneTypes.Add(airplaneType);
            }
        }

        private AirplaneType GetSelected(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;
            return (AirplaneType)listView.SelectedItem;
        }
    }
}

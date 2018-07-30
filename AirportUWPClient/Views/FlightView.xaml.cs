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
    public sealed partial class FlightView : Page
    {
        public ObservableCollection<Flight> Flights { get; set; }
        private readonly FlightsDataLoadService _service;

        public FlightView()
        {
            this.InitializeComponent();

            Flights = new ObservableCollection<Flight>();
            _service = new FlightsDataLoadService();
            FlightsList.ItemsSource = Flights;
        }

        public async void LoadData(object sender, RoutedEventArgs e)
        {
            Flights.Clear();
            foreach (var airplaneType in await _service.LoadData())
            {
                Flights.Add(airplaneType);
            }
        }
    }
}

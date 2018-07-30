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
    public sealed partial class DepartureView : Page
    {
        public ObservableCollection<Departure> Departures { get; set; }
        private readonly DeparturesDataLoadService _service;

        public DepartureView()
        {
            this.InitializeComponent();

            Departures = new ObservableCollection<Departure>();
            _service = new DeparturesDataLoadService();
            DeparturesList.ItemsSource = Departures;
        }

        public async void LoadData(object sender, RoutedEventArgs e)
        {
            Departures.Clear();
            foreach (var airplaneType in await _service.LoadData())
            {
                Departures.Add(airplaneType);
            }
        }
    }
}

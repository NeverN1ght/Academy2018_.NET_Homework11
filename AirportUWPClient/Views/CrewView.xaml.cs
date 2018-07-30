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
    public sealed partial class CrewView : Page
    {
        public ObservableCollection<Crew> Crews { get; set; }
        private readonly CrewsDataLoadService _service;

        public CrewView()
        {
            this.InitializeComponent();

            Crews = new ObservableCollection<Crew>();
            _service = new CrewsDataLoadService();
            CrewsList.ItemsSource = Crews;
        }

        public async void LoadData(object sender, RoutedEventArgs e)
        {
            Crews.Clear();
            foreach (var airplaneType in await _service.LoadData())
            {
                Crews.Add(airplaneType);
            }
        }
    }
}

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
    public sealed partial class TicketView : Page
    {
        public ObservableCollection<Ticket> Tickets { get; set; }
        public Ticket SelectedTicket { get; set; }
        private readonly TicketsDataLoadService _service;

        public TicketView()
        {
            this.InitializeComponent();

            Tickets = new ObservableCollection<Ticket>();
            SelectedTicket = new Ticket();
            _service = new TicketsDataLoadService();
            TicketsList.ItemsSource = Tickets;
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

            Tickets.Clear();
            foreach (var airplaneType in await _service.LoadData())
            {
                Tickets.Add(airplaneType);
            }
        }

        public void OnSelectedItem(object sender, RoutedEventArgs e)
        {
            SelectedTicket = GetSelected(sender, e);

            if (SelectedTicket != null)
            {
                TextId.Text = SelectedTicket.Id.ToString();
                TextPrice.Text = SelectedTicket.Price.ToString();
                TextFlightNumber.Text = SelectedTicket.FlightNumber;
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
            TextPrice.Text = "";
            TextFlightNumber.Text = "";

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
            SelectedTicket.Id = 0;
            SelectedTicket.Price = decimal.Parse(TextPrice.Text);
            SelectedTicket.FlightNumber = TextFlightNumber.Text;


            await _service.Create(SelectedTicket);

            AddButton.Visibility = Visibility.Collapsed;
            TextId.IsReadOnly = false;

            // refresh listView
            Tickets.Clear();
            foreach (var airplaneType in await _service.LoadData())
            {
                Tickets.Add(airplaneType);
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

                await _service.Delete(SelectedTicket.Id.ToString());

                // refresh listView
                Tickets.Clear();
                foreach (var airplaneType in await _service.LoadData())
                {
                    Tickets.Add(airplaneType);
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
            SelectedTicket.Price = decimal.Parse(TextPrice.Text);
            SelectedTicket.FlightNumber = TextFlightNumber.Text;

            var id = SelectedTicket.Id;
            SelectedTicket.Id = 0;

            await _service.Update(id.ToString(), SelectedTicket);

            TextId.IsReadOnly = false;
            SaveButton.Visibility = Visibility.Collapsed;

            // refresh listView
            Tickets.Clear();
            foreach (var airplaneType in await _service.LoadData())
            {
                Tickets.Add(airplaneType);
            }
        }

        private Ticket GetSelected(object sender, RoutedEventArgs e)
        {
            var listView = (ListView)sender;
            return (Ticket)listView.SelectedItem;
        }
    }
}

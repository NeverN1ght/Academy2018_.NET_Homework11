using System;
using System.Collections.Generic;
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
using AirportUWPClient.Views;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace AirportUWPClient
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            myFrame.Navigate(typeof(AirplaneView));
            TitleTextBlock.Text = "Airplanes";
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AirplaneView.IsSelected)
            {
                myFrame.Navigate(typeof(AirplaneView));
                TitleTextBlock.Text = "Airplanes";
            }
            else if (AirplaneTypeView.IsSelected)
            {
                myFrame.Navigate(typeof(AirplaneTypeView));
                TitleTextBlock.Text = "Airplane types";
            }
            else if (PilotView.IsSelected)
            {
                myFrame.Navigate(typeof(PilotView));
                TitleTextBlock.Text = "Pilots";
            }
            else if (StewardesseView.IsSelected)
            {
                myFrame.Navigate(typeof(StewardesseView));
                TitleTextBlock.Text = "Stewardesses";
            }
            else if (CrewView.IsSelected)
            {
                myFrame.Navigate(typeof(CrewView));
                TitleTextBlock.Text = "Crews";
            }
            else if (FlightView.IsSelected)
            {
                myFrame.Navigate(typeof(FlightView));
                TitleTextBlock.Text = "Flights";
            }
            else if (DepartureView.IsSelected)
            {
                myFrame.Navigate(typeof(DepartureView));
                TitleTextBlock.Text = "Departures";
            }
            else if (TicketView.IsSelected)
            {
                myFrame.Navigate(typeof(TicketView));
                TitleTextBlock.Text = "Tickets";
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            mySplitView.IsPaneOpen = !mySplitView.IsPaneOpen;
        }
    }
}

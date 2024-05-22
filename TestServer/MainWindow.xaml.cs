﻿using Communication.Bluetooth;
using InTheHand.Net.Bluetooth;
using System.Text;
using System.Windows;

namespace TestServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BluetoothClassic_Server _BluetoothClassic_Server = new();
        int _clientId;
        public MainWindow()
        {
            InitializeComponent();
            BluetoothRadio.Default.Mode = RadioMode.Connectable;
            _BluetoothClassic_Server.OnReceiveOriginalDataFromClient += _BluetoothClassic_Server_OnReceiveOriginalDataFromClient;
        }

        private async Task _BluetoothClassic_Server_OnReceiveOriginalDataFromClient(byte[] data, int size, int clientId)
        {
            _clientId = clientId;

            await Dispatcher.InvokeAsync(() =>
            {
                TextBox_Receive.Text += Encoding.UTF8.GetString(data, 0, size);
            });
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await _BluetoothClassic_Server.StartAsync();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await _BluetoothClassic_Server.SendDataAsync(_clientId, Encoding.UTF8.GetBytes(TextBox_Send.Text));
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            await _BluetoothClassic_Server.StopAsync();
        }
    }
}
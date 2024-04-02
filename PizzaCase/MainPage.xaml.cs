﻿namespace PizzaCase
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public SocketTCP Tcp;
        public SocketUDP Udp;
        public byte[] data;
       
        public MainPage()
        {
            InitializeComponent();
            Tcp = new SocketTCP();
            Udp = new SocketUDP();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            udpset();

            //SemanticScreenReader.Announce(CounterBtn.Text);
        }


        public void tcpset()
        {
            Tcp.Connect("192.168.2.2");
            Tcp.Recieve(data);
            CounterBtn.Text = Tcp.decodeddata;
            Tcp.Close();
        }

        public void udpset()
        {
            Udp.Connect("192.168.2.2");
            Udp.Recieve(data);
            CounterBtn.Text = Udp.decodeddata;
            Udp.Close();
        }

    }

}

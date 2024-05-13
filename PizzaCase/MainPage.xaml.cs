using Microsoft.Maui.Animations;
using Microsoft.Maui.Dispatching;
using System.Runtime.Intrinsics.Arm;

namespace PizzaCase
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public byte[] data;


        public MainPage()
        {
            InitializeComponent();

        }

        private void OnudpbtnClicked(object sender, EventArgs e)
        {
            Thread tcp = new Thread(tcpset);
            Thread udp = new Thread(udpset);
            tcp.Start();
            udp.Start();

        }

        private void tick()
        {

                Thread udp = new Thread(udpset);
                udp.Start();

                Thread tcp = new Thread(tcpset);
                tcp.Start();

  
        }

        public void tcpset()
        {

            SocketTCP Tcp = new SocketTCP();
            Tcp.Connect("192.168.2.7", 12344);
            //Thread.Sleep(1000);
            Tcp.Recieve(data);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                text.Text = Tcp.decodeddata;
            });

            GC.Collect();

        }

        public void udpset()
        {
            SocketUDP Udp = new SocketUDP();
            Udp.Connect("192.168.2.7", 12345);

            Udp.Recieve(data);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                text.Text = Udp.decodeddata;
            });

            GC.Collect();

        }

    }

}

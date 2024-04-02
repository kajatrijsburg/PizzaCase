using System.Text;

namespace Appclient
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
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);

            udpset();
        }

        public void tcpset()
        {
            Tcp.Connect("192.168.2.2");
            data = Encoding.ASCII.GetBytes("TCP connect");
            Tcp.Send(data);
            CounterBtn.Text = Tcp.decodeddata;
            Tcp.Close();
        }

        public void udpset()
        {
            Udp.Connect("192.168.2.2");
            data = Encoding.ASCII.GetBytes("UDP connect");
            Udp.Send(data);
            CounterBtn.Text = Udp.decodeddata;
            Udp.Close();
        }
    }

}

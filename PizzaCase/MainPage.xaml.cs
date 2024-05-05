using Microsoft.Maui.Dispatching;

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
            udpset();
            //SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private void OntcpbtnClicked(object sender, EventArgs e)
        {
            tcpset();
            //udpset();
            //SemanticScreenReader.Announce(CounterBtn.Text);
        }


        public void tcpset()
        {
            SocketTCP Tcp = new SocketTCP();
            Tcp.Connect("192.168.2.7", 12345);
            Tcp.Recieve(data);

            text.Text = Tcp.decodeddata;

            Tcp.Close();

        }

        public void udpset()
        {
            SocketUDP Udp = new SocketUDP();
            Udp.Connect("192.168.2.7", 12345);
            Udp.Recieve(data);

            text.Text = Udp.decodeddata;
            Udp.Close();
        }

    }

}

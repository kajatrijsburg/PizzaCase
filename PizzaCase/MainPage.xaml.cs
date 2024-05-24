using Microsoft.Maui.Animations;
using Microsoft.Maui.Dispatching;
using System.Runtime.Intrinsics.Arm;

namespace PizzaCase
{
    public partial class MainPage : ContentPage
    {
        public byte[] data;
        public string ipadress = "192.168.1.250";

        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// start the two threads for udp and tcp.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnudpbtnClicked(object sender, EventArgs e)
        {
            Thread tcp = new Thread(tcpset);
            Thread udp = new Thread(udpset);
            tcp.Start();
            udp.Start();

        }

        /// <summary>
        /// Tcp thread runs only while mainthread is running
        /// </summary>
        public void tcpset()
        {
            {
                SocketTCP Tcp = new SocketTCP();
                Tcp.Connect(ipadress, 12344);
                while (MainThread.GetMainThreadSynchronizationContextAsync() != null)
                {
                    try {

                        Tcp.Recieve(data);

                        if (Tcp.GetDecodedData().Length == 0) { continue; }

                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            text.Text = Tcp.GetDecodedData();
                            Tcp.SetDecodedData("");
                        });

                    } catch { break; }
                }
                
            } //tcp is niet meer in scope

            GC.Collect();
        }

        /// <summary>
        /// udp thread runs only while mainthread is running
        /// </summary>
        public void udpset()
        {
            {
                SocketUDP Udp = new SocketUDP();
                Udp.Connect(ipadress, 12345);
                while (MainThread.GetMainThreadSynchronizationContextAsync() != null)
                {
                    try {
                        Udp.Recieve(data);

                        if (Udp.GetDecodedData().Length == 0) { continue; }

                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            text.Text = Udp.GetDecodedData();
                            Udp.SetDecodedData("");
                        });
                    }
                    catch{ break; }
                    
                }

            }//udp is niet meer in scope

            GC.Collect();

        }
    }
}

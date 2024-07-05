using Microsoft.Maui.Animations;
using Microsoft.Maui.Dispatching;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Serialization.Json;
using System.Text;

namespace PizzaCase
{
    public partial class MainPage : ContentPage
    {
        public byte[] data;
        public string ipadress = "127.0.0.1";

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
                            byte[] data = Encoding.Unicode.GetBytes(Tcp.GetDecodedData());
                            Tcp.SetDecodedData("");

                            var stream = new MemoryStream(data);
                            var ser = new DataContractJsonSerializer(typeof(EncryptedMessage));

                            stream.Position = 0;
                            //Trace.WriteLine(System.Text.Encoding.Default.GetString(buffer));

                            EncryptedMessage message = ser.ReadObject(stream) as EncryptedMessage; //todo add list

                            try
                            {
                                text.Text = Encryption.Decrypt(message.message, encryption_key.Text, message.IV);
                            }
                            catch(Exception e) {
                                text.Text = e.Message;
                            }
                            
                            
                            
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
                            byte[] data = Encoding.Unicode.GetBytes(Udp.GetDecodedData());
                            Udp.SetDecodedData("");

                            var stream = new MemoryStream(data);
                            var ser = new DataContractJsonSerializer(typeof(EncryptedMessage));

                            stream.Position = 0;
                            //Trace.WriteLine(System.Text.Encoding.Default.GetString(buffer));

                            EncryptedMessage message = ser.ReadObject(stream) as EncryptedMessage; //todo add list
                            try
                            {
                                text.Text = Encryption.Decrypt(message.message, encryption_key.Text, message.IV);
                            }
                            catch (Exception e)
                            {
                                text.Text = e.Message;
                            }
                        });
                    }
                    catch{ break; }
                    
                }

            }//udp is niet meer in scope

            GC.Collect();

        }
    }
}

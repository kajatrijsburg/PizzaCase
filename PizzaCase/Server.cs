using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PizzaCase
{
    internal class Server
    {
        private static Server? _instance;
        private Label? outputLabel;
        private string ipaddress;
        private string key;
        private byte[] data;
        private Thread udp;
        private Thread tcp;

        private Server(string ipaddress, string key)
        {
            this.ipaddress = ipaddress;
            this.key = key;
        }

        public static Server GetInstance(string ipaddress, string key)
        {
            if (_instance == null)
            {
                return _instance = new Server( ipaddress, key);

            }
            return _instance;
        }

        public void SetOutputLabel(Label label)
        {
            this.outputLabel = label;
        }

        public void SetKey(string key)
        {
            this.key = key;
        }
        public void Start ()
        {
            tcp = new Thread(tcpset);
            udp = new Thread(udpset);
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
                Tcp.Connect(ipaddress, 12344);
                while (MainThread.GetMainThreadSynchronizationContextAsync() != null)
                {
                    try
                    {

                        Tcp.Recieve(data);

                        if (Tcp.GetDecodedData().Length == 0) { continue; }

                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            byte[] data = Encoding.Unicode.GetBytes(Tcp.GetDecodedData());
                            Tcp.SetDecodedData("");

                            var stream = new MemoryStream(data);
                            var ser = new DataContractJsonSerializer(typeof(EncryptedMessage));

                            stream.Position = 0;

                            EncryptedMessage message = ser.ReadObject(stream) as EncryptedMessage; //todo add list

                            try
                            {
                                outputLabel.Text = Encryption.Decrypt(message.message, key, message.IV);
                            }
                            catch (Exception e)
                            {
                                outputLabel.Text = e.Message;
                            }



                        });

                    }
                    catch { break; }
                }

            } //tcp is out of scope

            GC.Collect();
        }

        /// <summary>
        /// udp thread runs only while mainthread is running
        /// </summary>
        public void udpset()
        {
            {
                SocketUDP Udp = new SocketUDP();
                Udp.Connect(ipaddress, 12345);
                while (MainThread.GetMainThreadSynchronizationContextAsync() != null)
                {
                    try
                    {
                        Udp.Recieve(data);

                        if (Udp.GetDecodedData().Length == 0) { continue; }

                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            byte[] data = Encoding.Unicode.GetBytes(Udp.GetDecodedData());
                            Udp.SetDecodedData("");

                            var stream = new MemoryStream(data);
                            var ser = new DataContractJsonSerializer(typeof(EncryptedMessage));

                            stream.Position = 0;

                            EncryptedMessage message = ser.ReadObject(stream) as EncryptedMessage; //todo add list
                            try
                            {
                                outputLabel.Text = Encryption.Decrypt(message.message, key, message.IV);
                            }
                            catch (Exception e)
                            {
                                outputLabel.Text = e.Message;
                            }
                        });
                    }
                    catch { break; }

                }

            }//udp is niet meer in scope

            GC.Collect();

        }

    }
}

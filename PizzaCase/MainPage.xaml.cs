using Microsoft.Maui.Animations;
using Microsoft.Maui.Dispatching;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Serialization.Json;
using System.Text;

namespace PizzaCase
{
    public partial class MainPage : ContentPage
    {
        static string ipadress = "127.0.0.1";
        Server server = Server.GetInstance(ipadress, "");

        public MainPage()
        {
            InitializeComponent();
            server.SetOutputLabel(text);
        }

        /// <summary>
        /// start the two threads for udp and tcp.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnudpbtnClicked(object sender, EventArgs e)
        {
            server.Start();
            text.Text = "started server on: " + ipadress;
        }
        private void OnkeybtnClicked(object sender, EventArgs e)
        {
            server.SetKey(encryption_key.Text);
        }

    }
}

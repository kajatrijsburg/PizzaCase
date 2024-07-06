using PizzaCase;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Appclient
{
    public partial class MainPage : ContentPage
    {
        public SocketTCP Tcp;
        public SocketUDP Udp;
        public byte[] data;
        public Order order;
        public Pizza pizza;
        public string ipadress = "127.0.0.1";

        public MainPage()
        {
            InitializeComponent();
            order = new Order();
            order.pizzas = new List<Pizza>();
            order.name = "Test";
            pizza = new Pizza();
        }

        /// <summary>
        /// add the pizza to the pizza to list, and reset the input field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddPizza_Clicked(object sender, EventArgs e)
        {
            //validate
            string? errorMsg = Validator.Any(pizzaname.Text, "pizza name", 1, 100);
            if (errorMsg != null) { error.Text = errorMsg; return; }
            try
            {
                pizza.count = int.Parse(pizzacount.Text);
            }
            catch
            {
                error.Text = "Failed to parse pizza count.";
                return;
            }

            pizza.name = pizzaname.Text;
            order.pizzas.Add(pizza);
            pizza = new Pizza();
            pizza.extraToppings = new List<string>();
            pizzaname.Text = "";
            pizzacount.Text = "";

        }
        /// <summary>
        /// add the toppings, and reset the input field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddtopping_Clicked(object sender, EventArgs e)
        {
            string? errorMessage = Validator.Any(Topping.Text, "topping", 1, 100);
            if (errorMessage != null) { error.Text= errorMessage; return; }
            pizza.extraToppings.Add(Topping.Text);
            Topping.Text = "";
        }

        /// <summary>
        /// send through udp 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendudp_Clicked(object sender, EventArgs e)
        {
            Udp = new SocketUDP();
            Udp.Connect(ipadress, 12345);
            set_order();
            Udp.Send(data);
            Udp.Close();
        }

        /// <summary>
        /// send through tcp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendtcp_Clicked(object sender, EventArgs e)
        {
            Tcp = new SocketTCP();
            Tcp.Connect(ipadress, 12344);
            set_order();
            Tcp.Send(data);
            Tcp.Close();
        }

        /// <summary>
        /// create the order, convert order to bytes and reset the input fields
        /// </summary>
        private void set_order()
        {
            string? errorMessage = Validator.Any(name.Text, "name", 1, 100);
            if (errorMessage != null) { error.Text = errorMessage; return; }
            errorMessage = Validator.Any(postalCode.Text, "postalcode", 1, 100);
            if (errorMessage != null) { error.Text = errorMessage; return; }
            errorMessage = Validator.Any(city.Text, "city", 1, 100);
            if (errorMessage != null) { error.Text = errorMessage; return; }
            errorMessage = Validator.Any(street.Text, "street", 1, 100);
            if (errorMessage != null) { error.Text = errorMessage; return; }
            errorMessage = Validator.Any(housenumber.Text, "housenumber", 1, 100);
            if (errorMessage != null) { error.Text = errorMessage; return; }

            order.name = name.Text;
            order.postalCode = postalCode.Text;
            order.city = city.Text;
            order.street = street.Text;
            order.housenumber = housenumber.Text;
            order.timeSend = DateTime.Now;

            VisitorUTFConverter convert = new VisitorUTFConverter();
            convert.VisitOrder(order);
            Random ran = new Random();
            string IV = "test";//ran.Next().ToString();
            byte[] text = Encryption.Encrypt(convert.GetString(), encryption_key.Text, IV);
            EncryptedMessage message = new EncryptedMessage(text, IV);

            var stream1 = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof(EncryptedMessage));

            ser.WriteObject(stream1, message);

            stream1.Position = 0;
            var sr = new StreamReader(stream1);
            string mes = sr.ReadToEnd();
            data = Encoding.Unicode.GetBytes(mes);
            sr.Close();

            name.Text = "";
            postalCode.Text = "";
            city.Text = "";
            street.Text = "";
            housenumber.Text = "";
            order = new Order();
            order.pizzas = new List<Pizza>();
            pizza = new Pizza();
            pizza.extraToppings = new List<string>();
        }
    }

}

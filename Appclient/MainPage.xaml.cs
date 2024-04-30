using System.Text;

namespace Appclient
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        bool pizzaord = false;
        public SocketTCP Tcp;
        public SocketUDP Udp;
        public byte[] data;
        public Order order;
        public Pizza pizza;

        public MainPage()
        {
            InitializeComponent();
            Tcp = new SocketTCP();
            Udp = new SocketUDP();
            order = new Order();
            order.pizzas = new List<Pizza>();
            pizza = new Pizza();
            pizza.extraToppings = new List<string>();
        }

        /// <summary>
        /// add the pizza to the pizza to list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddPizza_Clicked(object sender, EventArgs e)
        {
            pizza.name = pizzaname.Text;
            pizza.count = int.Parse(pizzacount.Text);
            order.pizzas.Add(pizza);
            pizza = new Pizza();
            pizza.extraToppings = new List<string>();
            pizzaname.Text = "";
            pizzacount.Text = "";

        }
        /// <summary>
        /// add the toppings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddtopping_Clicked(object sender, EventArgs e)
        {
            pizza.extraToppings.Add(Topping.Text);
            Topping.Text = "";
        }

        /// <summary>
        /// creates the order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOrder_Clicked(object sender, EventArgs e)
        {
            order.name = name.Text;
            order.postalCode = postalCode.Text;
            order.city = city.Text;
            order.street = street.Text;
            order.housenumber = housenumber.Text;



            //test
            string a  = order.name + " " + order.postalCode + " " + order.city + " " + order.street + " " + order.housenumber;

            foreach(Pizza piz in order.pizzas)
            {
                a = a + " " + piz.name + " " + piz.count;
                foreach (string top in piz.extraToppings)
                {
                    a = a+ " " + top;
                }
            }

            placeholder.Text = a;

            //reset the order later will be done in sendudp or tcp
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

        /// <summary>
        /// send through udp 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendudp_Clicked(object sender, EventArgs e)
        {
            Udp.Connect("192.168.2.2");
            data = Encoding.ASCII.GetBytes("the order udp"); //needs to be replaced with the order that will be send
            Udp.Send(data);
            //CounterBtn.Text = Udp.decodeddata;
            Udp.Close();
        }

        /// <summary>
        /// send through tcp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendtcp_Clicked(object sender, EventArgs e)
        {
            Tcp.Connect("192.168.2.2");
            data = Encoding.ASCII.GetBytes("the order tcp"); //needs to be replaced with the order that will be send
            Tcp.Send(data);
            //CounterBtn.Text = Tcp.decodeddata;
            Tcp.Close();
        }
    }

}

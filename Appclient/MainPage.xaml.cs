﻿using System.Text;

namespace Appclient
{
    public partial class MainPage : ContentPage
    {
        public SocketTCP Tcp;
        public SocketUDP Udp;
        public byte[] data;
        public Order order;
        public Pizza pizza;
        public string ipadress = "192.168.1.250";

        public MainPage()
        {
            InitializeComponent();
            order = new Order();
            order.pizzas = new List<Pizza>();
            pizza = new Pizza();
            pizza.extraToppings = new List<string>();
        }

        /// <summary>
        /// add the pizza to the pizza to list, and reset the input field
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
        /// add the toppings, and reset the input field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddtopping_Clicked(object sender, EventArgs e)
        {
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
            order.name = name.Text;
            order.postalCode = postalCode.Text;
            order.city = city.Text;
            order.street = street.Text;
            order.housenumber = housenumber.Text;
            order.timeSend = DateTime.Now;

            VisitorByteConverter convert = new VisitorByteConverter();
            convert.VisitOrder(order);
            data = convert.GetData();

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

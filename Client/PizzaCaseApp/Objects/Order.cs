using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCaseApp.Objects
{
    internal class Order
    {
        private UserInformation UserInformation;
        private List<Pizza> Pizzas = new List<Pizza>();
        private DateTime DateTime;
    }
}

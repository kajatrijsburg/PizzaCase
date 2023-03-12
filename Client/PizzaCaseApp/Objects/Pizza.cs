using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCaseApp.Objects
{
    internal class Pizza
    {
        private string Type { get; set; }
        private List<PizzaTopping> toppings = new List<PizzaTopping>();
    }
}

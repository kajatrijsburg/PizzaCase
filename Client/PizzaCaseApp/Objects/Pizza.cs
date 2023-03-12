using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCaseApp.Objects
{
    internal class Pizza
    {
        public string Type { get; set; }
        public List<PizzaTopping> toppings = new List<PizzaTopping>();
    }
}

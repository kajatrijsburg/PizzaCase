using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCaseApp.Objects
{
    internal static class ToppingTypes
    {
        public static readonly string Onion = "Onion";
        public static readonly string Tuna = "Tuna";
        public static readonly string Pepperoni = "Pepperoni";
        public static readonly string BlackOlives = "Black Olives";
        public static readonly string GreenOlives = "Green Olives";
        public static readonly string Mushrooms = "Mushrooms";
        public static readonly string ExtraCheese = "Extra Cheese";
        public static readonly string pesto = "Pesto";
        public static readonly string Pineapple = "pineapple";
        public static readonly string Ham = "Ham";
        public static readonly string Anchovies = "Anchovies";
        public static readonly string Chicken = "Chicken";
        public static readonly string Jalapenos = "Jalapenos";
        public static readonly string Mozzarella = "Mozzarella";
        public static readonly string Bellpeppers = "Bellpeppers";

        public static readonly List<string> AllTypes = new() {
            "Onion",
            "Tuna",
            "Pepperoni",
            "Black Olives",
            "Green Olives",
            "Mushrooms",
            "Extra Cheese",
            "Pesto",
            "pineapple",
            "Ham",
            "Anchovies",
            "Chicken",
            "Jalapenos",
            "Mozzarella",
            "Bellpeppers"
        };
    }
}

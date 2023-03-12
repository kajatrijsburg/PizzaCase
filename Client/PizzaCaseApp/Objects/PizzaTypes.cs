namespace PizzaCaseApp.Objects
{
    internal static class PizzaTypes
    {
        public static readonly string Margherita = "Margherita";
        public static readonly string Tonno = "Tonno";
        public static readonly string Cheese = "Cheese";
        public static readonly string Hawaii = "Hawaii";
        public static readonly string Pollo = "Pollo";
        public static readonly string Peperoni = "Peperoni";

        public static readonly List<string> AllTypes = new()
            {
            "Margherita",
            "Tonno",
            "Cheese",
            "Hawaii",
            "Pollo",
            "Peperoni"
            };
    }
}

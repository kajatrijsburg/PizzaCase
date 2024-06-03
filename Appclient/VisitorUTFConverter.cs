using PizzaCase;


public class VisitorUTFConverter : IVisitor
{
    private string str = "";
    
    /// <summary>
    /// Visits an instance of the Order class and generates a formatted message with the contents of the class. This message may be retrieved by calling GetString.
    /// </summary>
    /// <param name="order"></param>
    public void VisitOrder(Order order)
    {
        str = "";
        str += order.name + "\n";
        str += order.street + " " + order.housenumber + ", " + order.city + "\n";
        str += order.postalCode + "\n";
        str += "\n";
        foreach (Pizza pizza in order.pizzas)
        {
            pizza.Accept(this);
        }
        str += order.timeSend.ToShortTimeString();
    }
    /// <summary>
    /// Visits an instance of the Pizza class and generates a formatted message with the contents of the class. This message may be retrieved by calling GetString.
    /// </summary>
    /// <param name="pizza"></param>
    public void VisitPizza(Pizza pizza)
    {
        str += pizza.name + "\n";
        foreach (string topping in pizza.extraToppings)
        {
            str += topping + " ";
        }
        str += "\n";
    }

    /// <summary>
    /// returns the most recently generated message.
    /// </summary>
    /// <returns></returns>
    public string GetString() { return str; }
}


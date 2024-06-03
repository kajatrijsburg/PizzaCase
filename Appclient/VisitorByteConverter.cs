using PizzaCase;
using System.Text;

public class VisitorByteConverter : IVisitor
{
    private byte[]? _data;

    /// <summary>
    /// visits an instance of the Order class and generates a byte array based on a text conversion of that object. You may retrieve this byte array by calling GetData
    /// </summary>
    /// <param name="order"></param>
    public void VisitOrder(Order order)
    {
        VisitorUTFConverter visitorUTFConverter = new();
        order.Accept(visitorUTFConverter);
        string str = visitorUTFConverter.GetString();
        _data = Encoding.Unicode.GetBytes(str);
    }

    /// <summary>
    /// visits an instance of the Pizza class and generates a byte array based on a text conversion of that object. You may retrieve this byte array by calling GetData
    /// </summary>
    /// <param name="pizza"></param>
    public void VisitPizza(Pizza pizza)
    {
        VisitorUTFConverter visitorUTFConverter = new();
        pizza.Accept(visitorUTFConverter);
        string str = visitorUTFConverter.GetString();
        _data = Encoding.Unicode.GetBytes(str);
    }

    /// <summary>
    /// retrieves the last generated byte array. If no byte arrays have been generated it returns Null.
    /// </summary>
    /// <returns></returns>
    public byte[] GetData() => _data;
}


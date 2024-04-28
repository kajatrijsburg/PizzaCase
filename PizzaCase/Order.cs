using PizzaCase;

public class Order : IAcceptVisitor
{
	public string name;

	public string postalCode;

	public string city;

	public string street;

	public string housenumber;

	public List<Pizza> pizzas;

	public DateTime timeSend;

	public void Accept(IVisitor Visitor)
	{
		Visitor.VisitOrder(this);
	}

}


using PizzaCase;

public class Pizza
{
	public string name;

	public int count;

	public List<string> extraToppings;

    public void Accept(IVisitor Visitor)
    {
        Visitor.VisitPizza(this);
    }

}


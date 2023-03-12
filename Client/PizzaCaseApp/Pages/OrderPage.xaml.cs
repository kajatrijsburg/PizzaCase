using PizzaCaseApp.Objects;

namespace PizzaCaseApp.Pages;

public partial class OrderPage : ContentPage
{
    private Order CurrentOrder = new();
	private Pizza CurrentPizza = new();

    public OrderPage()
	{
		InitializeComponent();
		foreach (string pizzaType in PizzaTypes.AllTypes)
		{
			pizza_picker.Items.Add(pizzaType);
		}

		foreach (string toppingTime in ToppingTypes.AllTypes)
		{
			topping_picker.Items.Add(toppingTime);
		}
	}
}
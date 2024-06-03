namespace PizzaCase
{
    public interface IVisitor
    {
        void VisitOrder(Order order);

        void VisitPizza(Pizza pizza);

    }
}
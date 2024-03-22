using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCase
{
    public interface IAcceptVisitor
    {
        void Accept(IVisitor visitor);
    }
}

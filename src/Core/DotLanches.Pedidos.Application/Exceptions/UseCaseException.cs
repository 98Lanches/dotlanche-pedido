using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotLanches.Pedidos.UseCases.Exceptions
{
    public class UseCaseException(string message) : Exception(message)
    {
    }
}

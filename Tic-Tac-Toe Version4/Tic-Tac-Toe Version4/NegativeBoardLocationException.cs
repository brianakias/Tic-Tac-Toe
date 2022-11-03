using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
    public class NegativeCoordinatesException : Exception
    {
        public NegativeCoordinatesException(string message) : base(message)
        {

        }
    }
}

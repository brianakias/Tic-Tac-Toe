using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
    public class NegativeBoardCoordinatesException : Exception
    {
        public NegativeBoardCoordinatesException(string message) : base(message)
        {

        }
    }
}

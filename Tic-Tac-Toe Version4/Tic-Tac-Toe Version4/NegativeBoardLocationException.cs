using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
    public class NegativeBoardLocationException : Exception
    {
        public NegativeBoardLocationException(string message) : base(message)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papago
{
    public class HotkeyException : Exception
    {
        public HotkeyException(string message) : base(message) { }
        public HotkeyException(string message, Exception inner) : base(message, inner) { }
    }
}

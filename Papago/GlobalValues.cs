using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papago
{
    class GlobalValues
    {
        public PapagoApi api;
        public PLangType Lang;
        public Hotkey hotKey;
        public System.Windows.Forms.NotifyIcon nIcon;
        public bool isClose = false;

        public GlobalValues()
        {
            api = new PapagoApi();
            Lang = new PLangType();
        }
    }
}

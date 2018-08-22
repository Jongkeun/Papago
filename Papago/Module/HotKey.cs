using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Papago
{
    class HotKey
    {
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            Windows = 8
        }

        public void Regist(IntPtr hwd)
        {
            NativeMethods.RegisterHotKey(hwd, PDefine.OPEN, (uint)(KeyModifiers.Control | KeyModifiers.Shift | KeyModifiers.Alt), Key.F);
        }

        public void UnRegist(IntPtr hwd)
        {
            NativeMethods.UnregisterHotKey(hwd, PDefine.OPEN);
        }
    }
}

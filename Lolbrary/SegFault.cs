using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lolbrary
{
    public static class SegFault
    {
        public static void GenSegFault()
        {
            System.Runtime.InteropServices.Marshal.ReadInt32(IntPtr.Zero);
        }
    }
}

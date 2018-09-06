using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace XamarinNDKSample
{
    public static class XamCppLibCS
    {

        [DllImport("libCppLib.so", EntryPoint = "Add_Integers")]
        public static extern int Add(int left, int right);
    }
}

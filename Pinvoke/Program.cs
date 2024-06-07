using System;
using System.Runtime.InteropServices;

namespace Pinvoke
{
    public class Program
    {
        // Define a delegate that corresponds to the unmanaged function.
        private delegate bool EnumWC(IntPtr hwnd, IntPtr lParam);
        private delegate void MyCallback(int i);
        private delegate void MyCallback2(string i);

        // Import user32.dll (containing the function we need) and define
        // the method corresponding to the native function.
        [DllImport("user32.dll")]
        private static extern int EnumWindows(EnumWC lpEnumFunc, IntPtr lParam);

        [DllImport("MyDll.dll")]
        private static extern int RegisterIntCallback(MyCallback callback);

        [DllImport("MyDll.dll")]
        private static extern int RegisterStringCallback(MyCallback2 callback);

        [DllImport("MyDll.dll")]
        private static extern int CallIntCallback(int j);

        [DllImport("MyDll.dll")]
        private static extern int CallStringCallback(string j);

        [DllImport("MyDll.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private extern static IntPtr AddPerson(Person person);
        // Define the implementation of the delegate; here, we simply output the window handle.
        private static bool OutputWindow(IntPtr hwnd, IntPtr lParam)
        {
            Console.WriteLine(hwnd.ToInt64());
            return true;
        }
        private static void PrintString(string s)
        {
            Console.WriteLine(s);
        }
        private static void Print(int a)
        {
            Console.WriteLine(a);
        }
        static void Main(string[] args)
        {
            // Invoke the method; note the delegate as a first parameter.
            //EnumWindows(OutputWindow, IntPtr.Zero);
            RegisterIntCallback(Print);
            CallIntCallback(123);

            RegisterStringCallback(PrintString);
            CallStringCallback("你好 Pinvoke");

            var person = new Person() { username = "dotnetfly", password = "123456" };

            var ptr = AddPerson(person);
            var str = Marshal.PtrToStringAnsi(ptr);

            Console.WriteLine($"username={str}");

            Console.ReadKey();
        }
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Person
    {
        /// char*
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        public string username;

        /// char*
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        public string password;
    }
}

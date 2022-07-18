using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ConsoleApp1
{


    internal class Program
    {

        const int STD_INPUT_HANDLE = -10;
        const uint ENABLE_QUICK_EDIT_MODE = 0x0040;
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetStdHandle(int hConsoleHandle);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint mode);


        public static void DisbleQuickEditMode()
        {
            IntPtr hStdin = GetStdHandle(STD_INPUT_HANDLE);
            uint mode;
            GetConsoleMode(hStdin, out mode);
            mode &= ~ENABLE_QUICK_EDIT_MODE;
            SetConsoleMode(hStdin, mode);
        }

        static void Main(string[] args)
        {
            DisbleQuickEditMode();

            var task = Task.Run(async () =>
            {
                int i = 0;
                while (true)
                {
                    Console.WriteLine(i++);
                    await Task.Delay(1000);
                }
            });
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            CalcInvoker invoker = new CalcInvoker();
            UserInterface ui = new UserInterface(invoker);
            Console.WriteLine("Enter an expression followed by the '=' key. Use 'Q' to exit.");
            char ch = Console.ReadKey().KeyChar;
            while (ch != 'Q')
            {
                ui.GetInput(ch);
                ch = Console.ReadKey().KeyChar;
            }
        }
    }
}

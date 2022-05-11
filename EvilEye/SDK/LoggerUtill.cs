using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilEye.SDK
{
    class LoggerUtill
    {
        public static void DisplayLogo()
        {
            Console.Title = "EvilEye";
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("=============================================================================================================");
            Console.WriteLine("                                                                                                             ");           
            Console.WriteLine("                          ███████╗██╗░░░██╗██╗██╗░░░░░  ███████╗██╗░░░██╗███████╗                            ");
            Console.WriteLine("                          ██╔════╝██║░░░██║██║██║░░░░░  ██╔════╝╚██╗░██╔╝██╔════╝                            ");
            Console.WriteLine("                          █████╗░░╚██╗░██╔╝██║██║░░░░░  █████╗░░░╚████╔╝░█████╗░░                            ");
            Console.WriteLine("                          ██╔══╝░░░╚████╔╝░██║██║░░░░░  ██╔══╝░░░░╚██╔╝░░██╔══╝░░                            ");
            Console.WriteLine("                          ███████╗░░╚██╔╝░░██║███████╗  ███████╗░░░██║░░░███████╗                            ");
            Console.WriteLine("                          ╚══════╝░░░╚═╝░░░╚═╝╚══════╝  ╚══════╝░░░╚═╝░░░╚══════╝                            ");
            Console.WriteLine("                                               Free Version V1.0.2                                                ");                               
            Console.WriteLine("=============================================================================================================");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Log(string msg, ConsoleColor color = ConsoleColor.White)
        {
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("EvilEye");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] "); 
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(DateTime.Now.ToString("hh:mm"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

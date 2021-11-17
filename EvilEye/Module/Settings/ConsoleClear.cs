using EvilEye.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilEye.SDK;

namespace EvilEye.Module.Settings
{
    class ConsoleClear : BaseModule
    {
        public ConsoleClear() : base("Clear Console", "Clears Melon Loader Console", Main.Instance.settingsButton, null, false)
        {
        }
        public override void OnEnable()
        {
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
            Console.WriteLine("                                 Beta Release By Four_DJ, Literal, and Fish.                                 ");
            Console.WriteLine("=============================================================================================================");
            Console.ForegroundColor = ConsoleColor.White;
            LoggerUtill.Log("Cleared Console!", Console.ForegroundColor);
        }
    }
}

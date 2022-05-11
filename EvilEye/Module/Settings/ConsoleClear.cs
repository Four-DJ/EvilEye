using EvilEye.SDK;
using System;



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
            Console.WriteLine("                                               Free Version V1.0.2                                                ");
            Console.WriteLine("=============================================================================================================");
            Console.ForegroundColor = ConsoleColor.White;
            LoggerUtill.Log("Cleared Console!", ConsoleColor.Green);
        }
    }
}

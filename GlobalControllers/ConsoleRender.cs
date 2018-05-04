using OurLastHope.Screens;
using System;
using System.Threading;

namespace OurLastHope.Controllers
{
    public class ConsoleRender
    {
        const int C_WIDTH = 70;
        const int C_HEIGHT = 25;

        public static ConsoleRender Instance { get; } = new ConsoleRender();

        IScreen currentScreen;
        bool isRunning;

        private ConsoleRender()
        {
            Console.SetWindowSize(C_WIDTH, C_HEIGHT);
            Console.Title = "OUR LAST HOPE";
            currentScreen = GameController.initialScreen;
        }

        public void Run()
        {
            if (isRunning) { return; }
            isRunning = true;
            while(isRunning)
            {
                currentScreen.Display();
            }
            Console.Clear();
            Console.WriteLine("El programa se cerrara...");
            Thread.Sleep(1000);
        }
        public void Quit()
        {
            isRunning = false;
        }

        public void Render(IScreen screen)
        {
            currentScreen.Clear();
            currentScreen = screen;
        }
    }

}

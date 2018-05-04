using OurLastHope.Controllers;
using System;

namespace OurLastHope.Screens
{
    public class QuitGameScreen : BaseScreen
    {
        static QuitGameScreen instance;
        public override IScreen PreviousScreen => DefaultScreens.PlayerMenu;

        public QuitGameScreen() : base(Evaluate)
        {
            instance = this;
        }

        public override void Display()
        {
            Console.WriteLine(@"¿Desea Salir del Juego?
    1. No.
    2. Si.
");
            base.Display();
        }

        static void Evaluate(int value)
        {
            GameController.Beep();

            if (value == 1) { ConsoleRender.Instance.Render(instance.PreviousScreen); }
            if(value == 2) { ConsoleRender.Instance.Quit(); }
        }
    }
}

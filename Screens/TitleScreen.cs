using OurLastHope.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurLastHope.Screens
{
    public class TitleScreen : BaseScreen
    {
        static TitleScreen instace;

        IScreen nextScreen;
        const string FORMAT = @"            OUR LAST HOPE           

La Tierra ya no puede contener más Vida.         
Encuentra un Nuevo Planeta antes que se te acabe el tiempo.


            TIENES {0} TURNOS.


Pulsa 1 y Enter para Continuar.
";

        public override IScreen PreviousScreen { get { return null; } }

        public TitleScreen(IScreen nextScreen) : base(EvaluateInput)
        {
            instace = this;
            this.nextScreen = nextScreen;
        }

        static void EvaluateInput(int value)
        {
            if(value != 1) { return; }

            GameController.Beep();
            ConsoleRender.Instance.Render(instace.nextScreen);
        }

        public override void Display()
        {
            Clear();

            Console.WriteLine(string.Format(FORMAT, GameController.TOTAL_TURNS));

            base.Display();
        }
    }
}

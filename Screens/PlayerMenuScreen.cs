using OurLastHope.Controllers;
using OurLastHope.Player;
using System;

namespace OurLastHope.Screens
{
    public class PlayerMenuScreen : BaseScreen
    {
        const string FORMAT = @"            PLANETA TIERRA

TURNO ACTUAL: {0}. TURNOS RESTANTES: {1}.

RECURSOS:
    Metal: {2},
    Cristal: {3},
    Alimento:{4},
    Conbustible: {5},
    Trabajadores:{6}/{7}.

ACCIONES DISPONIBLES:
    1. Explorar el Universo.
    2. Ver Instalaciones.
    3. Finalizar Turno.
    4. Salir de Juego.
";

        public PlayerMenuScreen(Action<int> onInputSubmit) : base(onInputSubmit)
        {

        }

        public override IScreen PreviousScreen => DefaultScreens.TitleScreen;

        public override void Display()
        {
            Console.WriteLine(string.Format(FORMAT,
                GameController.CurrentTurn,
                GameController.TOTAL_TURNS - GameController.CurrentTurn,
                PlayerController.Instance.Resources.Metal,
                PlayerController.Instance.Resources.Cristal,
                PlayerController.Instance.Resources.Food,
                PlayerController.Instance.Resources.Fuel,
                PlayerController.Instance.UneployedWorkers,
                PlayerController.Instance.EmployedWorkers + PlayerController.Instance.UneployedWorkers));

            base.Display();
        }
    }
}

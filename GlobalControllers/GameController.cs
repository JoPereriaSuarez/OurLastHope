using System;
using System.Threading;
using OurLastHope.Player;
using OurLastHope.Screens;

namespace OurLastHope.Controllers
{
    /// <summary>
    /// Manage turns. Has GameOver and GameSucces Condition
    /// Contain Initial Screen to Render
    /// </summary>
    public static class GameController
    {
        public const int TOTAL_TURNS = 30;
        public static int CurrentTurn { get; private set; }
        public static readonly IScreen initialScreen = DefaultScreens.TitleScreen;

        const string BEGIN_TURN_FORMAT = @"Comienza el Turno {0}.
{1} TURNOS PARA EL FIN DE LA TIERRA.
{2} Personas Muertas por Hambre.

Pulsa Enter para Continuar...";

        static GameController()
        {
            CurrentTurn = 1;
        }

        public static void EndTurn()
        {
            Console.Clear();
            Console.WriteLine($"Terminando Turno {CurrentTurn}");
            Thread.Sleep(500);

            PlayerController.Instance.UpdatePlayerState();
            if(CurrentTurn +1 > TOTAL_TURNS)
            {
                Thread.Sleep(1000);
                GameOver();
            }
            StartTurn();
        }

        public static void StartTurn()
        {
            CurrentTurn++;
            Console.Clear();
            //            Console.WriteLine($"Comienza el Turno {CurrentTurn}");
            //            Console.WriteLine($"{TOTAL_TURNS - CurrentTurn} Turnos para el Fin de la Tierra");
            //            Console.WriteLine(@" 
            //Pulsa Enter para continuar...");

            Console.WriteLine(string.Format(BEGIN_TURN_FORMAT, 
                CurrentTurn, 
                TOTAL_TURNS-CurrentTurn, 
                PlayerController.Instance.DeadPeople));

            Console.Read();
            Console.Clear();
        }

        public static void GameOver()
        {

        }
        public static void Beep()
        {
            Console.Beep();
        }
    }
}

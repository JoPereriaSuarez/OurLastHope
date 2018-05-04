using System;
using OurLastHope.Player;

namespace OurLastHope.Screens
{
    public class PlayerBuildingsScreen : BaseScreen
    {
        const string FORMAT = "{0}. {1} (Nivel {2}, Trabajadores: {3} de {4}.)";

        public PlayerBuildingsScreen(Action<int> onInputSubmit) : base(onInputSubmit)
        {

        }

        public override IScreen PreviousScreen => PlayerController.Instance.playerMenu;

        public override void Display()
        {
            int i = 0;
            Console.WriteLine("INSTALACIONES ACTUALES \n \n");
            for (i= 0; i < PlayerController.Instance.Buildings.Length; i++)
            {
                Console.WriteLine(string.Format(FORMAT,
                    i + 1,
                    PlayerController.Instance.Buildings[i].Title,
                    PlayerController.Instance.Buildings[i].CurrentLevel,
                    PlayerController.Instance.Buildings[i].CurrentWorkers,
                    PlayerController.Instance.Buildings[i].MaxWorkers));

            }
            Console.WriteLine($"{i+1}. Átras.");
            Console.WriteLine();

            base.Display();
        }
    }
}

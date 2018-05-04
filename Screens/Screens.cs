using OurLastHope.Building;
using System;

namespace OurLastHope.Screens
{
    public struct DefaultScreens
    {
        public static IScreen TitleScreen
        {
            get { return new TitleScreen(PlayerMenu); }
        }
        public static IScreen PlayerMenu
        {
            get { return Player.PlayerController.Instance.playerMenu; }
        }
        public static IScreen TestMenu
        {
            get { return new TestScreen((value) => Console.WriteLine(value + " Choosed Option")); }
        }
        public static IScreen QuitScreen
        {
            get { return new QuitGameScreen(); }
        }

        /// <summary>
        /// Try to not use it so much.
        /// </summary>
        public static IScreen ExploreScreen
        {
            get
            {
                Observatory playerObservatory = Player.PlayerController.Instance.Buildings[0] as Observatory;
                return playerObservatory.ExploreUniverseScreen;
            }
        }
    }
}

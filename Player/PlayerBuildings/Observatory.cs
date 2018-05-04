using OurLastHope.Controllers;
using OurLastHope.Planets;
using OurLastHope.Player;
using OurLastHope.Screens;
using System;

namespace OurLastHope.Building
{
    public class Observatory : BaseBuilding
    {
        public override string Title => "Observatorio";
        Planet[][] allPlanets;
        public int AllPlanetAmount
        {
            get { return allPlanets[CurrentLevel - 1].Length; }
        }


        public SpyingReport[] SpyingReports { get; private set; }
        public Planet[] PlanetsToDisplay
        {
            get
            {
                if (!IsWorking) { return null; }
                return allPlanets[CurrentLevel - 1];
            }
        }
        public override int MaxWorkers
        {
            get
            {
                switch (CurrentLevel)
                {
                    case 1:
                        return 5;
                    case 2:
                        return 10;
                    case 3:
                    default:
                        return 15;
                }
            }
        }

        public override IScreen Screen => new ObservatoryScreen(this);
        public IScreen ExploreUniverseScreen { get { return new ExploreUniverseScreen(this); } }

        public Observatory(int initialWorkers = 0, int initialLevel = 1) : base ( new Resources[]
            {
                new Resources(metal: 150, cristal: 50, workers: 5, fuel: 0),
                new Resources(metal: 250, cristal: 100, workers: 10, fuel: 0),
                new Resources(metal: 400, cristal: 150, workers: 25, fuel: 50),
            }, new int[] { 8, 5, 3 } )
        {
            initialWorkers = Mathf.Clamp(initialWorkers, 0, MaxWorkers);
            if (initialWorkers > 0) { TakeWorkersFromPlayer(initialWorkers); }
            

            allPlanets = new Planet[maxLevel] [];
            bool findHome = false;
            for (int i = 0; i < allPlanets.Length; i++)
            {
                if (!findHome)
                {
                    findHome = ((new Random(Mathf.RandomSeed()).Next((i + 1) * 3, 12 - 3 + i)) >= 9);
                    ///DEBUG
                    //if (findHome) { Console.WriteLine($"Find Planet at Level {i+1}"); Console.Read(); }
                    allPlanets[i] = GeneratePlanets(i + 1, findHome);
                }
                else
                {
                    allPlanets[i] = GeneratePlanets(i + 1);
                }
            }
        }

        public override void DoWork(Action<int> callback)
        {
            for (int i = 0; i < Workers.Count; i++)
            {
                Workers[i].Health -= Worker.DEFAULT_HUNGRY + inpactOnWorkers[CurrentLevel - 1];
            }
        }

        /// <summary>
        /// Evaluate the result of the Observatory Menu Screen
        /// </summary>
        /// <param name="value"></param>
        public override void EvaluateOption(int value)
        {
            switch (value)
            {
                case 1:
                    ConsoleRender.Instance.Render(ExploreUniverseScreen);
                    break;
                case 2:
                    WaitToAddWorkers();
                    break;
                case 3:
                    WaitToAddWorkers(false);
                    break;
                case 4:
                    ConsoleRender.Instance.Render(Screen.PreviousScreen);
                    break;
                default:
                    Console.Clear();
                    break;
            }

            GameController.Beep();
        }

        /// <summary>
        /// Evaluate the reslt of the Explore Universe Screen
        /// </summary>
        /// <param name="value"></param>
        public void AccesToPlanet(int value)
        {
            if (value == AllPlanetAmount)
            {
                ConsoleRender.Instance.Render(ExploreUniverseScreen.PreviousScreen);
            }

            GameController.Beep();
        }

        private Planet[] GeneratePlanets(int level, bool addHabitablePlanet = false)
        {
            int planetsAmount = 5 * level - 2;
            int homePlanetLocation = (addHabitablePlanet)? new Random().Next(0, planetsAmount) : - 1;

            Planet[] planetsToReturn = new Planet[planetsAmount];
            for (int i = 0; i < planetsAmount; i++)
            {
                if(homePlanetLocation > -1 && i ==homePlanetLocation)
                {
                    planetsToReturn[i] = Planet.RandomHabitablePlanet();
                    continue;
                }
                planetsToReturn[i] = Planet.RandomNonHabitable(level);
            }

            return planetsToReturn;
        }
    }
}

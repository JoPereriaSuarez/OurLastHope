using OurLastHope.Building;
using OurLastHope.Controllers;
using OurLastHope.Screens;
using System;
using System.Collections.Generic;

namespace OurLastHope.Player
{
    public class PlayerController
    {
        private static readonly PlayerController instance = new PlayerController();
        public static PlayerController Instance => instance;

        const int INITIAL_METAL = 100;
        const int INITIAL_CRISTAL = 100;
        const int INITIAL_FUEL = 100;
        const int INITIAL_FOOD = 45;
        const int INITIAL_WORKERS = 45;
        const int INITIAL_DEAD_WORKERS = 0;

        const int FOOD_RATION = 3;

        public int DeadPeople { get; private set; }

        public int EmployedWorkers
        {
            get
            {
                int value = 0;
                for (int i = 0; i < Buildings.Length; i++)
                {
                    value += Buildings[i].CurrentWorkers;
                }
                return value;
            }
        }
        public int UneployedWorkers
        {
            get { return resources.Workers.Count; }
        }

        public readonly IScreen playerMenu;

        /// <summary>
        /// Workers from here are the total amount of free workers
        /// </summary>
        private Resources resources;
        public Resources Resources { get { return resources; } }

        public PlayerBuildingsScreen PlayerBuildings => new PlayerBuildingsScreen(AccesToBuilding);

        public IBuildingController[] Buildings { get; private set; }
       

        /// <summary>
        /// Constructor: Create Player with default Resources
        /// </summary>
        private PlayerController()
        {
            resources = new Resources(INITIAL_METAL, INITIAL_CRISTAL, INITIAL_FOOD, INITIAL_FUEL, INITIAL_WORKERS);
            Buildings = new IBuildingController[]
            {
                new Observatory(),
                new RecyclingCenter()
            };

            DeadPeople = INITIAL_DEAD_WORKERS;

            playerMenu = new PlayerMenuScreen(Evaluate);
        }

        void Evaluate(int value)
        {
            switch(value)
            {
                case 1:
                    Observatory observatory = Buildings[0] as Observatory;
                    ConsoleRender.Instance.Render(observatory.ExploreUniverseScreen);
                    break;
                case 2:
                    ConsoleRender.Instance.Render(PlayerBuildings);
                    break;
                case 3:
                    GameController.EndTurn();
                    break;
                case 4:
                    ConsoleRender.Instance.Render(DefaultScreens.QuitScreen);
                    break;
                default:
                    Console.Clear();
                    break;
            }
            GameController.Beep();
        }

        void AccesToBuilding(int i)
        {
            try
            {
                if(i > Buildings.Length) { ConsoleRender.Instance.Render(playerMenu); }

                ConsoleRender.Instance.Render(Buildings[i - 1].Screen);
                GameController.Beep();
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        /// <summary>
        /// Call doWork from every building
        /// Update expedition state
        /// Feed workers
        /// Update workers health state
        /// </summary>
        public void UpdatePlayerState()
        {
            UpdateWorkersHealth();
            UpdateBuildings();
            FeedWorkers();
            DeadPeople = RemoveDeadWorkers();
        }

        void UpdateWorkersHealth()
        {
            Random randomHealth = new Random();
            for (int i = 0; i < Buildings.Length; i++)
            {
                for (int j = 0; j < Buildings[i].Workers.Count; j++)
                {
                    Buildings[i].Workers[j].Health -= (Worker.DEFAULT_HUNGRY + randomHealth.Next(-10, +10));
                }
            }

            for(int i = 0; i < resources.Workers.Count; i++)
            {
                resources.Workers[i].Health -= (Worker.DEFAULT_HUNGRY + randomHealth.Next(-10, +10));
            }
        }

        /// <summary>
        /// Execute DoWork for all buildings. Do it manually
        /// </summary>
        void UpdateBuildings()
        {
            Buildings[0].DoWork((i) => i = 0);
            Buildings[1].DoWork((food) => resources.Food += food);
        }

        void FeedWorkers()
        {
            int foodRation = FOOD_RATION;
            for(int i = 0; i < Buildings.Length; i++)
            {
                for (int j = 0; j < Buildings[i].CurrentWorkers; j++)
                {
                    if (resources.Food == 0) { return; }
                    if((resources.Food - FOOD_RATION)  <= 0)
                    {
                        foodRation = resources.Food;
                        //Console.WriteLine("food " + foodRation);
                        //Console.Read();
                    }
                    else
                    {
                        foodRation = FOOD_RATION;
                    }
                    Buildings[i].Workers[j].Feed(foodRation);
                    resources.Food -= FOOD_RATION;
                }
            }

            for (int i = 0; i < resources.Workers.Count; i++)
            {
                if (resources.Food == 0) { return; }
                if ((resources.Food - FOOD_RATION) <= 0)
                {
                    foodRation = resources.Food;
                    //Console.WriteLine("food " + foodRation);
                    //Console.Read();
                }
                else
                {
                    foodRation = FOOD_RATION;
                }
                resources.Workers[i].Feed(foodRation);
                resources.Food -= foodRation;
            }
        }

        int RemoveDeadWorkers()
        {
            int deadWorkers = 0;
            for (int i = 0; i < Buildings.Length; i++)
            {
                deadWorkers += Buildings[i].Workers.RemoveAll((w) => w.HealthState == WorkerHealth.Dead);
            }
            deadWorkers += resources.Workers.RemoveAll((w) => w.HealthState == WorkerHealth.Dead);

            return deadWorkers;
        }
    }

}

using OurLastHope.Controllers;
using OurLastHope.Player;
using OurLastHope.Screens;
using System;
using System.Collections.Generic;

namespace OurLastHope.Building
{
    public class TestRecyclingCenter : IBuildingController
    {
        public string Title => "Centro de Reciclaje";

        public IScreen Screen { get { return null; } } //{ get { return new RecyclingScreen(this); } }

        private int[] inpactOnWorkers;
        private const int MAX_LEVEL = 3;

        public int CurrentLevel { get; private set; }

        private readonly Resources[] AllLevelRequirements = new Resources[]
        {
            new Resources(metal: 150, cristal: 50, workers: 5, fuel: 0),
            new Resources(metal: 250, cristal: 100, workers: 10, fuel: 0),
            new Resources(metal: 400, cristal: 150, workers: 25, fuel: 50),
        };

        public Resources NextLevelRequirements
        {
            get
            {
                if(CurrentLevel >= MAX_LEVEL)
                {
                    return new Resources();
                }
                return AllLevelRequirements[CurrentLevel];
            }
        }
        public int MaxWorkers
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
                        return 25;
                }
            }
        }
        public List<Worker> Workers { get; set; }

        public int CurrentWorkers
        {
            get { return Workers.Count; }
        }
        public int FoodProduction
        {
            get { return GetFoodProduction(CurrentLevel, Workers.Count); }
        }
        public int FoodProductionPerWorker
        {
            get
            {
                return GetFoodProduction(CurrentLevel, 1);
            }
        }
        public int ProductionNextLevel
        {
            get
            {
                int level = (CurrentLevel + 1 > MAX_LEVEL) ? 0 : CurrentLevel + 1;
                return GetFoodProduction(level, 1);
            }
        }

        public bool IsWorking => throw new NotImplementedException();

        int GetFoodProduction(int level, int workers)
        {
            return (int)(Math.Round((workers * 2.2F) * (level * 0.83F)));
        }

        public TestRecyclingCenter()
        {
            CurrentLevel = 1;
            Workers = new List<Worker>();

            inpactOnWorkers = new int[]
            {
                15,
                10,
                8,
            };
        }

        public void EvaluateOption(int value)
        {
            switch(value)
            {
                case 1:
                    WaitToAddWorkers();
                    break;
                case 2:
                    WaitToAddWorkers(false);
                    break;
                case 3:
                    LevelUp(PlayerController.Instance.Resources);
                    break;
                case 4:
                    ConsoleRender.Instance.Render(Screen.PreviousScreen);
                    break;
            }
            GameController.Beep();
        }

        void WaitToAddWorkers(bool positive = true)
        {
            string format = "Ingresa la Cantidad de Trabajadores a {0}.";
            int value;
            do
            {
                Console.WriteLine(string.Format(format, (positive) ? "Agregar" : "Remover"));
            }
            while (!Int32.TryParse(Console.ReadLine().ToString(), out value) || value <= 0);

            Console.Clear();
            Console.WriteLine();
            if (positive)
            {
                int quantity = TakeWorkersFromPlayer(value);
                Console.WriteLine($"Se agregaron {quantity} Trabajadores");
            }
            else
            {
                Worker[] workersToRemove = RemoveWorkers(value);
                PlayerController.Instance.Resources.Workers.AddRange(workersToRemove);
                Console.WriteLine($"Se quitaron {workersToRemove.Length} trabajadores");
            }
            Console.WriteLine("Pulsa Enter para Continuar");
            Console.Read();
        }

        public int TakeWorkersFromPlayer(int quantity)
        {
            if (PlayerController.Instance.UneployedWorkers < quantity)
            {
                quantity = PlayerController.Instance.UneployedWorkers;
            }
            quantity = Mathf.Clamp(quantity, 0, MaxWorkers);

            for (int i = 0; i < quantity; i++)
            {
                AddWorker(PlayerController.Instance.Resources.Workers[i]);
            }
            PlayerController.Instance.Resources.Workers.RemoveRange(0, quantity);
            return quantity;
        }
        public void AddWorker(Worker worker)
        {
            if(CurrentWorkers + 1 > MaxWorkers) { return; }
            Workers.Add(worker);
        }


        public Worker[] RemoveWorkers(int numberOfWorkers)
        {
            if(numberOfWorkers <= 0) { return null; }
            if(numberOfWorkers > CurrentWorkers)
            {
                numberOfWorkers = CurrentWorkers;
            }

            Worker[] workersToRemove = new Worker[numberOfWorkers];
            for(int i = 0; i < numberOfWorkers; i++)
            {
                workersToRemove[i] = Workers[i];
            }
            numberOfWorkers = Math.Max(0, numberOfWorkers - 1);
            Workers.RemoveRange(0, numberOfWorkers);
            return workersToRemove;
        }
        public void RemoveWorkers(Worker[] workers)
        {
            for(int i = 0; i < workers.Length; i++)
            {
                Workers.Remove(workers[i]);
            }
            Workers.TrimExcess();
        }

        /// <summary>
        /// Change the states over the player's worker
        /// Increase the players foood
        /// </summary>
        /// <param name="callback"></param>
        public void DoWork(Action<int> callback)
        {
            for(int i = 0; i < Workers.Count; i++)
            {
                Workers[i].Health -= Worker.DEFAULT_HUNGRY + inpactOnWorkers[CurrentLevel-1];
            }

            callback.Invoke(FoodProduction); 
        }

        public bool LevelUp(Resources resources)
        {
            if(CurrentLevel == MAX_LEVEL) { return false; }
            if(resources.Metal == NextLevelRequirements.Metal &&
                resources.Fuel == NextLevelRequirements.Fuel &&
                resources.Cristal == NextLevelRequirements.Cristal)
            {
                CurrentLevel++;
            }
            return true;
        }
    }
}

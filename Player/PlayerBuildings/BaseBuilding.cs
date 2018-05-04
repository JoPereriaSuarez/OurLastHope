using OurLastHope.Player;
using OurLastHope.Screens;
using System;
using System.Collections.Generic;

namespace OurLastHope.Building
{
    public abstract class BaseBuilding : IBuildingController
    {
        public abstract string Title { get; }
        public abstract int MaxWorkers { get; }
        public abstract IScreen Screen { get; }
        public Resources NextLevelRequirements
        {
            get
            {
                if (CurrentLevel >= maxLevel)
                {
                    return new Resources();
                }
                return allLevelRequirements[CurrentLevel];
            }
        }

        protected readonly int maxLevel;
        protected readonly Resources[] allLevelRequirements;
        protected readonly int[] inpactOnWorkers;

        public int CurrentLevel { get; private set; }
        protected int workersDifferenceToWork = 2;

        public bool IsWorking
        {
            get { return CurrentWorkers >= MaxWorkers - workersDifferenceToWork; }
        }

        public List<Worker> Workers { get; protected set; }
        public int CurrentWorkers { get { return Workers.Count; } }

        protected BaseBuilding(Resources[] allLevelRequirements, int[] inpactOnWorkers, int initialLevel = 0, int workersDifferenceToWork = 3, int maxLevel = 3)
        {
            Workers = new List<Worker>();
            maxLevel = Math.Max(0, maxLevel);
            this.maxLevel = maxLevel;
            initialLevel = Mathf.Clamp(initialLevel, 1, maxLevel);
            CurrentLevel = initialLevel;
            this.allLevelRequirements = allLevelRequirements;
            this.inpactOnWorkers = inpactOnWorkers;
            this.workersDifferenceToWork = workersDifferenceToWork;
        }

        public abstract void DoWork(Action<int> callback);

        public bool LevelUp(Resources resources)
        {
            if (CurrentLevel == maxLevel) { return false; }
            if (resources.Metal == NextLevelRequirements.Metal &&
                resources.Fuel == NextLevelRequirements.Fuel &&
                resources.Cristal == NextLevelRequirements.Cristal)
            {
                CurrentLevel++;
            }
            return true;
        }

        protected void WaitToAddWorkers(bool positive = true)
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
            if (CurrentWorkers + 1 > MaxWorkers) { return; }
            Workers.Add(worker);
        }
        public Worker[] RemoveWorkers(int numberOfWorkers)
        {
            if (numberOfWorkers <= 0) { return null; }
            if (numberOfWorkers > CurrentWorkers)
            {
                numberOfWorkers = CurrentWorkers;
            }

            Worker[] workersToRemove = new Worker[numberOfWorkers];
            for (int i = 0; i < numberOfWorkers; i++)
            {
                workersToRemove[i] = Workers[i];
            }
            numberOfWorkers = Math.Max(0, numberOfWorkers);
            Workers.RemoveRange(0, numberOfWorkers);
            return workersToRemove;
        }
        public void RemoveWorkers(Worker[] workers)
        {
            for (int i = 0; i < workers.Length; i++)
            {
                Workers.Remove(workers[i]);
            }
            Workers.TrimExcess();
        }

        public abstract void EvaluateOption(int value);
    }
}

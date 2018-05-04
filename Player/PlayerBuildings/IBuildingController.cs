using OurLastHope.Player;
using OurLastHope.Screens;
using System;
using System.Collections.Generic;

namespace OurLastHope.Building
{
    public interface IBuildingController
    {
        string Title { get; }
        int CurrentLevel { get; }
        IScreen Screen { get; }
        bool IsWorking { get; }

        Resources NextLevelRequirements { get; }

        List<Worker> Workers { get; }

        int CurrentWorkers { get; }
        int MaxWorkers { get; }

        /// <summary>
        /// Execute its operation over the player states.
        /// Call the player callback
        /// </summary>
        /// <param name="callback"></param>
        void DoWork(Action<int> callback);

        bool LevelUp(Resources resources);

        void AddWorker(Worker worker);
        /// <summary>
        /// Remove the first n workers from building
        /// </summary>
        /// <param name="numberOfWorkers"></param>
        /// <returns></returns>
        Worker[] RemoveWorkers(int numberOfWorkers);

        /// <summary>
        /// Remove an specific list of workers
        /// </summary>
        /// <param name="workers"></param>
        void RemoveWorkers(Worker[] workers);
    }
}

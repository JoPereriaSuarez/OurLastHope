using OurLastHope.Controllers;
using OurLastHope.Player;
using OurLastHope.Screens;
using System;

namespace OurLastHope.Building
{
    public class RecyclingCenter : BaseBuilding
    {
        public override string Title => "Centro de Reciclaje";

        public override IScreen Screen { get { return new RecyclingScreen(this); } }

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
                        return 25;
                }
            }
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
                int level = (CurrentLevel + 1 > maxLevel) ? 0 : CurrentLevel + 1;
                return GetFoodProduction(level, 1);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="initialWorkers"></param>
        public RecyclingCenter(int initialWorkers = 0, int initialLevel = 1) : base( new Resources[] 
            {
                new Resources(metal: 150, cristal: 50, workers: 5, fuel: 0),
                new Resources(metal: 250, cristal: 100, workers: 10, fuel: 0),
                new Resources(metal: 400, cristal: 150, workers: 25, fuel: 50),
            },  new int[] { 15, 10, 8 }, initialLevel )
        {
            initialWorkers = Mathf.Clamp(initialWorkers, 0, MaxWorkers);
            if(initialWorkers == 0) { return; }
            TakeWorkersFromPlayer(initialWorkers);
        }

        int GetFoodProduction(int level, int workers)
        {
            return (int)(Math.Round((workers * 2.2F) * (level * 0.83F)));
        }

        public override void DoWork(Action<int> callback)
        {
            for (int i = 0; i < Workers.Count; i++)
            {
                Workers[i].Health -= Worker.DEFAULT_HUNGRY + inpactOnWorkers[CurrentLevel - 1];
            }
            if (!IsWorking) { return; }
            callback.Invoke(FoodProduction);
        }

        public override void EvaluateOption(int value)
        {
            switch (value)
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

                default:
                    Screen.Clear();
                    break;
            }
            GameController.Beep();
        }
    }
}

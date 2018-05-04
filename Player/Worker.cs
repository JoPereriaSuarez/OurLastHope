using System;

namespace OurLastHope.Player
{
    public class Worker
    {
        public const int DEFAULT_HUNGRY = 20;

        private int health;
        public int Health { get => health; set => health = value; }

        public Worker() : this(100)
        { 
        }
        public Worker(int health)
        {
            this.health = health;
        }

        public WorkerHealth HealthState
        {
            get
            {
                if(health <= 0) { return WorkerHealth.Dead; }
                if(health > 0 && health <= 20) { return WorkerHealth.Terrible; }
                if(health > 20 && health <= 60) { return WorkerHealth.Sick; }

                return WorkerHealth.Fine; 
            }
        }

        public void Feed(int food)
        {
            int amount = (int)Math.Round((food * 5.5F) + (new Random(Mathf.RandomSeed()).Next(-5, 8) * (food > 0 ? 1 : 0)) );
            health += amount;
            //Console.WriteLine(food + "  " + amount);
            //Console.Read();
        }
    }

    public enum WorkerHealth
    {
        Dead = 0,
        Terrible = 20,
        Sick = 60,
        Fine = 100,
    }
}

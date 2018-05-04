using OurLastHope.Player;
using System;
using System.Collections.Generic;

namespace OurLastHope
{
    public struct Resources
    {
        const int MAX_METAL     = 10000;
        const int MAX_CRISTAL   = 10000;
        const int MAX_FOOD      = 10000;
        const int MAX_FUEL      = 10000;
        const int MAX_WORKERS   = 10000;

        public static Resources Empty
        {
            get { return new Resources(0, 0, 0, 0, 0); }
        }
        public static Resources Full
        {
            get { return new Resources(MAX_METAL, MAX_CRISTAL, MAX_FOOD, MAX_FUEL, MAX_WORKERS); }
        }

        public static Resources operator +(Resources a, Resources b)
        {
            return new Resources(
                a.metal + b.metal,
                a.cristal + b.cristal,
                a.food + b.food,
                a.fuel + b.fuel,
                a.Workers.Count + b.Workers.Count );
        }
        public static Resources operator -(Resources a, Resources b)
        {
            return new Resources(
                a.metal - b.metal,
                a.cristal - b.cristal,
                a.food - b.food,
                a.fuel - b.fuel,
                a.Workers.Count - b.Workers.Count );
        }

        int metal;
        public int Metal
        {
            get { return metal; }
            set
            {
                if (value < 0 || value == int.MaxValue) { return; }
                metal = value;
                metal = Mathf.Clamp(metal, 0, MAX_METAL);
            }
        }

        int cristal;
        public int Cristal
        {
            get { return cristal; }
            set
            {
                if (value < 0 || value == int.MaxValue) { return; }
                cristal = value;
                cristal = Mathf.Clamp(cristal, 0, MAX_CRISTAL);
            }
        }

        int food;
        public int Food
        {
            get { return food; }
            set
            {
                if(value == int.MaxValue) { return; }
                food = value;
                food = Mathf.Clamp(food, 0, MAX_FOOD);
            }
        }

        int fuel;
        public int Fuel
        {
            get { return fuel; }
            set
            {
                if (value < 0 || value == int.MaxValue) { return; }
                fuel = value;
                fuel = Mathf.Clamp(fuel, 0, MAX_FUEL);
            }
        }

        public List<Worker> Workers { get; set; }

        public Resources(int metal, int cristal, int fuel, int workers) : 
            this(metal, cristal, 0, fuel, workers)
        {

        }
        public Resources(int metal, int cristal, int fuel) : 
            this(metal, cristal, 0, fuel, 0)
        {

        }
        public Resources(int metal, int cristal, int food, int fuel, int workers)
        {
            metal = Math.Max(0, metal);
            cristal = Math.Max(0, cristal);
            food = Math.Max(0, food);
            fuel = Math.Max(0, fuel);
            workers = Math.Max(0, workers);

            this.metal = metal;
            this.cristal = cristal;
            this.food = food;
            this.fuel = fuel;
            Workers = new List<Worker>();
            for(int i = 0; i < workers; i++)
            {
                Workers.Add(new Worker());
            }
        }
    }

}

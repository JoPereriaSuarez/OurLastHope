using OurLastHope.AttackSystem;
using OurLastHope.Screens;
using System;
using System.Collections.Generic;

namespace OurLastHope.Planets
{
    public struct Planet
    {
        const int MIN_DIAMETER = 2290;
        const int MAX_DIAMETER = 120000;

        const int FREEZE_MIN_TEMPERATURE = -120;
        const int FREEZE_MAX_TEMPERATURE = -20;

        const int COLD_MIN_TEMPERATURE = -15;
        const int COLD_MAX_TEMPERATURE = 5;

        const int WARM_MIN_TEMPERATURE = -3;
        const int WARM_MAX_TEMPERATURE = 25;

        const int HOT_MIN_TEMPERATURE = 20;
        const int HOT_MAX_TEMPERATURE = 50;

        const int HELL_MIN_TEMPERATURE = 60;
        const int HELL_MAX_TEMPERATURE = 120;

        static string[] defaultNames = new string[]
        {
            "Frarvis YG1K",
            "Slarth K8",
            "Therth R521",
            "Crolla S37",
            "Smion 5F",
            "Brurn YSY",
            "Clion O68",
            "Snars 0O2",
            "Skoria SN",
            "Glinda KFH",
            "Skonoe TFT",
            "Glara 5F2",
            "Briea 076Z",
            "Frao 4F8",
            "Breon 22SO",
            "Spurn 1K3",
            "Sopapigloblo",
            "Slurn 31PW",
            "Plosie FNZ",
            "Glion 65Z",
            "Plichi O424",
            "Flagua Z6TF",
            "Plichi O424",
            "Flagua Z6TF",
            "Whinda IRBF",
            "Spilia 55W1",
            "Smuna 58L3",
            "Fleshan STZ",
            "Sporia 67",
            "Whone S808",
            "Shore 1WD",
            "Whippe VIO",
            "Wiseppi",
            "Blurn 0I",
            "Plapus 112",
            "Stadus 45P",
            "Drurn FT",
            "Shoth 28A",
            "Chichi H",
            "Drilia MBF",
            "Glorix ZMV4",
            "Plarth 716",
            "Tryria MLNP",
            "Swolla 94",
            "Window 95",
            "Clolla 70MZ",
            "Frorth 03",
        };
        static List<string> planetNamesList = new List<string>(defaultNames);

        public readonly string name;

        public readonly uint diameter;

        public readonly TemperatureType temperature;

        public readonly int minTemperature;
        public readonly int maxTemperature;

        public readonly bool breathable;
        public readonly bool water;

        public readonly int spyingLevel;

        public Resources resources;
        public readonly bool hasLife;
        public readonly PlanetLife lifeType;

        public Defense[] Defenses { get; private set; }
        public int GetDefenseValue()
        {
            int value = 0;
            return value;
        }
        //public IScreen Screen => new PlanetScreen()

        static void ResetPlanetNames()
        {
            planetNamesList.AddRange(defaultNames);
            planetNamesList.TrimExcess();
        }
        static string GetRandomName()
        {
            if (planetNamesList.Count == 0)
            {
                ResetPlanetNames();
            }

            string value = planetNamesList[new Random().Next(0, planetNamesList.Count)];
            planetNamesList.Remove(value);
            return value;
        }

        static int GetRandomMinTemperature(TemperatureType temperature)
        {
            Random r = new Random();
            int value = 0;
            switch (temperature)
            {
                case TemperatureType.Freeze:
                    value = r.Next(FREEZE_MIN_TEMPERATURE, FREEZE_MAX_TEMPERATURE / 2);
                    break;

                case TemperatureType.Cold:
                    value = r.Next(COLD_MIN_TEMPERATURE, COLD_MAX_TEMPERATURE / 2);
                    break;
                case TemperatureType.Warm:
                    value = r.Next(WARM_MIN_TEMPERATURE, WARM_MAX_TEMPERATURE / 2);
                    break;
                case TemperatureType.Hot:
                    value = r.Next(HOT_MIN_TEMPERATURE, HOT_MAX_TEMPERATURE / 2);
                    break;
                case TemperatureType.Hell:
                    value = r.Next(HELL_MIN_TEMPERATURE, HELL_MAX_TEMPERATURE / 2);
                    break;
            }
            return value;
        }
        static int GetRandomMaxTemperature(TemperatureType temperature, int minTemperature)
        {
            Random r = new Random();
            int value = 0;
            int min = minTemperature + (minTemperature / 2);
            switch (temperature)
            {
                case TemperatureType.Freeze:
                    value = r.Next(min, FREEZE_MAX_TEMPERATURE);
                    break;

                case TemperatureType.Cold:
                    value = r.Next(min, COLD_MAX_TEMPERATURE);
                    break;
                case TemperatureType.Warm:
                    value = r.Next(min, WARM_MAX_TEMPERATURE);
                    break;
                case TemperatureType.Hot:
                    value = r.Next(min, HOT_MAX_TEMPERATURE);
                    break;
                case TemperatureType.Hell:
                    value = r.Next(min, HELL_MAX_TEMPERATURE);
                    break;
            }
            return value;
        }
        static uint GetRandomDiameter()
        {
            return (uint)new Random(Mathf.RandomSeed()).Next(MIN_DIAMETER, MAX_DIAMETER);
        }

        public static Planet RandomNonHabitable(int level = 1, PlanetLife life = PlanetLife.NoLife)
        {
            level = Math.Max(level, 1);
            TemperatureType temperature = (TemperatureType)new Random(Mathf.RandomSeed()).Next((int)TemperatureType.Freeze, (int)TemperatureType.Hell);
            return new Planet(RandomResources(level), temperature, Mathf.RandomBool(), Mathf.RandomBool(), life, level);
        }
        public static Planet RandomHabitablePlanet()
        {
            Resources resources = Resources.Full;
            return new Planet(resources, TemperatureType.Warm, true, true, PlanetLife.NoLife | PlanetLife.Pacifict, 1);
        }

        public static Resources RandomResources(int level)
        {
            /// NOT IMPLEMENTED
            return new Resources(500, 250, 100);
        }

        private Planet(Resources resources, TemperatureType temperature, bool breathable, bool water, PlanetLife life, int level)
            : this( name: GetRandomName(),
                diameter: GetRandomDiameter(),
                temperature: temperature,
                lifeType: life,
                resources: resources,
                water: Mathf.RandomBool(), breathable: Mathf.RandomBool(),
                spyingLevel: level + new Random(Mathf.RandomSeed()).Next(-1, 1) )
        {

        }
        private Planet (string name, uint diameter,  TemperatureType temperature, bool breathable, bool water, Resources resources,  PlanetLife lifeType, int spyingLevel) 
            : this()
        {
            this.name = name;
            this.diameter = diameter;
            this.temperature = temperature;
            this.minTemperature = GetRandomMinTemperature(temperature);
            this.maxTemperature = GetRandomMaxTemperature(temperature, minTemperature);
            this.breathable = breathable;
            this.water = water;
            this.resources = resources;
            this.hasLife = (lifeType != PlanetLife.NoLife);
            this.lifeType = lifeType;
            this.spyingLevel = spyingLevel;

            planetNamesList.AddRange(defaultNames);
            planetNamesList.TrimExcess();
        }

    }

    public enum PlanetLife
    {
        NoLife = 0, // No Game?
        Pacifict = 2,
        Hostile = 5,
        Unknown = Pacifict - Hostile
    }
    public enum TemperatureType
    {
        Freeze = 1,
        Cold = 2,
        Hot = 3,
        Hell = 4,
        Warm = Hot + Cold,
    }
}

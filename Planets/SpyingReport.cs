namespace OurLastHope.Planets
{
    public struct SpyingReport 
    {
        const string MIN_REPORT_FORMAT = @"       INFORMACIÓN PLANETA: {0}.

RECURSOS: {1} Metal, {2} Cristal

    Diametro:       {3}km.
    Temperatura:    {4}.
";

        const string BASIC_REPORT_FORMAT = @"       INFORMACIÓN PLANETA: {0}.

RECURSOS: {1} Metal, {2} Cristal, {3} Combustible

    Atmosfera:          {4}.
    Diametro:           {5}km.
    Temperatura:        {6}.
    Temperartura Min:   {7}.
    Temperatura Max:    {8}.

";

        const string COMPLETE_REPORT_FORMAT = @"       INFORMACIÓN PLANETA: {0}.

RECURSOS: {1} Metal, {2} Cristal, {3} Combustible, {4} Comida.

    Atmosfera:          {5}.
    Diametro:           {6}km.
    Temperatura:        {7}.
    Temperartura Min:   {8}.
    Temperatura Max:    {9}.
    Agua:               {10}.

";

        const string FULL_REPORT_FORMAT = @"       INFORMACIÓN PLANETA: {0}.

RECURSOS: {1} Metal, {2} Cristal, {3} Combustible, {4} Comida.
GASTOS EN DEFENSAS: {5}.

    Atmosfera:          {6}.
    Diametro:           {7}km.
    Temperatura:        {8}.
    Temperartura Min:   {9}.
    Temperatura Max:    {10}.
    Agua:               {11}.

";



        public readonly SpyingInfoLevel infoLevel;

        public readonly string name;
        public readonly int diameter;
        public readonly Resources resources;
        public readonly int? minTemperature;
        public readonly int? maxTemperature;
        public readonly bool? breathable;
        public readonly TemperatureType temperatureType;
        public readonly PlanetLife life;
        public readonly bool? water;
        public readonly int? defensesValue;

        public static SpyingReport GetFromPlanet(int probes, Planet planet, int spyingLevel)
        {
            int spyDiff = (spyingLevel * probes) - planet.spyingLevel;
            SpyingInfoLevel infoLevel = (spyDiff <= 0 || spyDiff < (int)SpyingInfoLevel.BasicReport) ? SpyingInfoLevel.MinimalReport :
                (spyDiff >= (int)SpyingInfoLevel.BasicReport && spyDiff < (int)SpyingInfoLevel.CompleteReport) ? SpyingInfoLevel.BasicReport :
                (spyDiff >= (int)SpyingInfoLevel.CompleteReport && spyDiff < (int)SpyingInfoLevel.FullReport) ? SpyingInfoLevel.CompleteReport :
                SpyingInfoLevel.FullReport;

            return new SpyingReport(planet, infoLevel);
        }

        public string GetFormatedReport()
        {
            string value = "";
            switch(infoLevel)
            {
                case SpyingInfoLevel.MinimalReport:
                    value = string.Format(MIN_REPORT_FORMAT,
                        name, resources.Metal, resources.Cristal, diameter, temperatureType);
                    break;

                case SpyingInfoLevel.BasicReport:
                    value = string.Format(BASIC_REPORT_FORMAT,
                        name, resources.Metal, resources.Cristal, resources.Fuel, breathable, diameter,
                        temperatureType, minTemperature, maxTemperature);
                    break;

                case SpyingInfoLevel.CompleteReport:
                    value = string.Format(BASIC_REPORT_FORMAT,
                        name, resources.Metal, resources.Cristal, resources.Fuel, resources.Food,
                        breathable, diameter, temperatureType, minTemperature, maxTemperature, water);
                    break;

                case SpyingInfoLevel.FullReport:
                    value = string.Format(BASIC_REPORT_FORMAT,
                        name, resources.Metal, resources.Cristal, resources.Fuel, resources.Food, defensesValue,
                        breathable, diameter, temperatureType, minTemperature, maxTemperature, water);
                    break;
            }

            return value;
        }

        public SpyingReport(Planet planet, SpyingInfoLevel infoLevel)
            : this()
        {
            /// NO SE ME OCURRE NADA MEJOR 
            switch (infoLevel)
            {
                case SpyingInfoLevel.MinimalReport:
                    name = planet.name;
                    resources = new Resources(planet.resources.Metal, planet.resources.Cristal,0,0,0);
                    diameter = (int)planet.diameter;
                    temperatureType = planet.temperature;
                    life = PlanetLife.Unknown;
                    break;

                case SpyingInfoLevel.BasicReport:
                    diameter = (int)planet.diameter;
                    minTemperature = planet.minTemperature;
                    maxTemperature = planet.maxTemperature;
                    name = planet.name;
                    resources = new Resources(planet.resources.Metal, planet.resources.Cristal,0, planet.resources.Fuel, 0);
                    temperatureType = planet.temperature;
                    breathable = planet.breathable;
                    life = PlanetLife.Unknown;
                    break;

                case SpyingInfoLevel.CompleteReport:
                    diameter = (int)planet.diameter;
                    minTemperature = planet.minTemperature;
                    maxTemperature = planet.maxTemperature;
                    name = planet.name;
                    resources = new Resources(planet.resources.Metal, planet.resources.Cristal, planet.resources.Food, planet.resources.Fuel, 0);
                    temperatureType = planet.temperature;
                    breathable = planet.breathable;
                    life = planet.lifeType;
                    water = planet.water;

                    break;

                case SpyingInfoLevel.FullReport:
                    diameter = (int)planet.diameter;
                    minTemperature = planet.minTemperature;
                    maxTemperature = planet.maxTemperature;
                    name = planet.name;
                    resources = new Resources(planet.resources.Metal, planet.resources.Cristal, planet.resources.Food, planet.resources.Fuel, 0);
                    temperatureType = planet.temperature;
                    breathable = planet.breathable;
                    life = planet.lifeType;
                    water = planet.water;
                    defensesValue = planet.GetDefenseValue();

                    break;
            }
        }
    }

    public enum SpyingInfoLevel
    {
        MinimalReport = 0,
        BasicReport = 2,
        CompleteReport = 4,
        FullReport = 8,
    }
}

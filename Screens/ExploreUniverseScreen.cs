using OurLastHope.Building;
using OurLastHope.Planets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurLastHope.Screens
{
    public class ExploreUniverseScreen : BaseScreen
    {
        const string FORMAT = @"{0}. Nombre: {1}.
        Diametro:       {2}km.
        Temperatura:    min {3}°C / max {4}°C.
        Atmosfera:      {5}.
        Agua:           {6}.

";
        public override IScreen PreviousScreen => center.Screen;

        Observatory center;
        public Planet SelectedPlanet { get; private set; }

        public ExploreUniverseScreen(Observatory center) : base (center.AccesToPlanet)
        {
            this.center = center;
        }

        public override void Display()
        {
            int i = 0;
            if(center.PlanetsToDisplay == null)
            {
                Console.WriteLine("OBSERVATORIO NO OPERATIVO.");
                Console.WriteLine("ASIGNA TRABAJADORES PARA SU FUNCIONAMIENTO");

                Console.WriteLine($"    {i + center.AllPlanetAmount}. Átras.");
            }
            else
            {
                Console.WriteLine(@"        PLANETAS ENCONTRADOS
");
                for (i = 0; i < center.PlanetsToDisplay.Length; i++)
                {
                    Console.WriteLine(string.Format(FORMAT,
                        i +1,
                        center.PlanetsToDisplay[i].name.ToUpper(),
                        center.PlanetsToDisplay[i].diameter.ToString("N0"),
                        center.PlanetsToDisplay[i].minTemperature,
                        center.PlanetsToDisplay[i].maxTemperature,
                        (center.PlanetsToDisplay[i].breathable) ? "Respirable " : "No Apta",
                        (center.PlanetsToDisplay[i].water) ? "Posee Agua " : "NO Posee Agua"));
                }
                Console.WriteLine($"    {i +1}. Átras.");
            }

            base.Display();
        }
    }
}

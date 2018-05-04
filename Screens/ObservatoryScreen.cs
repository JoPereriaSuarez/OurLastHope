using OurLastHope.Building;
using System;

namespace OurLastHope.Screens
{
    public class ObservatoryScreen : BaseScreen
    {
        const string FORMAT = @"         OBSERVATORIO.

En el Observatorio puedes explorar los distintos Planetas y así,
poder encontrar nuestro proximo Hogar. 
Mejora el Observatorio para poder encontrar Planetas más lejanos.

Nivel Actual:       {0}. 
N° Trabajadores:    {1} / Max: {2}. 
Estado:             {3}

Requisitos Proximo Nivel: 
    Metal:                      {4},
    Cristal:                    {5},
    Trabajadores Disponibles:   {6}.
    Combustible:                {7},

Acciones Disponibles:
    1. Usar Observador.
    2. Asignar Trabajadores.
    3. Quitar Trabajadores.
    4. Subir de Nivel.
    5. Átras. ";

        Observatory center;

        public override IScreen PreviousScreen => Player.PlayerController.Instance.PlayerBuildings;
        public ObservatoryScreen(Observatory center) :
            base(center.EvaluateOption)
        {
            this.center = center;
        }

        public override void Display()
        {
            Console.WriteLine(string.Format(FORMAT,
                center.CurrentLevel,
                center.CurrentWorkers,
                center.MaxWorkers,
                (center.IsWorking) ? "Operativo" : "NO Operativo",
                center.NextLevelRequirements.Metal,
                center.NextLevelRequirements.Cristal,
                center.NextLevelRequirements.Workers.Count,
                center.NextLevelRequirements.Fuel));

            base.Display();
        }
    }
}

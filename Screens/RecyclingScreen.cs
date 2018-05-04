using OurLastHope.Building;
using System;

namespace OurLastHope.Screens
{
    public class RecyclingScreen : BaseScreen
    {
        const string FORMAT = @"        CENTRO DE RECICLAJE.

En el centro de Reciclaje, se convierten residuos en Comida.
A mayor nivel, más Comida se produce por turno

Nivel Actual:       {0}. 
N° Trabajadores:    {1} / Max: {2}. 
Estado:             {3}

Producción Actual:  +{4} Alimento por Trabajador por Turno.
Proximo Nivel:      +{5} Alimento por Trabajador por turno.

Requisitos Proximo Nivel: 
    Metal:                      {6},
    Cristal:                    {7},
    Trabajadores Disponibles:   {8}.
    Combustible:                {9},

Acciones Disponibles:
    1. Asignar Trabajadores.
    2. Quitar Trabajadores.
    3. Subir de Nivel.
    4. Átras. 
";

        RecyclingCenter center;
        public RecyclingScreen(RecyclingCenter center) :
            base (center.EvaluateOption)
        {
            this.center = center;
        }

        public override IScreen PreviousScreen => Player.PlayerController.Instance.PlayerBuildings;

        public override void Display()
        {
            Console.WriteLine(string.Format(FORMAT,
                center.CurrentLevel,
                center.CurrentWorkers,
                center.MaxWorkers,
                (center.IsWorking) ? "Operativo" : "NO Operativo",
                center.FoodProductionPerWorker, 
                center.ProductionNextLevel,
                center.NextLevelRequirements.Metal,
                center.NextLevelRequirements.Cristal,
                center.NextLevelRequirements.Workers.Count,
                center.NextLevelRequirements.Fuel));

            base.Display();
        }
    }
}

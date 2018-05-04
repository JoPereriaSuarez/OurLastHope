using OurLastHope.Building;
using OurLastHope.Planets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurLastHope.Screens
{
    public class PlanetScreen : BaseScreen
    {
        const string acciones = @"
ACCIONES DISPONIBLES
    1. Espiar.
    2. Visitar.
    3. Atacar.
    4. Átras.
";

        public PlanetScreen(SpyingReport report, BaseBuilding center) : base(center.EvaluateOption)
        {
        }

        public override IScreen PreviousScreen => DefaultScreens.ExploreScreen;


        public override void Display()
        {


            base.Display();
        }
    }
}

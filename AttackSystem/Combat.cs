using OurLastHope.Planets;
using System;

namespace OurLastHope.AttackSystem
{
    /// <summary>
    /// Agressive way to visit a Planet
    /// </summary>
    public struct Combat : IEncounter
    {
        public Ship[] Visitor { get; private set; }
        public Planet Local { get; private set; }
        public Resources Reward { get; private set; }
        int modifier;

        public Combat(Ship[] visitor, Planet local, int modifier) : this()
        {
            Visitor = visitor;
            Local = local;
            Reward = Resources.Empty;
            this.modifier = modifier;
        }

        public IEncounter Resolve()
        {
            throw new NotImplementedException();
        }
    }

}

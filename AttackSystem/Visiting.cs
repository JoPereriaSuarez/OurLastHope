using OurLastHope.Planets;
using System;

namespace OurLastHope.AttackSystem
{
    /// <summary>
    /// Pacific way to visit a planet
    /// </summary>
    public struct Visiting : IEncounter
    {
        public Ship[] Visitor { get; private set; }
        public Planet Local { get; private set; }
        public Resources Reward { get; private set; }
        int modifier;

        public Visiting(Ship[] visitor, Planet local, int modifier) : this()
        {
            Visitor = visitor;
            Local = local;
            this.modifier = modifier;
            Reward = Resources.Empty;
        }

        public IEncounter Resolve()
        {
            throw new NotImplementedException();
        }
    }
}

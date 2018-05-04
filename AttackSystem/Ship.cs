

namespace OurLastHope.AttackSystem
{
    public struct Ship : IStructure
    {
        public ushort Hull { get; private set; }
        public ushort Shield { get; private set; }
        public ushort Weapon { get; private set; }
        public readonly ushort cargo;

        public Ship(ushort hull, ushort shield, ushort weapon, ushort cargo) : this()
        {
            Hull = hull;
            Shield = shield;
            Weapon = weapon;
            this.cargo = cargo;
        }
    }
}

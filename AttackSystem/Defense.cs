
namespace OurLastHope.AttackSystem
{
    public struct Defense : IStructure
    {
        public readonly string name;

        public ushort Hull { get; private set; }
        public ushort Shield { get; private set; }
        public ushort Weapon { get; private set; }

        public Defense(string name, ushort hull, ushort shield, ushort weapon) : this()
        {
            this.name = name;
            Hull = hull;
            Shield = shield;
            Weapon = weapon;
        }
    }
}

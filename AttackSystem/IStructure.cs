using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurLastHope.AttackSystem
{
    public interface IStructure
    {
        ushort Hull { get; }
        ushort Shield { get; }
        ushort Weapon { get; }
    }
}

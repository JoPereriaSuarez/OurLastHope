using OurLastHope.Planets;

namespace OurLastHope.AttackSystem
{
    /// <summary>
    /// Interface when Player visit a Planet
    /// </summary>
    public interface IEncounter
    {
        Ship[] Visitor { get; }
        Planet Local { get; }
        Resources Reward { get; }

        /// <summary>
        /// The resources, ships and defenses remaining after the visit
        /// </summary>
        /// <returns></returns>
        IEncounter Resolve();
    }
}

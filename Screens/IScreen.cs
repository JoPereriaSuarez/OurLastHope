using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurLastHope.Screens
{
    /// <summary>
    /// Interface to display information on Console.
    /// Recieve user's input.
    /// </summary>
    public interface IScreen
    {
        IScreen PreviousScreen { get; }

        void Display();
        void Clear();
        void WaitForInput();
    }
}

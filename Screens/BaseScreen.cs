using System;

namespace OurLastHope.Screens
{
    /// <summary>
    /// Use reflection to display specifics screens information
    /// </summary>
    public abstract class BaseScreen : IScreen
    {
        const string CHOOSE_OPTION = "Elije una Opción";
        protected Action<int> callback;

        public abstract IScreen PreviousScreen { get; }

        public BaseScreen(Action<int> onInputSubmit)
        {
            callback = onInputSubmit;
        }
        ~BaseScreen()
        {
            callback = null;
        }

        public virtual void Display()
        {
            WaitForInput();
        }
        public virtual void Clear()
        {
            Console.Clear();
        }

        /// <summary>
        /// Wait until Player enters a positive numeric input (greater than 0)
        /// </summary>
        public void WaitForInput()
        {
            int value;
            do
            {
                Console.WriteLine(CHOOSE_OPTION);
            }
            while (!Int32.TryParse(Console.ReadLine().ToString(), out value) || value <= 0);

            callback.Invoke(value);
        }
    }
}

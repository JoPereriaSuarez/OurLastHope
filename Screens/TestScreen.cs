using System;

namespace OurLastHope.Screens
{
    internal class TestScreen : BaseScreen
    {
        public TestScreen(Action<int> onInputSubmit) : 
            base(onInputSubmit)
        {
        }

        public override IScreen PreviousScreen => throw new NotImplementedException();

        public override void Display()
        {
            System.Console.WriteLine(@"1. Test1.
2. Test2.
3. Test3. 
4. Atras.");

            base.Display();
        }
    }
}
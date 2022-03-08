using Prism.Mvvm;

namespace IssuesHoneys.Core.Types
{
    public class DummyClass : BindableBase
    {
        public DummyClass()
        {
            MyDummyProperty = 0;
        }

        public int MyDummyProperty { get; set; }
    }
}

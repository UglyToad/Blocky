namespace Blocky.Tests.Unit.Util
{
    using System.Linq;
    using Blocky.Util;
    using Shouldly;
    using UglyToad.Fixie.DataDriven;

    public class ThreeDimensionalListTests
    {
        private readonly ThreeDimensionalList<TestClass> list = new ThreeDimensionalList<TestClass>();

        public void CanSetOrigin()
        {
            list[0, 0, 0] = new TestClass("Boot");

            list[0, 0, 0].Name.ShouldBe("Boot");

            list.Count().ShouldBe(1);
        }

        [InlineData(-1, 0, 0)]
        [InlineData(0, -1, 0)]
        [InlineData(0, 0, -1)]
        [InlineData(-20, 0, -1)]
        public void CanAddAtNegativeIndex(int x, int y, int z)
        {
            list[x, y, z] = new TestClass("Moose");

            list[x, y, z].ShouldNotBeNull();

            list[x, y, z].Name.ShouldBe("Moose");
        }

        public void CanAddAndAddAgain()
        {
            list[0, 0, 0] = new TestClass("Bee");

            list[0, 1, 0] = new TestClass("Buzz");
        }
    }

    internal class TestClass
    {
        public string Name { get; set; }

        public TestClass(string name)
        {
            Name = name;
        }
    }
}

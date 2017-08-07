namespace Blocky.Tests.Unit.Util
{
    using System.Collections.Generic;
    using Blocky.Util;
    using Shouldly;
    using UglyToad.Fixie.DataDriven;

    public class WorldToLogicalSpaceConverterTests
    {
        private static IEnumerable<object[]> Values => new[]
        {
            new object[] {0f, 0f, 0f, new IntPoint3D(0, 0, 0)},
            new object[] {0f, 0f, 0.9999f, new IntPoint3D(0, 0, 0)},
            new object[] {0f, 0f, 1f, new IntPoint3D(0, 0, 0)},
            new object[] {0f, 0f, 1.5f, new IntPoint3D(0, 0, 1)},
            new object[] {0f, 0f, 1.0001f, new IntPoint3D(0, 0, 1)},
            new object[] {0f, 0f, 2f, new IntPoint3D(0, 0, 1)},
            new object[] {0f, 0f, 3f, new IntPoint3D(0, 0, 1)},
            new object[] {0f, 0f, 3.0001f, new IntPoint3D(0, 0, 2)},
            new object[] {0f, 0f, 3.051f, new IntPoint3D(0, 0, 2)},
            new object[] {0f, 0f, 4f, new IntPoint3D(0, 0, 2)},
            new object[] {0f, 0f, 5.00000001f, new IntPoint3D(0, 0, 2)},
            new object[] {0f, 0f, 5.001f, new IntPoint3D(0, 0, 3)},
            new object[] {0f, 0f, 5.5f, new IntPoint3D(0, 0, 3)},
            new object[] {0f, 0f, 6f, new IntPoint3D(0, 0, 3)},
            new object[] {0f, 0f, 6.5f, new IntPoint3D(0, 0, 3)},
            new object[] {0f, 0f, 7f, new IntPoint3D(0, 0, 3)},
            new object[] {1.5f, 5f, 10f, new IntPoint3D(1, 2, 5)},
            new object[] {7f, 3f, 7f, new IntPoint3D(3, 1, 3)},
        };

        [MemberData(nameof(Values))]
        public void Convert(float x, float y, float z, IntPoint3D expected)
        {
            const int blockSize = 1;

            var result = WorldToLogicalSpaceConverter.Convert(x, y, z, blockSize);
            
            result.X.ShouldBe(expected.X);
            result.Y.ShouldBe(expected.Y);
            result.Z.ShouldBe(expected.Z);
        }

        [InlineData(0f, 0, 1, 0)]
        [InlineData(0.9f, 0, 1, 0)]
        [InlineData(1f, 0, 1, 0)]
        [InlineData(1.01f, 0, 1, 1)]
        [InlineData(2f, 0, 1, 1)]
        [InlineData(2.5f, 0, 1, 1)]
        [InlineData(3f, 0, 1, 1)]
        [InlineData(3.1f, 0, 1, 2)]
        [InlineData(3.5f, 0, 1, 2)]
        [InlineData(4f, 0, 1, 2)]
        [InlineData(4.5f, 0, 1, 2)]
        [InlineData(5f, 0, 1, 2)]
        [InlineData(5.0000001f, 0, 1, 2)]
        [InlineData(5.000001f, 0, 1, 3)]
        public void ConvertsToCorrectInt(float value, int start, int size, int expected)
        {
            var result = WorldToLogicalSpaceConverter.ConvertSingle(value, size);

            result.ShouldBe(expected);
        }
    }
}

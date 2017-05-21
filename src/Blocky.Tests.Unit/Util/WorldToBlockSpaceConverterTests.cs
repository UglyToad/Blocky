using System.Collections.Generic;

namespace Blocky.Tests.Unit.Util
{
    using Blocky.Util;
    using Shouldly;
    using UglyToad.Fixie.DataDriven;

    public class WorldToBlockSpaceConverterTests
    {
        private static IEnumerable<object[]> Values => new[]
        {
            new object[] {0f, 0f, 0f, new IntPoint3D(0, 0, 0)},
        };

        [MemberData(nameof(Values))]
        public void Convert(float x, float y, float z, IntPoint3D expected)
        {
            const int blockSize = 1;

            var result = WorldToBlockSpaceConverter.Convert(x, y, z, blockSize);
            
            result.X.ShouldBe(expected.X);
            result.Y.ShouldBe(expected.Y);
            result.Z.ShouldBe(expected.Z);
        }
    }
}

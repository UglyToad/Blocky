using Blocky.Entities.Helpers;

namespace Blocky.Tests.Unit.Entities
{
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Shouldly;

    public class CubeFactoryTests
    {
        private const int BlockSize = 1;

        public void CubeHasCorrectNumberOfVertices()
        {
            const int verticesInAFace = 6;
            const int facesInACube = 6;

            var cube = CubeFactory.GetCubeWithDefaultFaces(BlockSize, new Vector3(0, 0, 0));

            cube.Length.ShouldBe(verticesInAFace * facesInACube);
        }

        /// <summary>
        ///  _+ size /2__
        /// |            |
        /// |    _       |
        /// |            |
        /// |__- size /2_|
        /// </summary>
        public void CubeAtOrigin()
        {
            var cube = CubeFactory.GetCubeWithDefaultFaces(BlockSize, new Vector3(0, 0, 0));

            const float halfSize = BlockSize / 2f;

            AssertCubeBounds(new Vector3(-halfSize, -halfSize, -halfSize), 
                new Vector3(halfSize, halfSize, halfSize), cube);
        }

        public void CubeAtOffset()
        {
            var cube = CubeFactory.GetCubeWithDefaultFaces(BlockSize, new Vector3(1, 2, 0));

            const float halfSize = BlockSize / 2f;

            AssertCubeBounds(new Vector3(1- halfSize, 2- halfSize, -halfSize), 
                new Vector3(1 + halfSize, 2 + halfSize, halfSize), cube);
        }

        public void BlockSizesChangesCube()
        {
            var cube = CubeFactory.GetCubeWithDefaultFaces(3, new Vector3(0, 0, 0));

            AssertCubeBounds(new Vector3(-1.5f, -1.5f, -1.5f), 
                new Vector3(1.5f, 1.5f, 1.5f), cube);
        }

        private static void AssertCubeBounds(Vector3 minimum, Vector3 maximum, VertexPositionColor[] cube)
        {
            cube.Min(x => x.Position.X).ShouldBe(minimum.X, 0.000001);
            cube.Max(x => x.Position.X).ShouldBe(maximum.X, 0.000001);

            cube.Min(x => x.Position.Y).ShouldBe(minimum.Y, 0.000001);
            cube.Max(x => x.Position.Y).ShouldBe(maximum.Y, 0.000001);

            cube.Min(x => x.Position.Z).ShouldBe(minimum.Z, 0.000001);
            cube.Max(x => x.Position.Z).ShouldBe(maximum.Z, 0.000001);

        }
    }
}

namespace Blocky.Tests.Unit.Entities
{
    using System.Linq;
    using Blocky.Entities;
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

            var cube = CubeFactory.GetCube(BlockSize, 0, 0, 0);

            cube.Length.ShouldBe(verticesInAFace * facesInACube);
        }

        /// <summary>
        ///  _+ size__
        /// |         |
        /// |    _    |
        /// |         |
        /// |__- size_|
        /// </summary>
        public void CubeAtOrigin()
        {
            var cube = CubeFactory.GetCube(BlockSize, 0, 0, 0);

            AssertCubeBounds(new Vector3(-BlockSize, -BlockSize, -BlockSize), 
                new Vector3(BlockSize, BlockSize, BlockSize), cube);
        }

        public void CubeAtOffset()
        {
            var cube = CubeFactory.GetCube(BlockSize, 1, 2, 0);

            AssertCubeBounds(new Vector3(1-BlockSize, 2-BlockSize, -BlockSize), 
                new Vector3(1 + BlockSize, 2 + BlockSize, BlockSize), cube);
        }

        public void BlockSizesChangesCube()
        {
            var cube = CubeFactory.GetCube(3, 0, 0, 0);

            AssertCubeBounds(new Vector3(-3, -3, -3), 
                new Vector3(3, 3, 3), cube);
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

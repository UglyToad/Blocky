using Blocky.Util;

namespace Blocky.Environment.Terrain.TerrainGenerators
{
    public class CubeTerrainGenerator : ITerrainGenerator
    {
        private readonly int dimension;

        public CubeTerrainGenerator(int dimension)
        {
            this.dimension = dimension;
        }

        public Array3D<byte> Generate()
        {
            var data = new Array3D<byte>(dimension, dimension, dimension);

            for (var c1 = 0; c1 < dimension; c1++)
            {
                for (var c2 = 0; c2 < dimension; c2++)
                {
                    for (var c3 = 0; c3 < dimension; c3++)
                    {
                        data[c1, c2, c3] = (byte) BlockType.Dirt;
                    }
                }
            }

            return data;
        }
    }
}

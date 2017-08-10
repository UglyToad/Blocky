using System;
using Blocky.Util;

namespace Blocky.Environment.Terrain.TerrainGenerators
{
    public class HillBillyGenerator : ITerrainGenerator
    {
        private readonly int width;
        private readonly int depth;
        private readonly int height;
        private readonly int iteratirons;

        public HillBillyGenerator(int width, int depth, int iteratirons)
        {
            this.width = width;
            this.depth = depth;

            height = (int)Math.Sqrt(width * width + depth * depth) / 10;

            this.iteratirons = iteratirons;
        }

        public Array3D<byte> Generate()
        {
            var result = new Array3D<byte>(width, depth, Math.Max(height, 2));

            var random = new Random();

            for (var i = 0; i < iteratirons; i++)
            {
                if (i == 0)
                {
                    for (var c1 = 0; c1 < width; c1++)
                    {
                        for (var c2 = 0; c2 < depth; c2++)
                        {
                            result[c1, c2, 0] = (byte)BlockType.Dirt;
                        }
                    }
                }
                else
                {
                    var hillParams = new IntPoint3D(random.Next(width), random.Next(depth), random.Next(height));

                    CreateHill(result, hillParams);
                }
            }

            return result;
        }

        private void CreateHill(Array3D<byte> data, IntPoint3D hillParams)
        {
            var c1Min = hillParams.X - hillParams.Z < 0 ? 0 : hillParams.X - hillParams.Z;
            var c1Max = Math.Min(hillParams.X + hillParams.Z, width - 1);

            var c2Min = hillParams.Y - hillParams.Z < 0 ? 0 : hillParams.Y - hillParams.Z;
            var c2Max = Math.Min(hillParams.Y + hillParams.Y, depth - 1);

            for (var c1 = c1Min; c1 <= c1Max; c1++)
            {
                for (var c2 = c2Min; c2 <= c2Max; c2++)
                {
                    var c3Max = (int)Math.Sqrt(hillParams.Z * hillParams.Z - (c1 - hillParams.X) * (c1 - hillParams.X) - (c2 - hillParams.Y) * (c2 - hillParams.Y));

                    if (c3Max <= 0) continue;

                    for (var i = 0; i < c3Max; i++)
                    {
                        data[c1, c2, i] = (byte)BlockType.Dirt;
                    }
                }
            }
        }
    }
}

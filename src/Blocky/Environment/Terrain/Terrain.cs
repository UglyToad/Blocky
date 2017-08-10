using System.Linq;
using Blocky.Entities.Camera;
using Blocky.Environment.Terrain.TerrainGenerators;
using Blocky.Util;
using Microsoft.Xna.Framework.Graphics;

namespace Blocky.Environment.Terrain
{
    public class Terrain : ITerrain
    {
        private readonly GraphicsDevice graphicsDevice;

        private readonly Array3D<byte> terrainData;

        private Block[] exposedBlocks;

        public Terrain(ITerrainGenerator generator, GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;

            terrainData = generator.Generate();

            RenderOuterBlocks();
        }

        public void AddBlockAt(IntPoint3D location, BlockType type)
        {
            terrainData[location] = (byte)type;
        }

        public void RenderOuterBlocks()
        {
            exposedBlocks = terrainData.GetOuterBlocks().Select(x => new Block(graphicsDevice, x)).ToArray();
        }

        public void Draw(BaseCamera camera)
        {
            foreach (var block in exposedBlocks)
            {
                block.Draw(camera);
            }
        }
    }
}

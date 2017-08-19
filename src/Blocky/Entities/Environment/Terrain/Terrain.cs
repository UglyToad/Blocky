using System.Linq;
using Blocky.Entities.Camera;
using Blocky.Entities.Environment.Terrain.TerrainGenerators;
using Blocky.Entities.Helpers;
using Blocky.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocky.Entities.Environment.Terrain
{
    public class Terrain : GameComponent, ITerrain
    {
        private readonly ITerrainGenerator generator;
        private readonly GraphicsDevice graphicsDevice;
        private readonly BaseCamera camera;

        private Array3D<byte> terrainData;

        private Block[] exposedBlocks;

        public Terrain(Game game, ITerrainGenerator generator, GraphicsDevice graphicsDevice, BaseCamera camera) : base(game)
        {
            this.generator = generator;
            this.graphicsDevice = graphicsDevice;
            this.camera = camera;
        }

        public void AddBlockAt(IntPoint3D location, BlockType type)
        {
            terrainData[location] = (byte)type;
        }

        public void RenderOuterBlocks()
        {
            exposedBlocks = terrainData.GetOuterBlocks().Select(x => new Block(graphicsDevice, x)).ToArray();
        }

        public override void Initialize()
        {
            terrainData = generator.Generate();

            RenderOuterBlocks();
        }

        public void LoadContent() { }

        public void Update(GameTime gameTime, UpdateChanges changes) { }

        public void Draw(GameTime gameTime)
        {
            foreach (var block in exposedBlocks)
            {
                block.Draw(camera);
            }
        }
    }
}

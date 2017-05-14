namespace Blocky.Environment
{
    using Entities;
    using Microsoft.Xna.Framework.Graphics;
    using Util;

    public class Terrain
    {
        private const int BlockSize = 1;

        private readonly ThreeDimensionalList<Block> items = new ThreeDimensionalList<Block>();

        public void AddBlockAt(IntPoint3D location, GraphicsDevice graphicsDevice)
        {
            items[location.X, location.Y, location.Z] = new Block(graphicsDevice, location.X*BlockSize*2,
                location.Y*BlockSize*2,
                location.Z*BlockSize*2);
        }

        public void Draw(BaseCamera camera)
        {
            foreach (var block in items)
            {
                block?.Draw(camera);
            }
        }
    }
}

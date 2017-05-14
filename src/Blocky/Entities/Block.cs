namespace Blocky.Entities
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Block
    {
        private readonly GraphicsDevice graphicsDevice;
        private readonly BasicEffect effect;

        private readonly VertexPositionNormalTexture[] vertices;


        public Block(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            //vertices = CubeFactory.MakeCube();

            effect = new BasicEffect(graphicsDevice)
            {
                World = Matrix.Identity,
                TextureEnabled = true
            };
        }

        public void Draw(BaseCamera camera)
        {
            effect.Projection = camera.ProjectionMatrix;
            effect.View = camera.ViewSettings.ViewMatrix;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 12);
            }
        }
    }
}

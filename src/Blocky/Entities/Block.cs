namespace Blocky.Entities
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Block
    {
        private readonly GraphicsDevice graphicsDevice;
        private readonly BasicEffect effect;

        private readonly VertexBuffer buffer;


        public Block(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            var vertices = CubeFactory.GetCube(1);
            buffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);

            buffer.SetData(vertices);

            effect = new BasicEffect(graphicsDevice)
            {
                World = Matrix.Identity
            };
        }

        public void Draw(BaseCamera camera)
        {
            effect.Projection = camera.ProjectionMatrix;
            effect.View = camera.ViewSettings.ViewMatrix;
            effect.VertexColorEnabled = true;

            graphicsDevice.SetVertexBuffer(buffer);

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 12);
            }
        }
    }
}

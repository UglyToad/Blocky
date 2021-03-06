﻿using Blocky.Entities.Camera;
using Blocky.Entities.Helpers;
using Blocky.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocky.Entities.Environment
{
    public class Block
    {
        public const int BlockSize = 2;

        private readonly GraphicsDevice graphicsDevice;

        private readonly BasicEffect effect;
        private readonly VertexPositionColor[] vertices;

        public Vector3 Position { get; }

        public Block(GraphicsDevice graphicsDevice, IntPoint3D position)
        {
            this.graphicsDevice = graphicsDevice;

            Position = position.ToVector() * BlockSize;

            vertices = CubeFactory.GetCube(BlockSize, IntPoint3D.GetNeighbourPositions());

            effect = new BasicEffect(graphicsDevice);
        }

        public void Draw(BaseCamera camera)
        {
            effect.InitializeDrawEffect(camera);
            effect.World = Matrix.Identity * Matrix.CreateTranslation(Position);

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 12);
            }
        }
    }
}

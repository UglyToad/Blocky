namespace Blocky.Entities
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public static class CubeFactory
    {
        public static VertexPositionColor[] GetCube(int size)
        {
            var ninetyDegrees = -(float) Math.PI/2f;

            // Points which form the base of the cube.
            var face = new []
            {
                new Vector3(-size, 0, -size), 
                new Vector3(size, 0, -size), 
                new Vector3(-size, 0, size), 
                new Vector3(size, 0, -size),
                new Vector3(size, 0, size),
                new Vector3(-size, 0, size),
            };

            var result = new VertexPositionColor[36];

            var rotateX180Degrees = Matrix.CreateRotationX(MathHelper.Pi);
            var rotateX90Degrees = Matrix.CreateRotationX(ninetyDegrees);
            var rotateXNegative90Degrees = Matrix.CreateRotationX(-ninetyDegrees);
            var rotateZ90Degrees = Matrix.CreateRotationZ(ninetyDegrees);
            var rotateZNegative90Degrees = Matrix.CreateRotationZ(-ninetyDegrees);

            var offset = 0;

            // Base
            for (var i = 0; i < 3; i++)
            {
                // The generated plane faces into the cube so we flip it and move it down from the origin.
                result[i].Position = Vector3.Transform(face[i], rotateX180Degrees) - (Vector3.UnitY * size);
                result[i].Color = Color.ForestGreen;

                result[i + 3].Position = Vector3.Transform(face[i + 3], rotateX180Degrees) - (Vector3.UnitY * size);
                result[i + 3].Color = Color.ForestGreen;
            }

            offset += 6;

            // Top
            for (var i = 0; i < 3; i++)
            {
                // Move the plane up from the origin.
                result[i + offset].Position = face[i] + (Vector3.UnitY * size);
                result[i + offset].Color = Color.BlanchedAlmond;

                result[i + offset + 3].Position = face[i + 3] + (Vector3.UnitY * size);
                result[i + offset + 3].Color = Color.BlanchedAlmond;
            }

            offset += 6;

            for (var i = 0; i < 3; i++)
            {
                result[i + offset].Position = Vector3.Transform(face[i], rotateX90Degrees) - Vector3.UnitZ * size;
                result[i + offset].Color = Color.CadetBlue;

                result[i + offset + 3].Position = Vector3.Transform(face[i + 3], rotateX90Degrees) - Vector3.UnitZ * size;
                result[i + offset + 3].Color = Color.CadetBlue;
            }

            offset += 6;

            for (var i = 0; i < 3; i++)
            {
                result[i + offset].Position = Vector3.Transform(face[i], rotateXNegative90Degrees) + Vector3.UnitZ * size;
                result[i + offset].Color = Color.Yellow;
                
                result[i + offset + 3].Position = Vector3.Transform(face[i + 3], rotateXNegative90Degrees) + Vector3.UnitZ * size;
                result[i + offset + 3].Color = Color.Yellow;
            }

            offset += 6;

            for (var i = 0; i < 3; i++)
            {
                result[i + offset].Position = Vector3.Transform(face[i], rotateZ90Degrees) + Vector3.UnitX * size;
                result[i + offset].Color = Color.Crimson;

                result[i + offset + 3].Position = Vector3.Transform(face[i + 3], rotateZ90Degrees) + Vector3.UnitX * size;
                result[i + offset + 3].Color = Color.Crimson;
            }

            offset += 6;

            for (var i = 0; i < 3; i++)
            {
                result[i + offset].Position = Vector3.Transform(face[i], rotateZNegative90Degrees) - Vector3.UnitX * size;
                result[i + offset].Color = Color.GreenYellow;

                result[i + offset + 3].Position = Vector3.Transform(face[i + 3], rotateZNegative90Degrees) - Vector3.UnitX * size;
                result[i + offset + 3].Color = Color.GreenYellow;

            }

            return result;
        }

    }
}

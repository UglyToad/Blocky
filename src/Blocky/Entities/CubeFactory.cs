namespace Blocky.Entities
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public static class CubeFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// http://www.i-programmer.info/projects/119-graphics-and-games/1108-getting-started-with-3d-xna.html?start=4
        /// </remarks>
        public static VertexPositionNormalTexture[] MakeCube()
        {
            VertexPositionNormalTexture[] vertexes =
      new VertexPositionNormalTexture[36];

            Vector2 texcoords = new Vector2(0, 0);

            Vector3[] face = new Vector3[6];

            //TopLeft
            face[0] = new Vector3(-1, 1, 0);
            //BottomLeft
            face[1] = new Vector3(-1, -1, 0);
            //TopRight
            face[2] = new Vector3(1, 1, 0);
            //BottomLeft
            face[3] = new Vector3(-1, -1, 0);
            //BottomRight
            face[4] = new Vector3(1, -1, 0);
            //TopRight
            face[5] = new Vector3(1, 1, 0);

            //front face
            for (int i = 0; i <= 2; i++)
            {
                vertexes[i] =
                  new VertexPositionNormalTexture(
                       face[i] + Vector3.UnitZ,
                             Vector3.UnitZ, texcoords);
                vertexes[i + 3] =
                  new VertexPositionNormalTexture(
                       face[i + 3] + Vector3.UnitZ,
                             Vector3.UnitZ, texcoords);
            }

            //Back face
            for (int i = 0; i <= 2; i++)
            {
                vertexes[i + 6] =
                   new VertexPositionNormalTexture(
                        face[2 - i] - Vector3.UnitZ,
                            -Vector3.UnitZ, texcoords);
                vertexes[i + 6 + 3] =
                   new VertexPositionNormalTexture(
                        face[5 - i] - Vector3.UnitZ,
                            -Vector3.UnitZ, texcoords);
            }

            //left face
            Matrix rotY90 = Matrix.CreateRotationY(
                               -(float)Math.PI / 2f);
            for (int i = 0; i <= 2; i++)
            {
                vertexes[i + 12] =
                  new VertexPositionNormalTexture(
                     Vector3.Transform(face[i], rotY90)
                          - Vector3.UnitX,
                            -Vector3.UnitX, texcoords);
                vertexes[i + 12 + 3] =
                  new VertexPositionNormalTexture(
                 Vector3.Transform(face[i + 3], rotY90)
                         - Vector3.UnitX,
                           -Vector3.UnitX, texcoords);
            }

            //Right face
            for (int i = 0; i <= 2; i++)
            {
                vertexes[i + 18] =
                  new VertexPositionNormalTexture(
                   Vector3.Transform(face[2 - i], rotY90)
                    - Vector3.UnitX,
                     Vector3.UnitX, texcoords);
                vertexes[i + 18 + 3] =
                  new VertexPositionNormalTexture(
                  Vector3.Transform(face[5 - i], rotY90)
                  - Vector3.UnitX,
                     Vector3.UnitX, texcoords);
            }


            //Top face
            Matrix rotX90 = Matrix.CreateRotationX(
                                -(float)Math.PI / 2f);
            for (int i = 0; i <= 2; i++)
            {
                vertexes[i + 24] =
                  new VertexPositionNormalTexture(
                   Vector3.Transform(face[i], rotX90)
                    + Vector3.UnitY,
                    Vector3.UnitY, texcoords);
                vertexes[i + 24 + 3] =
                  new VertexPositionNormalTexture(
                   Vector3.Transform(face[i + 3], rotX90)
                    + Vector3.UnitY,
                    Vector3.UnitY, texcoords);
            }
            //Bottom face

            for (int i = 0; i <= 2; i++)
            {
                vertexes[i + 30] =
                 new VertexPositionNormalTexture(
                  Vector3.Transform(face[2 - i], rotX90)
                   - Vector3.UnitY,
                    -Vector3.UnitY, texcoords);
                vertexes[i + 30 + 3] =
                 new VertexPositionNormalTexture(
                  Vector3.Transform(face[5 - i], rotX90)
                   - Vector3.UnitY,
                    -Vector3.UnitY, texcoords);
            }
            return vertexes;
        }

        public static VertexPositionColor[] GetCube(int size)
        {
            var ninetyDegrees = -(float) Math.PI/2f;

            // Points which form the base of the cube.
            var face = new []
            {
                new Vector3(0, 0, 0), 
                new Vector3(size, 0, 0), 
                new Vector3(0, 0, size), 
                new Vector3(size, 0, 0),
                new Vector3(size, 0, size),
                new Vector3(0, 0, size),
            };

            var result = new VertexPositionColor[36];


            var oneEightyX = Matrix.CreateRotationX((float)Math.PI);
            int offset = 0;
            // Base
            for (int i = 0; i < 3; i++)
            {
                result[i].Position = Vector3.Transform(face[i], oneEightyX) + Vector3.UnitZ * size;
                result[i].Color = Color.Firebrick;

                result[i + 3].Position = Vector3.Transform(face[i + 3], oneEightyX) + Vector3.UnitZ * size;
                result[i + 3].Color = Color.Firebrick;
            }

            offset = 6;

            // Top
            for (int i = 0; i < 3; i++)
            {
                result[i + offset].Position = face[i] + (Vector3.UnitY * size);
                result[i + offset].Color = Color.BlanchedAlmond;

                result[i + offset + 3].Position = face[i + 3] + (Vector3.UnitY * size);
                result[i + offset + 3].Color = Color.BlanchedAlmond;
            }

            offset = 12;
            var rotationX = Matrix.CreateRotationX(ninetyDegrees);
            var otherrotationX = Matrix.CreateRotationX(-ninetyDegrees);
            var rotationZ = Matrix.CreateRotationZ(ninetyDegrees);

            for (int i = 0; i < 3; i++)
            {
                result[i + offset].Position = Vector3.Transform(face[i], rotationX);
                result[i + offset].Color = Color.CadetBlue;

                result[i + offset + 3].Position = Vector3.Transform(face[i + 3], rotationX);
                result[i + offset + 3].Color = Color.CadetBlue;
            }

            offset = 18;

            for (int i = 0; i < 3; i++)
            {
                result[i + offset].Position = Vector3.Transform(face[i], rotationZ) - Vector3.UnitX * size;
                result[i + offset].Color = Color.Chocolate;
            }

            return result;
        }

    }
}

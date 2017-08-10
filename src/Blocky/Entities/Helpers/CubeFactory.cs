using System.Collections.Generic;
using Blocky.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocky.Entities.Helpers
{
    public static class CubeFactory
    {
        private static readonly Dictionary<IntPoint3D, Vector3[]> Faces = new Dictionary<IntPoint3D, Vector3[]>() {
            { new IntPoint3D(-1, 0, 0), new[] {
                    new Vector3(-1 , -1, 1),
                    new Vector3(-1 , -1, -1),
                    new Vector3(-1 , 1, -1),
                    new Vector3(-1 , 1, -1),
                    new Vector3(-1 , 1, 1),
                    new Vector3(-1 , -1, 1)
                }
            },
            { new IntPoint3D(1, 0, 0), new[] {
                    new Vector3(1 , 1, -1),
                    new Vector3(1 , -1, -1),
                    new Vector3(1 , -1, 1),
                    new Vector3(1 , -1, 1),
                    new Vector3(1 , 1, 1),
                    new Vector3(1 , 1, -1)
                }
            },
            { new IntPoint3D(0, -1, 0), new[] {
                    new Vector3(-1 , -1, 1),
                    new Vector3(1 , -1, 1),
                    new Vector3(1 , -1, -1),
                    new Vector3(1 , -1, -1),
                    new Vector3(-1 , -1, -1),
                    new Vector3(-1 , -1, 1)
                }
            },
            { new IntPoint3D(0, 1, 0), new[] {
                    new Vector3(1 , 1, -1),
                    new Vector3(1 , 1, 1),
                    new Vector3(-1 , 1, 1),
                    new Vector3(-1 , 1, 1),
                    new Vector3(-1 , 1, -1),
                    new Vector3(1 , 1, -1)
                }
            },
            { new IntPoint3D(0, 0, -1), new[] {
                    new Vector3(-1 , -1, -1),
                    new Vector3(1 , -1, -1),
                    new Vector3(1 , 1, -1),
                    new Vector3(1 , 1, -1),
                    new Vector3(-1 , 1, -1),
                    new Vector3(-1 , -1, -1)
                }
            },
            { new IntPoint3D(0, 0, 1), new[] {
                    new Vector3(1 , 1, 1),
                    new Vector3(1 , -1, 1),
                    new Vector3(-1 , -1, 1),
                    new Vector3(-1 , -1, 1),
                    new Vector3(-1 , 1, 1),
                    new Vector3(1 , 1, 1)
                }
            }
        };

        private static readonly Dictionary<IntPoint3D, Color> Colors = new Dictionary<IntPoint3D, Color>() {
            { new IntPoint3D(-1, 0, 0), Color.GreenYellow},
            { new IntPoint3D(1, 0, 0), Color.Crimson},
            { new IntPoint3D(0, -1, 0), Color.DarkGreen},
            { new IntPoint3D(0, 1, 0), Color.ForestGreen},
            { new IntPoint3D(0, 0, -1), Color.CadetBlue},
            { new IntPoint3D(0, 0, 1), Color.Yellow}
        };

        public static VertexPositionColor[] GetCubeWithDefaultFaces(int size, Vector3 position)
        {
            return GetCube(size, position, IntPoint3D.GetNeighbourPositions());
        }

        /// <summary>
        /// Get cube vertices.
        /// </summary>
        /// <param name="size">Cube size</param>
        /// <param name="position">Position</param>
        /// <param name="visibleFaces">Face vectors. Should be normalized!</param>
        /// <returns></returns>
        public static VertexPositionColor[] GetCube(int size, Vector3 position, IntPoint3D[] visibleFaces)
        {
            var result = new List<VertexPositionColor>();

            foreach (var visibleFace in visibleFaces)
            {
                result.AddRange(GetFaceVerteces(visibleFace, size, position));
            }

            return result.ToArray();
        }

        private static IEnumerable<VertexPositionColor> GetFaceVerteces(IntPoint3D faceVector, int scale, Vector3 position)
        {
            var halfScale = scale / 2f;

            foreach (var vector in Faces[faceVector])
            {
                var result = new VertexPositionColor()
                {
                    Position = position + (vector * halfScale),
                    Color = Colors[faceVector]
                };

                yield return result;
            }
        }
    }
}

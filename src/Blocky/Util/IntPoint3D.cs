using Microsoft.Xna.Framework;

namespace Blocky.Util
{
    public struct IntPoint3D
    {
        public int X { get; }

        public int Y { get; }

        public int Z { get; }

        public IntPoint3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 ToVector()
        {
            return new Vector3(X, Y, Z);
        }

        public static IntPoint3D operator +(IntPoint3D a, IntPoint3D b)
        {
            return new IntPoint3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static IntPoint3D[] GetNeighbourPositions()
        {
            return new[]
            {
                new IntPoint3D(1, 0, 0),
                new IntPoint3D(-1, 0, 0),
                new IntPoint3D(0, 1, 0),
                new IntPoint3D(0, -1, 0),
                new IntPoint3D(0, 0, 1),
                new IntPoint3D(0, 0, -1),
            };
        }
    }
}

using System.Collections;
using System.Collections.Generic;

namespace Blocky.Util
{
    /// <summary>
    /// c1 = x
    /// c2 = z
    /// c3 = -y
    /// </summary>
    /// <typeparam name="T">Struct to store</typeparam>
    public class Array3D<T> : IEnumerable<T> where T : struct
    {
        public int Width { get; }
        public int Depth { get; }
        public int Height { get; }

        private readonly T[][][] data;

        public Array3D(int width, int depth, int height)
        {
            Width = width;
            Depth = depth;
            Height = height;

            data = new T[Width][][];

            for (var c1 = 0; c1 < Width; c1++)
            {
                var tmp = new T[Depth][];

                for (var c2 = 0; c2 < Depth; c2++)
                {
                    tmp[c2] = new T[Height];
                }

                data[c1] = tmp;
            }
        }

        public T this[int c1, int c2, int c3]
        {
            get => data[c1][c2][c3];
            set => data[c1][c2][c3] = value;
        }

        public T[] this[int c1, int c2]
        {
            get => data[c1][c2];
            set => data[c1][c2] = value;
        }

        public T this[IntPoint3D vector]
        {
            get => data[vector.X][vector.Z][vector.Y];
            set => data[vector.X][vector.Z][vector.Y] = value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var c1 = 0; c1 < Width; c1++)
            {
                for (var c2 = 0; c2 < Depth; c2++)
                {
                    for (var c3 = 0; c3 < Height; c3++)
                    {
                        yield return data[c1][c2][c3];
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

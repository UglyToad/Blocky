namespace Blocky.Environment
{
    using System;
    using System.Collections.Generic;
    using Entities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Terrain
    {
        private const int BlockSize = 1;

        private readonly List<List<List<Block>>> terrain = new List<List<List<Block>>>();

        public void AddBlockAt(Vector3 location, GraphicsDevice graphicsDevice)
        {
            var column = (int)location.X;
            var row = (int)location.Y;

            EnsureListHasIndex(terrain, column);

            var columnList = terrain[column];

            EnsureListHasIndex(columnList, row);

            var rowList = columnList[row];

            var z = (int) location.Z;
            EnsureListHasItems(rowList, (int) location.Z);

            rowList[z] = new Block(graphicsDevice, (int)location.X * BlockSize * 2, 
                (int)location.Y * BlockSize * 2, 
                (int)location.Z * BlockSize * 2);
        }

        private static void EnsureListHasItems<T>(List<T> list, int index) where T : class
        {
            if (list.Count > index)
            {
                return;
            }

            var missingCount = index - (list.Count - 1);

            var target = list.Count + missingCount;
            for (int i = Math.Max(0, list.Count - 1); i < target; i++)
            {
                list.Add(null);
            }
        }

        private static void EnsureListHasIndex<T>(List<T> list, int index) where T : new() 
        {
            if (list.Count > index)
            {
                return;
            }

            var missingCount = index - (list.Count - 1);

            var target = list.Count + missingCount;
            for (int i = Math.Max(0, list.Count - 1); i < target; i++)
            {
                list.Add(new T());
            }
        }

        public void Draw(BaseCamera camera)
        {
            foreach (var column in terrain)
            {
                foreach (var row in column)
                {
                    foreach (var block in row)
                    {
                        block?.Draw(camera);
                    }
                }
            }
        }
    }
}

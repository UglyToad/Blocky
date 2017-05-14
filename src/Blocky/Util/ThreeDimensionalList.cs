namespace Blocky.Util
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    public class ThreeDimensionalList<T> : IEnumerable<T> where T : class
    {
        private readonly InfiniteList<InfiniteList<InfiniteList<T>>> content;

        [CanBeNull]
        public T this[int x, int y, int z]
        {
            get
            {
                var row = content[x];

                var column = row?[y];

                return column?[z];
            }
            set
            {
                var xList = content[x];

                if (xList == null)
                {
                    content[x] = new InfiniteList<InfiniteList<T>>();
                    xList = content[x];
                }

                var yList = xList[y];

                if (yList == null)
                {
                    xList[y] = new InfiniteList<T>();
                    yList = xList[y];
                }

                yList[z] = value;
            }
        }

        public ThreeDimensionalList()
        {
            content = new InfiniteList<InfiniteList<InfiniteList<T>>>();
        }


        public IEnumerator<T> GetEnumerator()
        {
            foreach (var xList in content)
            {
                if (xList == null)
                {
                    continue;
                }

                foreach (var yList in xList)
                {
                    if (yList == null)
                    {
                        continue;
                    }

                    foreach (var zItem in yList)
                    {
                        yield return zItem;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class InfiniteList<T> : IEnumerable<T> where T : class
    {
        private readonly List<T> positive;
        private readonly List<T> negative;

        [CanBeNull]
        public T this[int index]
        {
            get
            {
                if (index >= positive.Count)
                {
                    return null;
                }

                if (index < 0 && Math.Abs(index) > negative.Count)
                {
                    return null;
                }

                if (index >= 0)
                {
                    return positive[index];
                }

                return negative[Math.Abs(index) - 1];
            }
            set
            {
                var list = index >= 0 ? positive : negative;
                var targetIndex = index >= 0 ? index : Math.Abs(index) - 1;

                PlaceItemAtIndex(list, targetIndex, value);
            }
        }

        public InfiniteList()
        {
            positive = new List<T>();
            negative = new List<T>();
        }

        private void PlaceItemAtIndex(List<T> list, int index, T item)
        {
            if (list.Count > index)
            {
                list[index] = item;
                return;
            }

            var missingCount = index - (list.Count - 1);
            
            for (int i = 0; i < missingCount; i++)
            {
                list.Add(null);
            }

            list[index] = item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (negative.Count > 0)
            {
                for (int i = negative.Count - 1; i >= 0; i--)
                {
                    yield return negative[i];
                }
            }

            for (int i = 0; i < positive.Count; i++)
            {
                yield return positive[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}

namespace Blocky.Util
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    public class ThreeDimensionalList<T> : IEnumerable<T> where T : class
    {
        private readonly InfiniteCollection<InfiniteCollection<InfiniteCollection<T>>> content;

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
                    content[x] = new InfiniteCollection<InfiniteCollection<T>>();
                    xList = content[x];
                }

                var yList = xList[y];

                if (yList == null)
                {
                    xList[y] = new InfiniteCollection<T>();
                    yList = xList[y];
                }

                yList[z] = value;
            }
        }

        public ThreeDimensionalList()
        {
            content = new InfiniteCollection<InfiniteCollection<InfiniteCollection<T>>>();
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

    public class InfiniteCollection<T> : IEnumerable<T>
    {
        private readonly List<T> positive;
        private readonly List<T> negative;

        public InfiniteCollection()
        {
            positive = new List<T>();
            negative = new List<T>();
        }

        public T this[int index]
        {
            get { return List(index).ElementAtOrDefault(Math.Abs(index)); }
            set { Add(value, index); }
        }

    private List<T> List(int index) => index < 0 ? negative : positive;

        private void Add(T item, int index)
        {
            var list = List(index);
            var missingCount = Math.Abs(index) - (list.Count - 1);
            if (missingCount > 0)
            {
                list.AddRange(Enumerable.Repeat(default(T), missingCount));
            }
            
            list[Math.Abs(index)] = item;
            return;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = negative.Count - 1; i > 0; i--)
            {
                yield return negative[i];
            }

            for (var i = 0; i < positive.Count; i++)
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

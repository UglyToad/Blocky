namespace Blocky.Util
{
    using System.Collections;
    using System.Collections.Generic;

    public class Collection3D<T> : IEnumerable<T>
    {
        private readonly SortedDictionary<int, SortedDictionary<int, SortedDictionary<int, T>>> xdic;

        public Collection3D()
        {
            xdic = new SortedDictionary<int, SortedDictionary<int, SortedDictionary<int, T>>>();
        }

        public T this[int x, int y, int z]
        {
            get
            {
                SortedDictionary<int, SortedDictionary<int, T>> ydic;
                SortedDictionary<int, T> zdic;
                T value;

                return
                    xdic.TryGetValue(x, out ydic) &&
                    ydic.TryGetValue(y, out zdic) &&
                    zdic.TryGetValue(z, out value)
                        ? value
                        : default(T);
            }
            set
            {
                SortedDictionary<int, SortedDictionary<int, T>> ydic;
                if (!xdic.TryGetValue(x, out ydic))
                {
                    xdic[x] = (ydic = new SortedDictionary<int, SortedDictionary<int, T>>());
                }

                SortedDictionary<int, T> zdic;
                if (!ydic.TryGetValue(y, out zdic))
                {
                    ydic[y] = (zdic = new SortedDictionary<int, T>());
                }

                zdic[x] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var ydic in xdic)
            {
                foreach (var zdic in ydic.Value)
                {
                    foreach (var pair in zdic.Value)
                    {
                        yield return pair.Value;
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

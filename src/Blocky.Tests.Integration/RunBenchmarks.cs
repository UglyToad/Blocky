namespace Blocky.Tests.Integration
{
    using System;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Running;
    using Util;

    public class RunBenchmarks
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<Collection3DVsInfiniteList>();

            Console.ReadLine();
        }
    }

    public class Collection3DVsInfiniteList
    {
        private readonly Collection3D<TestItem> _rCollection3D;
        private readonly ThreeDimensionalList<TestItem> _r3DimensionalList;

        public Collection3DVsInfiniteList()
        {
            var collection = new Collection3D<TestItem>();

            for (int x = -64; x < 32; x++)
            {
                for (int y = -128; y < 64; y++)
                {
                    for (int z = -64; z < 64; z++)
                    {
                        if (x % 3 == 0 || x % 5 == 0 || x % 2 == 0)
                        {
                            if (y % 3 == 0 || y % 4 == 0 || y % 6 == 0 || y % 7 == 0)
                            {
                                if (z % 11 != 0)
                                {
                                    collection[x, y, z] = new TestItem
                                    {
                                        Id = x * y / z,
                                        Offset = x * y * z
                                    };
                                }
                            }
                        }
                    }
                }
            }

            var threeDimensionalList = new ThreeDimensionalList<TestItem>();

            for (int x = -64; x < 32; x++)
            {
                for (int y = -128; y < 64; y++)
                {
                    for (int z = -64; z < 64; z++)
                    {
                        if (x % 3 == 0 || x % 5 == 0 || x % 2 == 0)
                        {
                            if (y % 3 == 0 || y % 4 == 0 || y % 6 == 0 || y % 7 == 0)
                            {
                                if (z % 11 != 0)
                                {
                                    collection[x, y, z] = new TestItem
                                    {
                                        Id = x * y / z,
                                        Offset = x * y * z
                                    };
                                }
                            }
                        }
                    }
                }
            }

            _rCollection3D = collection;
            _r3DimensionalList = threeDimensionalList;
        }

        [Benchmark]
        public void Collection3DRun()
        {
            var collection = new Collection3D<TestItem>();

            for (int x = -64; x < 64; x++)
            {
                for (int y = -128; y < 128; y++)
                {
                    for (int z = -64; z < 64; z++)
                    {
                        if (x % 3 == 0 || x % 5 == 0 || x % 2 == 0)
                        {
                            if (y % 3 == 0 || y % 4 == 0 || y % 6 == 0 || y % 7 == 0)
                            {
                                if (z % 11 != 0)
                                {
                                    collection[x, y, z] = new TestItem
                                    {
                                        Id = x * y / z,
                                        Offset = x * y * z
                                    };
                                }
                            }
                        }
                    }
                }
            }
        }

        [Benchmark]
        public void InfiniteListRun()
        {
            var infiniteList = new ThreeDimensionalList<TestItem>();

            for (int x = -64; x < 64; x++)
            {
                for (int y = -128; y < 128; y++)
                {
                    for (int z = -64; z < 64; z++)
                    {
                        if (x % 3 == 0 || x % 5 == 0 || x % 2 == 0)
                        {
                            if (y % 3 == 0 || y % 4 == 0 || y % 6 == 0 || y % 7 == 0)
                            {
                                if (z % 11 != 0)
                                {
                                    infiniteList[x, y, z] = new TestItem
                                    {
                                        Id = x * y / z,
                                        Offset = x * y * z
                                    };
                                }
                            }
                        }
                    }
                }
            }
        }

        [Benchmark]
        public void Collection3DGet()
        {
            for (int x = 63; x > -128; x--)
            {
                for (int y = -128; y < 79; y += 3)
                {
                    for (int z = 0; z < 60; z++)
                    {
                        var i = _rCollection3D[x, y, z];
                    }
                }
            }
        }

        [Benchmark]
        public void InfiniteListGet()
        {
            for (int x = 63; x > -128; x--)
            {
                for (int y = -128; y < 79; y += 3)
                {
                    for (int z = 0; z < 60; z++)
                    {
                        var i = _r3DimensionalList[x, y, z];
                    }
                }
            }
        }
    }

    public class TestItem
    {
        public int Id { get; set; }

        public double Offset { get; set; }
    }
}

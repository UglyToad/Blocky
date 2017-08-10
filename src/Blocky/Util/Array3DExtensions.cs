using System.Collections.Generic;

namespace Blocky.Util
{
    public static class Array3DExtensions
    {
        public static IEnumerable<IntPoint3D> GetOuterBlocks(this Array3D<byte> data)
        {
            for (var c1 = 0; c1 < data.Width; c1++)
            {
                for (var c2 = 0; c2 < data.Depth; c2++)
                {
                    for (var c3 = 0; c3 < data.Height; c3++)
                    {
                        if (data[c1, c2, c3] == 0) continue;

                        var hasExposedFace = false;

                        if (c1 != 0)
                        {
                            hasExposedFace |= data[c1 - 1, c2, c3] != 0;
                        }

                        if (c1 != data.Width - 1)
                        {
                            hasExposedFace |= data[c1 + 1, c2, c3] != 0;
                        }

                        if (c2 != 0)
                        {
                            hasExposedFace |= data[c1, c2 - 1, c3] != 0;
                        }

                        if (c2 != data.Depth - 1)
                        {
                            hasExposedFace |= data[c1, c2 + 1, c3] != 0;
                        }

                        if (c3 != 0)
                        {
                            hasExposedFace |= data[c1, c2, c3 - 1] != 0;
                        }

                        if (c3 != data.Height - 1)
                        {
                            hasExposedFace |= data[c1, c2, c3 + 1] != 0;
                        }

                        if (hasExposedFace)
                        {
                            yield return new IntPoint3D(c1, c3, c2);
                        }
                    }
                }
            }
        }
    }
}

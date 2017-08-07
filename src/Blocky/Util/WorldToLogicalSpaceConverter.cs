namespace Blocky.Util
{
    using System;

    public static class WorldToLogicalSpaceConverter
    {
        /// <summary>
        /// Takes a point in 3d space and convert it to the underlying logical block index, based on the block size.
        /// </summary>
        /// <param name="x">The point in the x-dimension.</param>
        /// <param name="y">The point in the y-dimension.</param>
        /// <param name="z">The point in the z-dimension.</param>
        /// <param name="blockSize">The size of a block.</param>
        /// <returns>The coordinates of the point in the logical integer based world model.</returns>
        public static IntPoint3D Convert(float x, float y, float z, int blockSize)
        {
            return new IntPoint3D(ConvertSingle(x, blockSize), ConvertSingle(y, blockSize), ConvertSingle(z, blockSize));
        }

        /// <summary>
        /// Converts without knowledge of dimensions.
        /// </summary>
        public static int ConvertSingle(float value, int blockSize)
        {
            var range = blockSize * 2;

            var modulo = (value + blockSize) % range;

            var bracket = (int)(value + blockSize - double.Epsilon) / range;
            
            if (Math.Abs(modulo) < float.Epsilon)
            {
                bracket--;
            }

            return bracket;
        }
    }
}

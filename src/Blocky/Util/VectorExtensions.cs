using Microsoft.Xna.Framework;

namespace Blocky.Util
{
    public static class VectorExtensions
    {
        public static IntPoint3D ToIntPoint3D(this Vector3 vector)
        {
            return new IntPoint3D((int)vector.X, (int)vector.Y, (int)vector.Z);
        }
    }
}

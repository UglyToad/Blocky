using Blocky.Util;

namespace Blocky.Environment.Terrain.TerrainGenerators
{
    public interface ITerrainGenerator
    {
        Array3D<byte> Generate();
    }
}

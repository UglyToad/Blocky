using Blocky.Util;

namespace Blocky.Entities.Environment.Terrain.TerrainGenerators
{
    public interface ITerrainGenerator
    {
        Array3D<byte> Generate();
    }
}

using Blocky.Util;

namespace Blocky.Entities.Environment.Terrain
{
    public interface ITerrain : IEntity
    {
        void AddBlockAt(IntPoint3D location, BlockType type);

        void RenderOuterBlocks();
    }
}

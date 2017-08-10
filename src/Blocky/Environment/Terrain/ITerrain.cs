using Blocky.Entities;
using Blocky.Entities.Camera;
using Blocky.Util;

namespace Blocky.Environment.Terrain
{
    public interface ITerrain
    {
        void AddBlockAt(IntPoint3D location, BlockType type);

        void RenderOuterBlocks();

        void Draw(BaseCamera camera);
    }
}

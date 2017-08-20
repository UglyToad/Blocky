using Blocky.Util;

namespace Blocky.Entities.Environment.Logical
{
    public struct LogicalBlock
    {
        public IntPoint3D Location { get; }

        public short Type { get; }

        public bool IsUserPlaced { get; }

        public LogicalBlock(IntPoint3D location, short type, bool isUserPlaced)
        {
            Location = location;
            Type = type;
            IsUserPlaced = isUserPlaced;
        }
    }
}

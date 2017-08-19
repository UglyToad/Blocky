using Blocky.Entities.Helpers;
using Microsoft.Xna.Framework;

namespace Blocky.Entities
{
    public interface IEntity
    {
        void Initialize();

        void LoadContent();

        void Update(GameTime gameTime, UpdateChanges changes);

        void Draw(GameTime gameTime);

        bool IsOccupied(Vector3 position);
    }
}

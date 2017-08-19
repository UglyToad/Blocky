using Blocky.Entities.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocky.Entities.Camera
{
    public abstract class BaseCamera : GameComponent, IEntity
    {
        protected readonly GraphicsDevice GraphicsDevice;

        public ViewMatrixSettings ViewSettings { get; }

        public Matrix ProjectionMatrix
        {
            get
            {
                const float fieldOfView = MathHelper.PiOver4;
                const float nearClipPlane = 1;
                const float farClipPlane = 500;
                var aspectRatio = GraphicsDevice.Viewport.AspectRatio;

                return Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
            }
        }

        protected BaseCamera(Game game, GraphicsDevice graphicsDevice, ViewMatrixSettings viewMatrixSettings) : base(game)
        {
            GraphicsDevice = graphicsDevice;
            ViewSettings = viewMatrixSettings;
        }

        public override void Initialize() { }

        public void LoadContent() { }

        public abstract void Update(GameTime gameTime, UpdateChanges changes);

        public void Draw(GameTime gameTime) { }

    }
}

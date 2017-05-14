namespace Blocky.Entities
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class BaseCamera
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

        protected BaseCamera(GraphicsDevice graphicsDevice, ViewMatrixSettings viewMatrixSettings)
        {
            GraphicsDevice = graphicsDevice;
            ViewSettings = viewMatrixSettings;
        }
    }
}

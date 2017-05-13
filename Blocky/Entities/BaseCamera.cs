namespace Blocky.Entities
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class BaseCamera
    {
        protected readonly GraphicsDevice graphicsDevice;

        public ViewMatrixSettings ViewSettings { get; }

        public Matrix ProjectionMatrix
        {
            get
            {
                float fieldOfView = MathHelper.PiOver4;
                float nearClipPlane = 1;
                float farClipPlane = 500;
                float aspectRatio = graphicsDevice.Viewport.AspectRatio;

                return Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
            }
        }

        protected BaseCamera(GraphicsDevice graphicsDevice, ViewMatrixSettings viewMatrixSettings)
        {
            this.graphicsDevice = graphicsDevice;
            ViewSettings = viewMatrixSettings;
        }
    }

    public class FirstPersonCamera : BaseCamera
    {
        private Vector3 cameraReference;
        private float leftRightRotation;
        private float upDownRotation;

        private float rotationSpeed = 0.005f;
        private float speed = 1f;

        public FirstPersonCamera(GraphicsDevice graphicsDevice, ViewMatrixSettings viewMatrixSettings) : base(graphicsDevice, viewMatrixSettings)
        {
            cameraReference = viewMatrixSettings.Target;
        }

        public void Update(Vector3 translation, float leftRightRotation, float upDownRotation)
        {
            this.leftRightRotation += leftRightRotation*rotationSpeed;
            this.upDownRotation += upDownRotation*rotationSpeed;

            var rotationMatrix = Matrix.CreateRotationX(this.upDownRotation)*
                                 Matrix.CreateRotationY(this.leftRightRotation);

            Vector3 transformed = Vector3.Transform(cameraReference, rotationMatrix);

            this.ViewSettings.Position += Vector3.Transform(translation, rotationMatrix)*speed;
            this.ViewSettings.Target = transformed + this.ViewSettings.Position;
        }
    }
}

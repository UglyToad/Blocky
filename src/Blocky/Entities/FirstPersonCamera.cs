namespace Blocky.Entities
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class FirstPersonCamera : BaseCamera
    {
        /// <summary>
        /// Maintain a reference to the original direction so we can translate the direction relative to the original in all future updates.
        /// </summary>
        private readonly Vector3 cameraReference;

        /// <summary>
        /// The left-right rotation of the view (rotates around the Y/vertical axis).
        /// </summary>
        private float leftRightRotation;
        private float upDownRotation;

        private const float RotationSpeed = 0.005f;
        private const float MovementSpeed = 0.5f;

        public FirstPersonCamera(GraphicsDevice graphicsDevice, ViewMatrixSettings viewMatrixSettings) : base(graphicsDevice, viewMatrixSettings)
        {
            cameraReference = viewMatrixSettings.Target;
        }

        public void Update(Vector3 translation, float leftRight, float upDown)
        {
            leftRightRotation += leftRight*RotationSpeed;
            upDownRotation += upDown*RotationSpeed;

            var rotationMatrix = Matrix.CreateRotationY(leftRightRotation);
            var rotationMatrix2 = Matrix.CreateRotationX(upDownRotation) * Matrix.CreateRotationY(leftRightRotation);

            Vector3 transformed = Vector3.Transform(cameraReference, rotationMatrix2);
            
            ViewSettings.Position += Vector3.Transform(translation, rotationMatrix)*MovementSpeed;

            ViewSettings.Target = transformed + ViewSettings.Position;
        }
    }
}
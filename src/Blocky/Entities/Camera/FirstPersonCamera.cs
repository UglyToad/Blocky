using Blocky.Entities.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocky.Entities.Camera
{
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

        public FirstPersonCamera(Game game, GraphicsDevice graphicsDevice, ViewMatrixSettings viewMatrixSettings) : base(game, graphicsDevice, viewMatrixSettings)
        {
            cameraReference = viewMatrixSettings.Target;
        }

        public override void Update(GameTime gameTime, UpdateChanges changes)
        {
            leftRightRotation += changes.LeftRightRotation * RotationSpeed;
            upDownRotation += changes.UpDownRotation * RotationSpeed;

            var positionRotation = Matrix.CreateRotationY(leftRightRotation);

            var targetRotation = Matrix.CreateRotationX(upDownRotation) * Matrix.CreateRotationY(leftRightRotation);

            var potentialPosition = ViewSettings.Position + Vector3.Transform(changes.ChangeVector, positionRotation) * MovementSpeed;

            if (changes.IsOccupied(potentialPosition - new Vector3(0, 6, 0)))
            {
                changes.MidJump = false;
            }
            else
            {
                ViewSettings.Position = potentialPosition;
            }

            ViewSettings.Target = Vector3.Transform(cameraReference, targetRotation) + ViewSettings.Position;
        }
    }
}
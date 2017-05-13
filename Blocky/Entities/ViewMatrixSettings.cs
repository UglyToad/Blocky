namespace Blocky.Entities
{
    using Microsoft.Xna.Framework;

    public class ViewMatrixSettings
    {
        /// <summary>
        /// The current position of the camera.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// The vertical axis of the camera.
        /// </summary>
        public Vector3 UpVector { get; set; }

        /// <summary>
        /// The direction the camera is pointing.
        /// </summary>
        public Vector3 Target { get; set; }

        public Matrix ViewMatrix => Matrix.CreateLookAt(Position, Target, UpVector);

        public ViewMatrixSettings(Vector3 position, Vector3 upVector, Vector3 target)
        {
            Position = position;
            UpVector = upVector;
            Target = target;
        }
    }
}

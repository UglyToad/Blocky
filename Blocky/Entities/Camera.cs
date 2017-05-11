using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Blocky.Entities
{
    using System;
    using Microsoft.Xna.Framework.Input;

    public class Camera
    {
        private readonly GraphicsDevice graphicsDevice;

        // Let's start at X = 0 so we're looking at things head-on
        private Vector3 position = new Vector3(0, -20, 10);

        private float angleZ;
        private float anglevertical;
        private MouseState mouseState;
        private Vector3 upVector;
        private Vector3 direction;

        public RotationStates RotationStates { get; } = new RotationStates();

        public Matrix ViewMatrix
        {
            get
            {
                var lookAtVector = direction;
                // We'll create a rotation matrix using our angleZ
                var rotationMatrix = Matrix.CreateRotationZ(angleZ);
                
                // Then we'll modify the vector using this matrix:
                lookAtVector = Vector3.Transform(lookAtVector, rotationMatrix);
                lookAtVector += position;

                return Matrix.CreateLookAt(position, position + direction, upVector);
            }
        }

        public Matrix ProjectionMatrix
        {
            get
            {
                float fieldOfView = MathHelper.PiOver4;
                float nearClipPlane = 1;
                float farClipPlane = 500;
                float aspectRatio = graphicsDevice.Viewport.Width / (float)graphicsDevice.Viewport.Height;

                return Matrix.CreatePerspectiveFieldOfView(
                    fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
            }
        }

        public Camera(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            Mouse.SetPosition(graphicsDevice.Viewport.Width/2, graphicsDevice.Viewport.Height/2);
            mouseState = Mouse.GetState();
            upVector = Vector3.UnitZ;
            direction = new Vector3(0, 1, 0);
        }
       
        public void Update(GameTime gameTime)
        {
            const float speed = 0.5f;

            var state = Keyboard.GetState();

            var changeVector = new Vector3();

            if (state.IsKeyDown(Keys.W))
                position += direction * speed;
            if (state.IsKeyDown(Keys.S))
                position -= direction*speed;
            if (state.IsKeyDown(Keys.A))
            {
                position += Vector3.Cross(upVector, direction) * speed;
            }
            if (state.IsKeyDown(Keys.D))
            {
                position -= Vector3.Cross(upVector, direction) * speed;
            }

            var currentMouseState = Mouse.GetState();

            RotationStates.X = currentMouseState.X - mouseState.X;
            RotationStates.Y = currentMouseState.Y - mouseState.Y;

            if (RotationStates.Y < -0.5)
            {
                anglevertical += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if(RotationStates.Y > 0.5f)
            {
                anglevertical -= (float) gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (RotationStates.X < -0.5f)
            {
                angleZ += (float)gameTime.ElapsedGameTime.TotalSeconds * Math.Abs(RotationStates.X)/25;
            }
            else if (RotationStates.X > 0.5f)
            {
                angleZ -= (float) gameTime.ElapsedGameTime.TotalSeconds*Math.Abs(RotationStates.X)/25;
            }

            var rotationMatrix = Matrix.CreateRotationZ(angleZ);
            var forwardVector = Vector3.Transform(changeVector, rotationMatrix * 2);
            
            position += forwardVector;
            
            Mouse.SetPosition(graphicsDevice.Viewport.Width/2, graphicsDevice.Viewport.Height/2);
            mouseState = Mouse.GetState();
        }
        
    }
}

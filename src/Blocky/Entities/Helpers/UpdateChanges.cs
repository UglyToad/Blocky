using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blocky.Entities.Helpers
{
    public class UpdateChanges
    {
        private readonly IEntity[] entities;
        private readonly GraphicsDevice graphicsDevice;

        public KeyboardState KeyboardState { get; private set; }

        public MouseState CurrentMouseState { get; private set; }

        private bool mouseLocked = true;

        public int LeftRightRotation { get; private set; }

        public int UpDownRotation { get; private set; }

        public Vector3 ChangeVector { get; private set; }

        private TimeSpan prevTime;

        public bool MidJump { get; set; }

        private bool jumpGoingUp;

        private int jumpTicks;

        public UpdateChanges(IEntity[] entities, GraphicsDevice graphicsDevice)
        {
            this.entities = entities;
            this.graphicsDevice = graphicsDevice;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState = Keyboard.GetState();

            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                mouseLocked = false;
            }

            UpdateMouse();

            ChangeVector = ApplyGravity(GetChangeVector(), gameTime);
        }

        private void UpdateMouse()
        {
            if (mouseLocked)
            {
                Mouse.SetCursor(MouseCursor.No);
                const int lockX = 0;
                const int lockY = 0;

                CurrentMouseState = Mouse.GetState();

                Mouse.SetPosition(lockX, lockY);

                LeftRightRotation = -(CurrentMouseState.X - lockX);

                UpDownRotation = -(CurrentMouseState.Y - lockY);
            } else if (IsMouseInsideWindow())
            {
                Mouse.SetCursor(MouseCursor.Arrow);

                var state = Mouse.GetState();

                if (state.LeftButton == ButtonState.Pressed) mouseLocked = true;
            }
        }

        private bool IsMouseInsideWindow()
        {
            var state = Mouse.GetState();

            var position = new Point(state.X, state.Y);

            return graphicsDevice.Viewport.Bounds.Contains(position);
        }

        private Vector3 ApplyGravity(Vector3 changeVector, GameTime gameTime)
        {
            if (gameTime.TotalGameTime <= prevTime + TimeSpan.FromMilliseconds(100)) return changeVector;

            prevTime = gameTime.TotalGameTime;

            if (jumpGoingUp)
            {
                jumpTicks++;
                changeVector.Y = 3f;

                if (jumpTicks != 3) return changeVector;

                jumpGoingUp = false;
                jumpTicks = 0;
            }
            else
            {
                changeVector.Y = -3f;
            }

            return changeVector;
        }

        public bool IsOccupied(Vector3 position)
        {
            var isOccupied = false;

            foreach (var entity in entities)
            {
                isOccupied |= entity.IsOccupied(position);
            }

            return isOccupied;
        }

        private Vector3 GetChangeVector()
        {
            var changeVector = new Vector3();

            if (KeyboardState.IsKeyDown(Keys.W))
            {
                changeVector.Z -= 1;
            }
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                changeVector.Z += 1;
            }
            if (KeyboardState.IsKeyDown(Keys.A))
            {
                changeVector.X -= 1;
            }
            if (KeyboardState.IsKeyDown(Keys.D))
            {
                changeVector.X += 1;
            }
            if (KeyboardState.IsKeyDown(Keys.Space) && !MidJump)
            {
                MidJump = true;
                jumpGoingUp = true;
            }

            return changeVector;
        }
    }
}
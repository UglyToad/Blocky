using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Blocky.Entities.Helpers
{
    public class UpdateChanges
    {
        public KeyboardState KeyboardState { get; }

        public MouseState CurrentMouseState { get; }

        public MouseState PrevMouseState { get; }

        public UpdateChanges(KeyboardState keyboardState, MouseState currentMouseState, MouseState prevMouseState)
        {
            KeyboardState = keyboardState;

            CurrentMouseState = currentMouseState;

            PrevMouseState = prevMouseState;
        }

        public int GetLeftRightRotation => -(CurrentMouseState.X - PrevMouseState.X);

        public int GetUpDownRotation => -(CurrentMouseState.Y - PrevMouseState.Y);

        public Vector3 GetChangeVector()
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

            return changeVector;
        }
    }
}
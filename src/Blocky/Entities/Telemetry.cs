using Blocky.Entities.Camera;
using Blocky.Entities.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blocky.Entities
{
    public class Telemetry : GameComponent, IEntity
    {
        private readonly GraphicsDevice graphicsDevice;
        private readonly BaseCamera camera;

        private SpriteFont font;
        private SpriteBatch fontBatch;
        private string output = string.Empty;

        public Telemetry(
            Game game,
            GraphicsDevice graphicsDevice,
            BaseCamera camera) : base(game)
        {
            this.graphicsDevice = graphicsDevice;
            this.camera = camera;
        }

        public void LoadContent()
        {
            fontBatch = new SpriteBatch(graphicsDevice);
            font = Game.Content.Load<SpriteFont>("Courier New");
        }

        public void Update(GameTime gameTime, UpdateChanges changes)
        {
            if (changes.CurrentMouseState.LeftButton != ButtonState.Pressed) return;

            output = $"\r\nx:{camera.ViewSettings.Position.X} Y: {camera.ViewSettings.Position.Y} Z: {camera.ViewSettings.Position.Z}";

            var near = graphicsDevice.Viewport.Unproject(new Vector3(changes.CurrentMouseState.X, changes.CurrentMouseState.Y, 0f),
                camera.ProjectionMatrix,
                camera.ViewSettings.ViewMatrix, Matrix.Identity);
            var far = graphicsDevice.Viewport.Unproject(new Vector3(changes.CurrentMouseState.X, changes.CurrentMouseState.Y, 1f),
                camera.ProjectionMatrix,
                camera.ViewSettings.ViewMatrix, Matrix.Identity);
            var ray = new Ray(near, Vector3.Normalize(far - near));

            var groundPlane = new Plane(new Vector3(0, 1, 0), 0);

            float? result;
            ray.Intersects(ref groundPlane, out result);
            if (result != null)
            {
                Vector3 worldPoint = ray.Position + ray.Direction * result.Value;
            }
        }

        public void Draw(GameTime gameTime)
        {
            fontBatch.Begin();

            fontBatch.DrawString(font, output, new Vector2(20, 20), Color.LightGreen);

            fontBatch.End();
        }
    }
}

using Blocky.Entities.Camera;
using Blocky.Environment.Terrain;
using Blocky.Environment.Terrain.TerrainGenerators;

namespace Blocky
{
    using System;
    using Entities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Util;

    /*
     *  X, Y, Z
     *  Y is treated at the VERTICAL axis!
     */

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;

        private ITerrain terrain;

        private SpriteFont font;
        private SpriteBatch fontBatch;

        // New camera code
        private FirstPersonCamera camera;

        private Player player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1500,
                PreferredBackBufferHeight = 960
            };

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            player = new Player(this, new Vector3(0, 2, 0));
            player.Initialize();

            // New camera code
            camera = new FirstPersonCamera(graphics.GraphicsDevice,
                new ViewMatrixSettings(new Vector3(50, 50, -50), Vector3.Up, Vector3.Forward));

            IsMouseVisible = true;
            Mouse.SetPosition(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);

            var terrainGenerator = new HillBillyGenerator(100, 100, 50);

            terrain = new Terrain(terrainGenerator, GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            previousState = Mouse.GetState();
            fontBatch = new SpriteBatch(graphics.GraphicsDevice);
            font = Content.Load<SpriteFont>("Courier New");
        }

        private MouseState previousState;
        protected override void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();

            var changeVector = new Vector3();



            if (state.IsKeyDown(Keys.W))
                changeVector.Z -= 1;
            if (state.IsKeyDown(Keys.S))
                changeVector.Z += 1;
            if (state.IsKeyDown(Keys.A))
            {
                changeVector.X -= 1;
            }
            if (state.IsKeyDown(Keys.D))
            {
                changeVector.X += 1;
            }

            var currentState = Mouse.GetState();
            var leftRightRotation = -(currentState.X - previousState.X);
            var upDownRotation = -(currentState.Y - previousState.Y);

            player.Update(gameTime, changeVector, leftRightRotation);
            camera.Update(changeVector, leftRightRotation, upDownRotation);

            if (currentState.LeftButton == ButtonState.Pressed)
            {
                output =
                    //$"X: {camera.ViewSettings.ViewMatrix.Rotation.X} Y: {camera.ViewSettings.ViewMatrix.Rotation.Y} Z: {camera.ViewSettings.ViewMatrix.Rotation.Z}" +
                    $"\r\nx:{camera.ViewSettings.Position.X} Y: {camera.ViewSettings.Position.Y} Z: {camera.ViewSettings.Position.Z}";


                var near = GraphicsDevice.Viewport.Unproject(new Vector3(currentState.X, currentState.Y, 0f),
                    camera.ProjectionMatrix,
                    camera.ViewSettings.ViewMatrix, Matrix.Identity);
                var far = GraphicsDevice.Viewport.Unproject(new Vector3(currentState.X, currentState.Y, 1f),
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

            //camera.Update(gameTime);
            previousState = currentState;
            base.Update(gameTime);
        }

        private string output = string.Empty;
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            fontBatch.Begin();
            // 2d drawing

            fontBatch.DrawString(font, output, new Vector2(20, 20), Color.LightGreen);

            fontBatch.End();

            // reset rendering for 3d
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            // New camera code
            terrain.Draw(camera);
            player.Draw(camera);

            base.Draw(gameTime);
        }
    }
}

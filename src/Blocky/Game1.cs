namespace Blocky
{
    using System;
    using Entities;
    using Environment;
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

        private Terrain terrain;

        private SpriteFont font;
        private SpriteBatch fontBatch;

        // New camera code
        private FirstPersonCamera camera;

        private Robot robot;

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
            robot = new Robot();
            robot.Initialize(Content);

            // New camera code
            camera = new FirstPersonCamera(graphics.GraphicsDevice,
                new ViewMatrixSettings(new Vector3(0, 3, 10), Vector3.Up, Vector3.Forward));

            IsMouseVisible = true;
            Mouse.SetPosition(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);

            terrain = new Terrain();

            Random random = new Random();
            for (int x = -20; x < 20; x++)
            {
                for (int z = -20; z < 20; z++)
                {
                    terrain.AddBlockAt(new IntPoint3D(x, 0, z), GraphicsDevice);

                    if (random.Next(3) == 0)
                    {
                        terrain.AddBlockAt(new IntPoint3D(x, 1, z), GraphicsDevice);
                    }
                }
            }

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
            robot.Update(gameTime);
            // New camera code


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

            camera.Update(changeVector, leftRightRotation, upDownRotation);

            //camera.Update(gameTime);
            previousState = currentState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            fontBatch.Begin();
            // 2d drawing
            string output = "hi";// camera.RotationStates.X.ToString();

            fontBatch.DrawString(font, output, new Vector2(20, 20), Color.LightGreen);

            fontBatch.End();

            // reset rendering for 3d
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            // New camera code
            robot.Draw(camera);
            terrain.Draw(camera);

            base.Draw(gameTime);
        }
    }
}

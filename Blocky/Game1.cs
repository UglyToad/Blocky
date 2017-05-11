namespace Blocky
{
    using Entities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;

        private VertexPositionNormalTexture[] floorVerts;

        private BasicEffect effect;

        private Texture2D checkerboardTexture;

        private SpriteFont font;
        private SpriteBatch fontBatch;

        // New camera code
        private Camera camera;

        private Robot robot;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1500;
            graphics.PreferredBackBufferHeight = 960;


            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            floorVerts = new VertexPositionNormalTexture[6];

            floorVerts[0].Position = new Vector3(-20, -20, 0);
            floorVerts[1].Position = new Vector3(-20, 20, 0);
            floorVerts[2].Position = new Vector3(20, -20, 0);

            floorVerts[3].Position = floorVerts[1].Position;
            floorVerts[4].Position = new Vector3(20, 20, 0);
            floorVerts[5].Position = floorVerts[2].Position;

            int repetitions = 20;

            floorVerts[0].TextureCoordinate = new Vector2(0, 0);
            floorVerts[1].TextureCoordinate = new Vector2(0, repetitions);
            floorVerts[2].TextureCoordinate = new Vector2(repetitions, 0);

            floorVerts[3].TextureCoordinate = floorVerts[1].TextureCoordinate;
            floorVerts[4].TextureCoordinate = new Vector2(repetitions, repetitions);
            floorVerts[5].TextureCoordinate = floorVerts[2].TextureCoordinate;

            effect = new BasicEffect(graphics.GraphicsDevice);

            robot = new Robot();
            robot.Initialize(Content);

            // New camera code
            camera = new Camera(graphics.GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            fontBatch = new SpriteBatch(graphics.GraphicsDevice);
            checkerboardTexture = Content.Load<Texture2D>("checkerboard");
            font = Content.Load<SpriteFont>("Courier New");
        }

        protected override void Update(GameTime gameTime)
        {
            robot.Update(gameTime);
            // New camera code
            camera.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            
            fontBatch.Begin();
            // 2d drawing
            string output = camera.RotationStates.X.ToString();
           
            fontBatch.DrawString(font, output, new Vector2(20, 20), Color.LightGreen);
             
            fontBatch.End();

            // reset rendering for 3d
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
            DrawGround();

            // New camera code
            robot.Draw(camera);

            base.Draw(gameTime);
        }

        private void DrawGround()
        {
            // New camera code
            effect.View = camera.ViewMatrix;
            effect.Projection = camera.ProjectionMatrix;

            effect.TextureEnabled = true;
            effect.Texture = checkerboardTexture;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphics.GraphicsDevice.DrawUserPrimitives(
                            PrimitiveType.TriangleList,
                    floorVerts,
                    0,
                    2);
            }
        }
    }

    public class RotationStates
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }
    }
}

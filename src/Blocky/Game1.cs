namespace Blocky
{
    using Entities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

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
        
        private VertexPositionNormalTexture[] floorVertices;
        private VertexPositionColor[] makeCubeVertices;
        private Block block;

        private BasicEffect effect;
        private BasicEffect cubeEffect;

        private Texture2D checkerboardTexture;

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
            floorVertices = new VertexPositionNormalTexture[6];

            floorVertices[0].Position = new Vector3(-20,0, 20);
            floorVertices[1].Position = new Vector3(-20, 0, -20);
            floorVertices[2].Position = new Vector3(20, 0, 20);
            floorVertices[3].Position = floorVertices[1].Position;
            floorVertices[4].Position = new Vector3(20, 0, -20);
            floorVertices[5].Position = floorVertices[2].Position;

            int repetitions = 20;

            floorVertices[0].TextureCoordinate = new Vector2(0, 0);
            floorVertices[1].TextureCoordinate = new Vector2(0, repetitions);
            floorVertices[2].TextureCoordinate = new Vector2(repetitions, 0);

            floorVertices[3].TextureCoordinate = floorVertices[1].TextureCoordinate;
            floorVertices[4].TextureCoordinate = new Vector2(repetitions, repetitions);
            floorVertices[5].TextureCoordinate = floorVertices[2].TextureCoordinate;
        
            effect = new BasicEffect(graphics.GraphicsDevice);
            cubeEffect = new BasicEffect(graphics.GraphicsDevice);

            makeCubeVertices = CubeFactory.GetCube(3);
            robot = new Robot();
            robot.Initialize(Content);
            block = new Block(graphics.GraphicsDevice);

            // New camera code
            camera = new FirstPersonCamera(graphics.GraphicsDevice, 
                new ViewMatrixSettings(new Vector3(0, 3, 10), Vector3.Up, Vector3.Forward));

            IsMouseVisible = true;
            Mouse.SetPosition(Window.ClientBounds.Width/2, Window.ClientBounds.Height/2);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            previousState = Mouse.GetState();
            fontBatch = new SpriteBatch(graphics.GraphicsDevice);
            checkerboardTexture = Content.Load<Texture2D>("checkerboard");
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

            DrawMakeCube();
            //DrawGround();

            // New camera code
            //robot.Draw(camera);
           // block.Draw(camera);

            base.Draw(gameTime);
        }

        private void DrawMakeCube()
        {
            cubeEffect.View = camera.ViewSettings.ViewMatrix;
            cubeEffect.Projection = camera.ProjectionMatrix;
            cubeEffect.World = Matrix.Identity;
            cubeEffect.VertexColorEnabled = true;
            
            foreach (var pass in cubeEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, makeCubeVertices, 0, 12);
            }
        }

        private void DrawGround()
        {
            // New camera code
            effect.View = camera.ViewSettings.ViewMatrix;
            effect.Projection = camera.ProjectionMatrix;
            effect.World = Matrix.Identity;


            effect.TextureEnabled = true;
            effect.Texture = checkerboardTexture;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                graphics.GraphicsDevice.DrawUserPrimitives(
                            PrimitiveType.TriangleList,
                    floorVertices,
                    0,
                    2);
            }
        }
    }
}

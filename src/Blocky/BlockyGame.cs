using Blocky.Entities;
using Blocky.Entities.Camera;
using Blocky.Entities.Environment.Terrain;
using Blocky.Entities.Environment.Terrain.TerrainGenerators;
using Blocky.Entities.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blocky
{
    /// <summary>
    /// This is the main type for your game.
    /// 
    /// X, Y, Z
    /// Y is treated at the VERTICAL axis!
    /// </summary>
    public class BlockyGame : Game
    {
        private MouseState previousState;

        private IEntity[] entities;
        private readonly GraphicsDeviceManager graphics;

        public BlockyGame()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1500,
                PreferredBackBufferHeight = 960
            };

            Content.RootDirectory = "Content";
        }

        private void InitializeEntities()
        {
            var viewMatrixSettings = new ViewMatrixSettings(new Vector3(50, 100, 600), Vector3.Up, Vector3.Forward);
            var camera = new FirstPersonCamera(this, graphics.GraphicsDevice, viewMatrixSettings);

            var player = new Player(this, new Vector3(0, 2, 0), camera);

            var telemetry = new Telemetry(this, graphics, GraphicsDevice, camera);

            var terrainGenerator = new HillBillyGenerator(200, 200, 50);
            var terrain = new Terrain(this, terrainGenerator, GraphicsDevice, camera);

            entities = new IEntity[]
            {
                terrain,
                camera,
                player,
                telemetry
            };
        }

        protected override void Initialize()
        {
            InitializeEntities();

            IsMouseVisible = true;
            Mouse.SetPosition(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);

            foreach (var entity in entities)
            {
                entity.Initialize();
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            previousState = Mouse.GetState();

            foreach (var entity in entities)
            {
                entity.LoadContent();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            var keybordState = Keyboard.GetState();

            var currentState = Mouse.GetState();

            var updateChanges = new UpdateChanges(keybordState, currentState, previousState);

            foreach (var entity in entities)
            {
                entity.Update(gameTime, updateChanges);
            }

            previousState = currentState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            foreach (var entity in entities)
            {
                entity.Draw(gameTime);
            }

            base.Draw(gameTime);
        }
    }
}

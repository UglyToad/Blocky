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
        private IEntity[] entities;

        // ReSharper disable once NotAccessedField.Local
        private readonly GraphicsDeviceManager graphics;

        private UpdateChanges updateChanges;

        public BlockyGame()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1500,
                PreferredBackBufferHeight = 960
            };

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Bind all entities here.
        /// </summary>
        private void InitializeEntities()
        {
            var viewMatrixSettings = new ViewMatrixSettings(new Vector3(50, 50, 50), Vector3.Up, Vector3.Forward);
            var camera = new FirstPersonCamera(this, GraphicsDevice, viewMatrixSettings);

            var telemetry = new Telemetry(this, GraphicsDevice, camera);

            var terrainGenerator = new HillBillyGenerator(100, 100, 50);
            var terrain = new Terrain(this, GraphicsDevice, terrainGenerator, camera);

            entities = new IEntity[]
            {
                terrain,
                camera,
                telemetry
            };

            updateChanges = new UpdateChanges(entities, GraphicsDevice);
        }

        protected override void Initialize()
        {
            InitializeEntities();

            Mouse.SetPosition(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);

            foreach (var entity in entities)
            {
                entity.Initialize();
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            foreach (var entity in entities)
            {
                entity.LoadContent();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            updateChanges.Update(gameTime);

            foreach (var entity in entities)
            {
                entity.Update(gameTime, updateChanges);
            }

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

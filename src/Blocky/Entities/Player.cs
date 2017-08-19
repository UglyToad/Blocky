using Blocky.Entities.Camera;
using Blocky.Entities.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Blocky.Entities
{
    public class Player : GameComponent, IEntity
    {
        private readonly BaseCamera camera;
        private const float RotationSpeed = 0.005f;
        private const float MovementSpeed = 0.5f;

        public Vector3 Position { get; set; }

        private float leftRightRotation;

        private Model model;

        public Player(Game game, Vector3 initialPosition, BaseCamera camera) : base(game)
        {
            this.camera = camera;
            Position = initialPosition;
        }

        public override void Initialize()
        {
            model = Game.Content.Load<Model>("box");

            base.Initialize();
        }

        void IEntity.Initialize()
        {
            Initialize();
        }

        public void LoadContent() {}

        public void Update(GameTime gameTime, UpdateChanges changes)
        {
            leftRightRotation += changes.GetLeftRightRotation * RotationSpeed;

            var rotationMatrix = Matrix.CreateRotationY(leftRightRotation);

            Position += Vector3.Transform(changes.GetChangeVector(), rotationMatrix) * MovementSpeed;
        }

        public void Draw(GameTime gameTime)
        {
            if (model == null)
            {
                return;
            }

            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (var mesh in model.Meshes)
            {
                foreach (var effectObject in mesh.Effects)
                {
                    var basicEffect = (BasicEffect)effectObject;

                    basicEffect.EnableDefaultLighting();
                    basicEffect.PreferPerPixelLighting = true;

                    basicEffect.World = Matrix.Identity *
                                        Matrix.CreateRotationY(leftRightRotation) * Matrix.CreateTranslation(Position);
                    basicEffect.View = camera.ViewSettings.ViewMatrix;
                    basicEffect.Projection = camera.ProjectionMatrix;
                }

                mesh.Draw();
            }
        }
    }
}

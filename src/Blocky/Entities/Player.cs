namespace Blocky.Entities
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Player : GameComponent
    {
        private const float RotationSpeed = 0.005f;
        private const float MovementSpeed = 0.5f;

        public Vector3 Position { get; set; }

        private float leftRightRotation;

        private Model model;

        public Player(Game game, Vector3 initialPosition) : base(game)
        {
            Position = initialPosition;
        }

        public override void Initialize()
        {
            model = Game.Content.Load<Model>("box");
           
            base.Initialize();
        }

        public void Draw(BaseCamera camera)
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
                    var basicEffect = (BasicEffect) effectObject;

                    basicEffect.EnableDefaultLighting();
                    basicEffect.PreferPerPixelLighting = true;

                    basicEffect.World = Matrix.Identity*
                                        Matrix.CreateRotationY(leftRightRotation) * Matrix.CreateTranslation(Position);
                    basicEffect.View = camera.ViewSettings.ViewMatrix;
                    basicEffect.Projection = camera.ProjectionMatrix;
                }

                mesh.Draw();
            }
        }

        internal void Update(GameTime gameTime, Vector3 changeVector, float rotation)
        {
            leftRightRotation += rotation*RotationSpeed;

            var rotationMatrix = Matrix.CreateRotationY(leftRightRotation);

            Position += Vector3.Transform(changeVector, rotationMatrix) * MovementSpeed;
        }
    }
}

using Blocky.Entities.Camera;

namespace Blocky.Entities
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class Robot
    {
        private Model model;

        // new code:
        private float angle;

        public void Initialize(ContentManager contentManager)
        {
            model = contentManager.Load<Model>("box");
        }

        public void Update(GameTime gameTime)
        {
            // TotalSeconds is a double so we need to cast to float
            angle += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(BaseCamera camera)
        {
            foreach (var mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    effect.World = Matrix.CreateWorld(new Vector3(0, 1, 5), Vector3.Forward, Vector3.Up);
                    effect.View = camera.ViewSettings.ViewMatrix;
                    effect.Projection = camera.ProjectionMatrix;
                }

                mesh.Draw();
            }
        }

        private Matrix GetWorldMatrix()
        {
            const float circleRadius = 8;
            const float heightOffGround = 3;

            // this matrix moves the model "out" from the origin
            Matrix translationMatrix = Matrix.CreateTranslation(
                circleRadius, heightOffGround, 0);

            // this matrix rotates everything around the origin
            Matrix rotationMatrix = Matrix.CreateRotationY(angle);

            // We combine the two to have the model move in a circle:
            Matrix combined = translationMatrix * rotationMatrix;

            return combined;
        }

    }
}

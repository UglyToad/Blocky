using Blocky.Entities.Camera;
using Microsoft.Xna.Framework.Graphics;

namespace Blocky.Entities.Helpers
{
    public static class EffectHelpers
    {
        public static void InitializeDrawEffect(this BasicEffect effect, BaseCamera camera)
        {
            effect.Projection = camera.ProjectionMatrix;
            effect.View = camera.ViewSettings.ViewMatrix;
            effect.VertexColorEnabled = true;
            effect.EnableDefaultLighting();
        }
    }
}

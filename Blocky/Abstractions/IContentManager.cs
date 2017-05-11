namespace Blocky.Abstractions
{
    public interface IContentManager
    {
        T Load<T>(string name);
    }

    public class ContentManager : IContentManager
    {
        private readonly Microsoft.Xna.Framework.Content.ContentManager contentManager;

        public ContentManager(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }

        public T Load<T>(string name)
        {
            return contentManager.Load<T>(name);
        }
    }
}

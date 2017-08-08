namespace Blocky.SandBox.Wpf
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Windows.Media.Imaging;
    using Infrastructure;
    using Microsoft.Xna.Framework;
    using Color = System.Drawing.Color;
    using DrawingContext = Tests.Unit.Logic.DrawingContext;

    public class MainWindowViewModel : ViewModelBase
    {
        private BitmapImage image;
        private string x;
        private string y;
        private string dx;
        private string dy;

        public BitmapImage Image
        {
            get { return image; }
            set { SetProperty(ref image, value); }
        }

        public string X
        {
            get { return x; }
            set { SetProperty(ref x, value); }
        }

        public string Y
        {
            get { return y; }
            set { SetProperty(ref y, value); }
        }

        public string Dx
        {
            get { return dx; }
            set { SetProperty(ref dx, value); }
        }

        public string Dy
        {
            get { return dy; }
            set { SetProperty(ref dy, value); }
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

        public void CalculateImage()
        {
            const int height = 500;
            const int width = 500;

            const int unit = 50;

            const int offset = 10;

            var point = new { x = float.Parse(X), y = float.Parse(Y) };
            var vector = new Vector2(float.Parse(Dx), float.Parse(Dy));

            var m = vector.Y / vector.X;

            var c = point.y - m * point.x;

            var ypoints = new List<Vector2>();
            for (int i = 0; i < height - offset / unit; i++)
            {
                var xintercept = (i - c) / m;

                ypoints.Add(new Vector2(xintercept, i));
            }

            var xpoints = new List<Vector2>();
            for (int i = 0; i < width - offset / unit; i++)
            {
                var yintercept = m * i + c;

                xpoints.Add(new Vector2(i, yintercept));
            }

            using (var context = new DrawingContext())
            {
                context.DrawPoint(Color.Red, point.x, point.y);

                context.DrawArrow(Color.DeepSkyBlue, new Vector2(point.x, point.y),
                    new Vector2(point.x + vector.X, point.y + vector.Y));

                foreach (var ypoint in ypoints)
                {
                    context.DrawPoint(Color.Coral, ypoint.X, ypoint.Y);
                }

                foreach (var xpoint in xpoints)
                {
                    context.DrawPoint(Color.Magenta, xpoint.X, xpoint.Y);
                }

                Image = BitmapToImageSource(context.Bitmap);
            }
        }
    }
}

namespace Blocky.Tests.Unit.Logic
{
    using System;
    using System.Drawing;
    using Microsoft.Xna.Framework;
    using Color = System.Drawing.Color;
    using Point = System.Drawing.Point;

    public class DrawingContext : IDisposable
    {
        private const int Height = 500;
        private const int Width = 500;
        private const int Unit = 50;
        private const int Offset = 10;

        private readonly Bitmap bitmap;
        private readonly Graphics graphics;

        public Bitmap Bitmap => bitmap;

        public DrawingContext()
        {
            bitmap = new Bitmap(Width, Height);
            graphics = Graphics.FromImage(bitmap);

            DrawGrid();
        }
        
        public void DrawPoint(Color color, float x, float y)
        {
            var drawX = x * Unit + Offset;
            var drawY = (int)(y * Unit) + Offset;

            graphics.DrawEllipse(new Pen(color), drawX - 3, LogicalYToGraphics(drawY) - 3, 6, 6);
        }

        public void DrawArrow(Color color, Vector2 start, Vector2 end)
        {
            var x = start.X * Unit + Offset;
            var y = (int)(start.Y * Unit) + Offset;
            
            var vectorStart = new Point((int)x, LogicalYToGraphics(y));

            var vectorEndX = end.X*Unit + Offset;
            var vectorEndY = end.Y*Unit + Offset;

            var vectorEnd = new Point((int)vectorEndX, LogicalYToGraphics((int)vectorEndY));

            var pen = new Pen(color);

            graphics.DrawLine(pen, vectorStart, vectorEnd);
            graphics.DrawLine(pen, vectorEnd, new Point(vectorEnd.X - 5, vectorEnd.Y));
            graphics.DrawLine(pen, vectorEnd, new Point(vectorEnd.X, vectorEnd.Y + 5));
        }

        public void Save(string filename)
        {
            bitmap.Save(filename);
        }

        private void DrawGrid()
        {
            graphics.Clear(Color.White);

            // x-axis
            graphics.DrawLine(Pens.Black, new Point(Offset, LogicalYToGraphics(10)),
                new Point(Width, LogicalYToGraphics(Offset)));
            // y-axis
            graphics.DrawLine(Pens.Black, new Point(Offset, LogicalYToGraphics(10)),
                new Point(Offset, LogicalYToGraphics(Height)));

            bool draw = false;
            for (var i = Offset; i < Math.Min(Width, Height); i += Unit)
            {
                // x-ticks
                graphics.DrawLine(Pens.Black, new Point(i, LogicalYToGraphics(Offset)),
                    new Point(i, LogicalYToGraphics(Offset - 5)));
                // y-ticks
                graphics.DrawLine(Pens.Black, new Point(Offset, LogicalYToGraphics(i)),
                    new Point(Offset - 5, LogicalYToGraphics(i)));

                if (draw)
                {
                    graphics.DrawLine(Pens.CornflowerBlue, new Point(i, LogicalYToGraphics(10)),
                        new Point(i, LogicalYToGraphics(Height)));
                    graphics.DrawLine(Pens.CornflowerBlue, new Point(10, LogicalYToGraphics(i)),
                        new Point(Width, LogicalYToGraphics(i)));
                }

                draw = true;
            }
        }

        private static int LogicalYToGraphics(int y)
        {
            return Height - y;
        }

        public void Dispose()
        {
            graphics.Dispose();
            bitmap.Dispose();
        }
    }
}
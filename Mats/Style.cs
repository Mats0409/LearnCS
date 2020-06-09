using SkiaSharp;

namespace LearnCSharp.Mats
{
    public class Style
    {
        public SKColor BackgroundColor = SKColors.SkyBlue;

        public SKPaint BallPaint;
        public SKPaint BallHitPaint;
        public SKPaint TextPaint;

        public Style()
        {

            BallPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Yellow,
                IsAntialias = true
            };

            BallHitPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Red,
                IsAntialias = true
            };

            TextPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.White,
                IsAntialias = true,
                TextSize = 20
            };

            
        }
    }
}
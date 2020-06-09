using SkiaSharp;

namespace LearnCSharp.Mats
{
    public class Style
    {
        public readonly SKColor BackgroundColor;
        public readonly SKPaint BallPaint;
        public readonly SKPaint BallHitPaint;
        public readonly SKPaint TextPaint;

        public Style(SKColor backgroundColor, SKColor ballColor, SKColor hitColor, SKColor textColor)
        {
            BackgroundColor = backgroundColor;

            BallPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = ballColor,
                IsAntialias = true
            };

            BallHitPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = hitColor,
                IsAntialias = true
            };

            TextPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = textColor,
                IsAntialias = true,
                TextSize = 20
            };

            
        }
    }
}
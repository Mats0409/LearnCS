using SkiaSharp;

namespace LearnCSharp.Mats
{
    public class Style
    {
        public readonly SKColor BackgroundColor;
        public readonly SKPaint PlayerColor;
        public readonly SKPaint TextPaint;

        public Style(SKColor backgroundColor, SKColor playerColor, SKColor textColor)
        {
            BackgroundColor = backgroundColor;

            PlayerColor = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = playerColor,
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
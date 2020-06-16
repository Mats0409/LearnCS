using System;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using LearnCSharp.libGLFW;
using SkiaSharp;

namespace LearnCSharp.Mats
{
    public class EnergyBar : IDrawable, IUpdatable
    {
        float width = 10;
        float height = 2;
        public Vector2 Position;

        public Player? player;

        SKPaint backgroundPaint = new SKPaint
        {
            Color = SKColors.White,
            IsAntialias = true
        };
        SKPaint fillPaint = new SKPaint
        {
            Color = SKColors.Yellow,
            IsAntialias = true
        };

        public void Update(float deltaTimeInSeconds, InputState input)
        {

        }

        public void Draw(SKCanvas canvas)
        {

            canvas.DrawRect(Position.X, Position.Y, width, height, backgroundPaint);

            if (player != null) 
            {
                canvas.DrawRect(Position.X, Position.Y, width * player.energy / Player.MaxEnergy, height, fillPaint);
            }
        }
    }
}
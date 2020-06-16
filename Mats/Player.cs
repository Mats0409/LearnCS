using System;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using LearnCSharp.libGLFW;
using SkiaSharp;

namespace LearnCSharp.Mats
{
    public class Player : IDrawable, IUpdatable
    {
        public const int MaxEnergy = 100;

        public Vector2 Position;
        public Vector2 Velocity;
        float size = 1;
        float speed = 25;
        public float energy = MaxEnergy;

        SKPaint paint = new SKPaint
        {
            Color = SKColors.Green,
            IsAntialias = true
        };

      
        public void Update(float deltaTimeInSeconds, InputState input)
        {
            Velocity = Vector2.Zero;

            if (input.IsKeyDown(Key.W))
            {
                Velocity += new Vector2(0, -speed);
            }

            if (input.IsKeyDown(Key.A))
            {
                Velocity += new Vector2(-speed, 0);
            }

            if (input.IsKeyDown(Key.S))
            {
                Velocity += new Vector2(0, speed);
            }

            if (input.IsKeyDown(Key.D))
            {
                Velocity += new Vector2(speed, 0);
            }
            
            energy -= deltaTimeInSeconds * 10;

            Position += Velocity * deltaTimeInSeconds;

        }

        public void Draw(SKCanvas canvas)
        {
            canvas.DrawRect(Position.X, Position.Y, size, size, paint);
        }

    }

}
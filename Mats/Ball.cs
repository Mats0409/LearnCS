using System;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using LearnCSharp.libGLFW;
using SkiaSharp;

namespace LearnCSharp.Mats
{
    public class Ball
    {
        public Vector2 Velocity;
        public Vector2 Position;

        public bool IsHit;

        public static float Radius = 200f / MathF.Sqrt(Scene.BallCount);

        public void Update(float deltaTimeInSeconds, InputState input)
        {

            Vector2 p = Position;
            Vector2 v = Velocity;

            p = (p + deltaTimeInSeconds * v);

            if (p.X < 0 + Radius && v.X < 0)
            {
                v.X = -v.X;
            }
            if (p.X > Scene.ViewWidth - Radius && v.X > 0)
            {
                v.X = -v.X;
            }

            if (p.Y < 0 + Radius && v.Y < 0)
            {
                v.Y = -v.Y;
            }
            if (p.Y > Scene.ViewHeight - Radius && v.Y > 0)
            {
                v.Y = -v.Y;
            }


            Position = p;
            Velocity = v;
        }

        public void Draw(Style style, SKCanvas canvas)
        {
            var paint = IsHit ? style.BallHitPaint : style.BallPaint;

            canvas.DrawCircle(Position.X, Position.Y, Radius, paint);
        }
    }
}
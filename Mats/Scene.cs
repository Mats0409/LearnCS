using System;
using LearnCSharp.libGLFW;
using SkiaSharp;

namespace LearnCSharp.Mats
{
    public class Scene
    {
        public const int ViewWidth = 1280;
        public const int ViewHeight = 720;

        public const int BallCount = 10;

        public SKColor BackgroundColor = SKColors.SkyBlue;

        public SKPaint BallPaint;
        public SKPaint TextPaint;

        public float[] BallPositionsX = new float[BallCount];
        public float[] BallPositionsY = new float[BallCount];

        public static float BallRadius = 100f / MathF.Sqrt(BallCount);

        // Velocity, in pixels per second.
        public static float[] BallVelocitiesX = new float[BallCount];
        public static float[] BallVelocitiesY = new float[BallCount];

        Random rnd = new Random();

        bool PreviousMouseDown;
        bool CurrentMouseDown;

        bool Click;

        public Scene()
        {


            for (int i = 0; i < BallCount; i++)
            {
                Randomize(i);
            }

            BallPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Yellow,
                IsAntialias = true
            };

            TextPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.White,
                IsAntialias = true,
                TextSize = 32
            };

        }


        // Assigns random position and velocity to ball[i] 
        private void Randomize(int i)
        {
            var angle = rnd.Next(0, 360) * MathF.PI / 180;
            var speed = 5000;

            BallPositionsY[i] = rnd.Next(0, ViewHeight);
            BallPositionsX[i] = rnd.Next(0, ViewWidth);

            BallVelocitiesX[i] = MathF.Cos(angle) * speed;
            BallVelocitiesY[i] = MathF.Sin(angle) * speed;
        }

        public void Update(float deltaTimeInSeconds, InputState input)
        {
            for (int i = 0; i < BallCount; i++)
            {
                float px = BallPositionsX[i];
                float py = BallPositionsY[i];
                float vx = BallVelocitiesX[i];
                float vy = BallVelocitiesY[i];

                px = (px + deltaTimeInSeconds * vx);
                py = (py + deltaTimeInSeconds * vy);

                if (px < 0 + BallRadius && vx < 0)
                {
                    vx = -vx;
                }
                if (px > ViewWidth - BallRadius && vx > 0)
                {
                    vx = -vx;
                }

                if (py < 0 + BallRadius && vy < 0)
                {
                    vy = -vy;
                }
                if (py > ViewHeight - BallRadius && vy > 0)
                {
                    vy = -vy;
                }




                if (input.IsMouseClicked(MouseButton.Left))
                {
                    Randomize(i);
                }
                else
                {
                    BallPositionsX[i] = px;
                    BallPositionsY[i] = py;
                    BallVelocitiesX[i] = vx;
                    BallVelocitiesY[i] = vy;
                }
            }

            PreviousMouseDown = CurrentMouseDown;

            CurrentMouseDown = input.IsMouseDown(MouseButton.Left);

            // if (!PreviousMouseDown && CurrentMouseDown)
        }

        public void Draw(SKCanvas canvas)
        {
            // canvas.DrawText("Click to set ball. Press ALT+ENTER to toggle fullscreen.", TextPaint.TextSize, TextPaint.TextSize, TextPaint);

            for (int i = 0; i < BallCount; i++)
            {
                canvas.DrawCircle(BallPositionsX[i], BallPositionsY[i], BallRadius, BallPaint);
            }
        }
    }
}
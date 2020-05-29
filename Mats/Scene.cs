using System;
using LearnCSharp.libGLFW;
using SkiaSharp;

namespace LearnCSharp.Mats
{
    public class Scene
    {
        public const int ViewWidth = 1280;
        public const int ViewHeight = 720;

        public const int BallCount = 100;

        public SKColor BackgroundColor = SKColors.SkyBlue;

        public SKPaint BallPaint;
        public SKPaint TextPaint;

        public float[] BallPositionsX = new float[BallCount];
        public float[] BallPositionsY = new float[BallCount];

        public static float BallRadius = 20f;

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
                BallPositionsY[i] = rnd.Next(0, ViewHeight);
                BallPositionsX[i] = rnd.Next(0, ViewWidth);

                BallVelocitiesX[i] = rnd.Next(-100, 100);
                BallVelocitiesY[i] = rnd.Next(-100, 100);
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

        public void Update(float deltaTimeInSeconds, InputState input)
        {
            for (int i = 0; i < BallCount; i++)
            {
                BallPositionsX[i] = (BallPositionsX[i] + deltaTimeInSeconds * BallVelocitiesX[i]) % ViewWidth;
                BallPositionsY[i] = (BallPositionsY[i] + deltaTimeInSeconds * BallVelocitiesY[i]) % ViewHeight;

                if (input.IsMouseClicked(MouseButton.Left))
                {
                    BallPositionsX[i] = rnd.Next(0, ViewWidth);
                    BallPositionsY[i] = rnd.Next(0, ViewHeight);

                    BallVelocitiesX[i] = rnd.Next(-100, 100);
                    BallVelocitiesY[i] = rnd.Next(-100, 100);
                }
            }

            PreviousMouseDown = CurrentMouseDown;

            CurrentMouseDown = input.IsMouseDown(MouseButton.Left);

            // if (!PreviousMouseDown && CurrentMouseDown)
        }

        public void Draw(SKCanvas canvas)
        {
            canvas.DrawText("Click to set ball. Press ALT+ENTER to toggle fullscreen.", TextPaint.TextSize, TextPaint.TextSize, TextPaint);

            for (int i = 0; i < BallCount; i++)
            {
                canvas.DrawCircle(BallPositionsX[i], BallPositionsY[i], BallRadius, BallPaint);
            }
        }
    }
}
﻿using System;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using LearnCSharp.libGLFW;
using SkiaSharp;

namespace LearnCSharp.Mats
{
    public class Scene
    {
        public const int ViewWidth = 1280;
        public const int ViewHeight = 720;

        public const int BallCount = 50;

        public SKColor BackgroundColor = SKColors.SkyBlue;

        public SKPaint BallPaint;
        public SKPaint HitPaint;
        public SKPaint TextPaint;

        public Vector2[] BallPositions = new Vector2[BallCount];

        public static float BallRadius = 200f / MathF.Sqrt(BallCount);

        // Velocity, in pixels per second.
        public Vector2[] BallVelocities = new Vector2[BallCount];

        Random rnd = new Random();

        bool PreviousMouseDown;
        bool CurrentMouseDown;

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

            HitPaint = new SKPaint
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


        // Assigns random position and velocity to ball[i] 
        private void Randomize(int i)
        {
            var angle = rnd.Next(0, 360) * MathF.PI / 180;
            var speed = 100;

            BallPositions[i] = new Vector2(rnd.Next(0, ViewWidth), rnd.Next(0, ViewHeight));

            BallVelocities[i] = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * speed;
        }

        public void Update(float deltaTimeInSeconds, InputState input)
        {
            for (int i = 0; i < BallCount; i++)
            {
                Vector2 p = BallPositions[i];
                Vector2 v = BallVelocities[i];

                p = (p + deltaTimeInSeconds * v);

                if (p.X < 0 + BallRadius && v.X < 0)
                {
                    v.X = -v.X;
                }
                if (p.X > ViewWidth - BallRadius && v.X > 0)
                {
                    v.X = -v.X;
                }

                if (p.Y < 0 + BallRadius && v.Y < 0)
                {
                    v.Y = -v.Y;
                }
                if (p.Y > ViewHeight - BallRadius && v.Y > 0)
                {
                    v.Y = -v.Y;
                }




                if (input.IsMouseClicked(MouseButton.Left))
                {
                    Randomize(i);
                }
                else
                {
                    BallPositions[i] = p;
                    BallVelocities[i] = v;
                }
            }

            PreviousMouseDown = CurrentMouseDown;

            CurrentMouseDown = input.IsMouseDown(MouseButton.Left);

            // if (!PreviousMouseDown && CurrentMouseDown)
        }

        public void Draw(SKCanvas canvas)
        {
            // Compute total kinetic energy. This should stay the same.
            // Formula for kinetic energy = 1/2 * ||velocity||^2 * mass
            var energy = 0.0;
            for (int i = 0; i < BallCount; i++)
            {
                const float mass = 1;
                var v = BallVelocities[i];
                energy += Vector2.Dot(v, v) * mass * 0.5f;
            }

            TextPaint.TextAlign = SKTextAlign.Right;
            canvas.DrawText($"Energy: {energy:0.0} Joule", ViewWidth, TextPaint.TextSize, TextPaint);

            for (int i = 0; i < BallCount; i++)
            {
                canvas.DrawCircle(BallPositions[i].X, BallPositions[i].Y, BallRadius, BallPaint);
            }

            for (int i1 = 0; i1 < BallCount - 1; i1++)
            {
                var p1 = BallPositions[i1];
                var v1 = BallVelocities[i1];


                for (int i2 = i1 + 1; i2 < BallCount; i2++)
                {
                    var p2 = BallPositions[i2];
                    var v2 = BallVelocities[i2];

                    var dp = p1 - p2;

                    var d = dp.Length();

                    if (d < BallRadius * 2)
                    {
                        canvas.DrawCircle(BallPositions[i1].X, BallPositions[i1].Y, BallRadius, HitPaint);
                        var dn = Vector2.Normalize(dp);

                        float d1 = Vector2.Dot(dn, v1);
                        float d2 = Vector2.Dot(dn, v2);

                        if (d1 < 0 || d2 > 0)
                        {
                            var a1 = d1 * dn;
                            var a2 = d2 * dn;

                            var b1 = v1 - a1;
                            var b2 = v2 - a2;

                            BallVelocities[i1] = b1 + a2;
                            BallVelocities[i2] = b2 + a1;
                            break;

                        }
                    }
                }


            }
        }
    }
}
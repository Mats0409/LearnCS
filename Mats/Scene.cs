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

        public Style Style;

        public Ball[] Balls;

        Random rnd = new Random();

        bool PreviousMouseDown;
        bool CurrentMouseDown;

        public Scene()
        {
            Style = new Style();
            Balls = new Ball[BallCount];


            for (int i = 0; i < BallCount; i++)
            {
                Balls[i] = new Ball();
                Randomize(i);
            }

        }


        // Assigns random position and velocity to ball[i] 
        private void Randomize(int i)
        {
            var angle = rnd.Next(0, 360) * MathF.PI / 180;
            var speed = 100;

            var margin = (int)Math.Ceiling(Ball.Radius);

            Balls[i].Position = new Vector2(rnd.Next(margin, ViewWidth - margin), rnd.Next(margin, ViewHeight - margin));

            Balls[i].Velocity = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * speed;
        }

        public void Update(float deltaTimeInSeconds, InputState input)
        {
            for (int i = 0; i < Balls.Length; i++)
            {
                Balls[i].Update(deltaTimeInSeconds, input);

                Balls[i].IsHit = false;


                if (input.IsMouseClicked(MouseButton.Left))
                {
                    Randomize(i);
                }

            }


            PreviousMouseDown = CurrentMouseDown;

            CurrentMouseDown = input.IsMouseDown(MouseButton.Left);

            // Physics
            for (int i1 = 0; i1 < BallCount - 1; i1++)
            {
                var b1 = Balls[i1];
                var p1 = b1.Position;
                var v1 = b1.Velocity;


                for (int i2 = i1 + 1; i2 < BallCount; i2++)
                {
                    var b2 = Balls[i2];
                    var p2 = b2.Position;
                    var v2 = b2.Velocity;

                    var dp = p1 - p2;

                    var d = dp.Length();

                    if (d < Ball.Radius * 2)
                    {
                        // ??? canvas.DrawCircle(BallPositions[i1].X, BallPositions[i1].Y, BallRadius, HitPaint);

                        var dn = Vector2.Normalize(dp);

                        float d1 = Vector2.Dot(dn, v1);
                        float d2 = Vector2.Dot(dn, v2);

                        if (d1 < 0 || d2 > 0)
                        {
                            var t1 = d1 * dn;
                            var t2 = d2 * dn;

                            var n1 = v1 - t1;
                            var n2 = v2 - t2;

                            b1.Velocity = n1 + t2;
                            b2.Velocity = n2 + t1;

                            b1.IsHit = true;
                            b2.IsHit = true;
                            break;

                        }
                    }
                }

            }
        }

        public void Draw(SKCanvas canvas)
        {

            for (int i = 0; i < BallCount; i++)
            {
                Balls[i].Draw(Style, canvas);

            }
        }
    }
}
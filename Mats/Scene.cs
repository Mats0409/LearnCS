using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using LearnCSharp.libGLFW;
using SkiaSharp;

namespace LearnCSharp.Mats
{
    public class Scene : IEnumerable
    {
        private List<IDrawable> drawables = new List<IDrawable>();
        private List<IUpdatable> updatables = new List<IUpdatable>();

        public const int GridWidth = 80;
        public const int GridHeight = 45;

        bool PreviousMouseDown;
        bool CurrentMouseDown;

        SKPaint linePaint = new SKPaint
        {
            Color = SKColors.Gray,
            IsAntialias = true,
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 0.05f
        };



        public Scene()
        {

        }

        public void Add(object obj)
        {
            if (obj is IDrawable drawable)
            {
                drawables.Add(drawable);
            }

            if (obj is IUpdatable updatable)
            {
                updatables.Add(updatable);
            }
        }

        public void Remove(object obj)
        {
            if (obj is IDrawable drawable)
            {
                drawables.Add(drawable);
            }

            if (obj is IUpdatable updatable)
            {
                updatables.Add(updatable);
            }
        }

        public void Update(float deltaTimeInSeconds, InputState input)
        {

            if (input.IsMouseClicked(MouseButton.Right))
            {

            }


            PreviousMouseDown = CurrentMouseDown;

            CurrentMouseDown = input.IsMouseDown(MouseButton.Left);

            // Physics

            foreach (var item in updatables)
            {
                item.Update(deltaTimeInSeconds, input);
            }

        }

        public void Draw(SKCanvas canvas)
        {
            for (float x = -GridWidth/2f; x <= GridWidth/2f; ++x)
            {

                canvas.DrawLine(x, -GridHeight/2f, x, GridHeight/2f, linePaint);
            }

            for (float y = -GridHeight/2f; y <= GridHeight/2f; ++y)
            {

                canvas.DrawLine(-GridWidth/2f, y, GridWidth/2f, y, linePaint);
            }

            foreach (var item in drawables)
            {
                item.Draw(canvas);
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return updatables.Concat(updatables).Distinct().GetEnumerator();
        }
    }
}
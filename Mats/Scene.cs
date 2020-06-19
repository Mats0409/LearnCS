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

            // Physics

            foreach (var item in updatables)
            {
                item.Update(deltaTimeInSeconds, input);
            }

        }

        public void Draw(SKCanvas canvas)
        {
            for (float x = Grid.Left; x <= Grid.Right; ++x)
            {

                canvas.DrawLine(x, Grid.Top, x, Grid.Bottom, linePaint);
            }

            for (float y = Grid.Top; y <= Grid.Bottom; ++y)
            {

                canvas.DrawLine(Grid.Left, y, Grid.Right, y, linePaint);
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
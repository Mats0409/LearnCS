using SkiaSharp;

namespace LearnCSharp.Mats
{
    public interface IDrawable
    {
        public int Layer => 0;
        void Draw(SKCanvas canvas);
    }

    public interface IUpdatable
    {
        void Update(float deltaTimeInSeconds, InputState input);
    }
}
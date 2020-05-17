using System;
using LearnCSharp.libGLFW;
using SkiaSharp;

namespace LearnCSharp.Mats
{
	public class Scene
	{
		public const int ViewWidth = 1024;
		public const int ViewHeight = 768;

		public const int TextSize = 32;

		public SKColor BackgroundColor = SKColors.SkyBlue;

		public SKPaint BallPaint;
		public SKPaint TextPaint;

		public float BallPositionX;
		public float BallPositionY;

		public static float BallRadius = 20f;

		// Velocity, in pixels per second.
		public static float BallVelocityX = 100;
		public static float BallVelocityY = 70;

		public Scene()
		{
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
			BallPositionX = (BallPositionX + deltaTimeInSeconds * BallVelocityX) % ViewWidth;
			BallPositionY = (BallPositionY + deltaTimeInSeconds * BallVelocityY) % ViewHeight;

			if (input.IsKeyDown(Key.Space) || input.IsMouseDown(MouseButton.ButtonLeft))
			{
				BallPositionX = input.MousePositionX;
				BallPositionY = input.MousePositionY;
			}
		}

		public void Draw(SKCanvas canvas)
		{
			canvas.DrawText("Click to set ball", TextPaint.TextSize, TextPaint.TextSize, TextPaint);
			canvas.DrawCircle(BallPositionX, BallPositionY, BallRadius, BallPaint);
		}
	}
}
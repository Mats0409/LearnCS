using System;
using System.Diagnostics;
using LearnCSharp.libGLFW;
using LearnCSharp.Mats;
using SkiaSharp;

namespace LearnCSharp
{
	class Program
	{
		public static void Main(string[] args)
		{
			Glfw.SetErrorCallback((error, description) => { Console.WriteLine($"GLFW Error {error}: {description}"); });
			Glfw.Init();

			IntPtr primaryMonitorPtr = Glfw.GetPrimaryMonitor();
			Glfw.VidMode videoMode = Glfw.GetVideoMode(primaryMonitorPtr);
			Glfw.GetMonitorWorkarea(primaryMonitorPtr, out var workAreaX, out var workAreaY, out var workAreaWidth, out var workAreaHeight);

			// For a full-screen game, replace IntPtr.Zero with primaryMonitorPtr
			IntPtr monitorPtr = IntPtr.Zero;
            
			// Create a GLFW window
			var windowPtr = Glfw.CreateWindow(Scene.ViewWidth, Scene.ViewHeight, "C# tutorial for Mats", monitorPtr, IntPtr.Zero);

			// Center the window on the primary monitor
			Glfw.SetWindowPos(windowPtr,
				workAreaX + (workAreaWidth - Scene.ViewWidth) / 2,
				workAreaY + (workAreaHeight - Scene.ViewHeight) / 2);

			// Make sure the OpenGL rendering context is set on the current thread,
			// otherwise Skia's GRContext.Create(GRBackend.OpenGL) will return null
			Glfw.MakeContextCurrent(windowPtr);

			var frameBufferInfo = new GRGlFramebufferInfo((uint)new UIntPtr(0), GRPixelConfig.Rgba8888.ToGlSizedFormat());
			using var backendRenderTarget = new GRBackendRenderTarget(Scene.ViewWidth, Scene.ViewHeight, 0, 8, frameBufferInfo);

			using var grContext = GRContext.Create(GRBackend.OpenGL);

			using var skSurface = SKSurface.Create(grContext, backendRenderTarget, GRSurfaceOrigin.BottomLeft, SKImageInfo.PlatformColorType);

			// get the canvas from the surface
			using var skCanvas = skSurface.Canvas;

			var style = new Style(SKColors.SkyBlue, SKColors.Yellow, SKColors.Red, SKColors.White);
			var scene = new Scene(style);

			var frameDuration = TimeSpan.FromSeconds(1.0 / videoMode.RefreshRate);

			var inputState = new InputState(windowPtr);

			var stopwatch = new Stopwatch();

			using var fpsTextPaint = new SKPaint
			{
				Style = SKPaintStyle.Fill,                
				Color = SKColors.White,
				IsAntialias = true,
				TextSize = 20,
			};

			TimeSpan renderDuration = default;
			TimeSpan updateDuration = default;

			while (Glfw.WindowShouldClose(windowPtr) == Glfw.False)
			{
				stopwatch.Restart();

				// Let GLFW process any queued input events, like keyboard, mouse, ...
				Glfw.PollEvents();

				// Clear the drawing canvas
				skCanvas.Clear(style.BackgroundColor);

				// Draw to scene to the canvas
				scene.Draw(skCanvas);

				skCanvas.DrawText($"Render: {renderDuration.TotalMilliseconds:0.0}ms  Update: {updateDuration.TotalMilliseconds:0.0}ms", fpsTextPaint.TextSize, fpsTextPaint.TextSize, fpsTextPaint);

				// Flush all pending Skia drawing commands
				grContext.Flush();

				// Present the canvas on the display
				Glfw.SwapBuffers(windowPtr);

				renderDuration = stopwatch.Elapsed;

				stopwatch.Restart();

				// Update the scene, moving it forward in time.
				scene.Update((float)frameDuration.TotalSeconds, inputState);

				inputState.Update();

				updateDuration = stopwatch.Elapsed;

				//Glfw.SetWindowTitle(windowPtr, );

				if (inputState.IsKeyDown(Key.Enter) && inputState.IsKeyDown(Key.LeftAlt))
				{
					if (Glfw.GetWindowMonitor(windowPtr) == IntPtr.Zero)
					{
						// Switch to full screen.
						Glfw.SetWindowMonitor(windowPtr, primaryMonitorPtr, 0, 0, Scene.ViewWidth, Scene.ViewHeight, 60);
					}
					else
					{
						// Switch to windowed mode
						Glfw.SetWindowMonitor(windowPtr, IntPtr.Zero,
							workAreaX + (workAreaWidth - Scene.ViewWidth) / 2,
							workAreaY + (workAreaHeight - Scene.ViewHeight) / 2,
							Scene.ViewWidth,
							Scene.ViewHeight, 0);
					}
				}

				// Glfw.SetWindowTitle(windowPtr, $"FPS = {1 / frameDuration.TotalSeconds:000.0}");
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using LearnCSharp.libGLFW;

namespace LearnCSharp.Mats
{
	public class InputState 
	{
		private readonly Dictionary<MouseButton, MouseButtonState> _mouseStates = new Dictionary<MouseButton, MouseButtonState>();
		private readonly Dictionary<Key, KeyState> _keyStates = new Dictionary<Key, KeyState>();

		public InputState(IntPtr windowPtr)
		{
			Glfw.SetCursorPosCallback(windowPtr, OnCursorPosChanged);
			Glfw.SetMouseButtonCallback(windowPtr, OnMouseButtonChanged);
			Glfw.SetKeyCallback(windowPtr, OnKeyChanged);
		}

		public float MousePositionX;

		public float MousePositionY;

		public KeyModifier KeyModifiers;

		public bool IsMouseDown(MouseButton button) => _mouseStates.TryGetValue(button, out var state) && state == MouseButtonState.Pressed;

		public bool IsMouseUp(MouseButton button) => !_mouseStates.TryGetValue(button, out var state) || state == MouseButtonState.Released;

		/// <summary>
		/// Is a key pressed?
		/// </summary>
		public bool IsKeyDown(Key key) => _keyStates.TryGetValue(key, out var state) && state == KeyState.Pressed;

		/// <summary>
		/// Is a key released?
		/// </summary>
		public bool IsKeyUp(Key key) => !_keyStates.TryGetValue(key, out var state) || state == KeyState.Released;

		/// <summary>
		/// Are multiple keys pressed?
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool AreKeysDown(params Key[] key) => key.All(IsKeyDown);

		private void OnKeyChanged(IntPtr window, Key key, int code, KeyState state, KeyModifier mods)
		{
			_keyStates[key] = state;
			KeyModifiers = mods;
		}

		private void OnMouseButtonChanged(IntPtr window, MouseButton button, MouseButtonState state, KeyModifier mods)
		{
			_mouseStates[button] = state;
			KeyModifiers = mods;
		}

		private void OnCursorPosChanged(IntPtr window, double xPos, double yPos)
		{
			MousePositionX = (float) xPos;
			MousePositionY = (float) yPos;
		}
	}
}
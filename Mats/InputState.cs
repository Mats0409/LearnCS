using System;
using System.Collections.Generic;
using System.Linq;
using LearnCSharp.libGLFW;
using SkiaSharp;

namespace LearnCSharp.Mats
{
    public class InputState
    {
        private readonly Dictionary<MouseButton, MouseButtonState> _currentMouseStates = new Dictionary<MouseButton, MouseButtonState>();
        private readonly Dictionary<MouseButton, MouseButtonState> _previousMouseStates = new Dictionary<MouseButton, MouseButtonState>();

        private readonly Dictionary<Key, KeyState> _currentKeyStates = new Dictionary<Key, KeyState>();
        private readonly Dictionary<Key, KeyState> _previousKeyStates = new Dictionary<Key, KeyState>();

        private SKMatrix _invViewTransform;

        public InputState(IntPtr windowPtr, SKMatrix viewTransform)
        {
            _invViewTransform = viewTransform.Invert();
            Glfw.SetCursorPosCallback(windowPtr, OnCursorPosChanged);
            Glfw.SetMouseButtonCallback(windowPtr, OnMouseButtonChanged);
            Glfw.SetKeyCallback(windowPtr, OnKeyChanged);
        }

        public float MousePositionX;

        public float MousePositionY;

        public KeyModifier KeyModifiers;


        private static bool IsMouseDown(Dictionary<MouseButton, MouseButtonState> table, MouseButton button)
        {
            return table.TryGetValue(button, out var state) && state == MouseButtonState.Pressed;
        }

        private static bool IsMouseUp(Dictionary<MouseButton, MouseButtonState> table, MouseButton button)
        {
            return !table.TryGetValue(button, out var state) || state == MouseButtonState.Released;
        }


        private static bool IsKeyDown(Dictionary<Key, KeyState> table, Key button)
        {
            return table.TryGetValue(button, out var state) && state == KeyState.Pressed;
        }

        private static bool IsKeyUp(Dictionary<Key, KeyState> table, Key button)
        {
            return !table.TryGetValue(button, out var state) || state == KeyState.Released;
        }

        public bool IsMouseDown(MouseButton button) => IsMouseDown(_currentMouseStates, button);

        public bool IsMouseUp(MouseButton button) => IsMouseUp(_currentMouseStates, button);


        public bool IsMouseClicked(MouseButton button)
        {
            var p = IsMouseDown(_previousMouseStates, button);
            var c = IsMouseDown(_currentMouseStates, button);
            return !p && c;
        }


        public bool IsMouseReleased(MouseButton button)
        {
            var p = IsMouseDown(_previousMouseStates, button);
            var c = IsMouseDown(_currentMouseStates, button);
            return p && !c;
        }

        /// <summary>
        /// Is a key pressed?
        /// </summary>
        public bool IsKeyDown(Key key) => _currentKeyStates.TryGetValue(key, out var state) && state == KeyState.Pressed;

        /// <summary>
        /// Is a key released?
        /// </summary>
        public bool IsKeyUp(Key key) => !_currentKeyStates.TryGetValue(key, out var state) || state == KeyState.Released;

        public bool IsKeyTapped(Key key)
        {
            var p = IsKeyDown(_previousKeyStates, key);
            var c = IsKeyDown(_currentKeyStates, key);
            return !p && c;
        }


        public bool IsKeyReleased(Key key)
        {
            var p = IsKeyDown(_previousKeyStates, key);
            var c = IsKeyDown(_currentKeyStates, key);
            return p && !c;
        }

        /// <summary>
        /// Are multiple keys pressed?
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool AreKeysDown(params Key[] key) => key.All(IsKeyDown);

        public void Update()
        {
            foreach (var pair in _currentKeyStates)
            {
                _previousKeyStates[pair.Key] = pair.Value;
            }

            foreach (var pair in _currentMouseStates)
            {
                _previousMouseStates[pair.Key] = pair.Value;
            }
        }

        private void OnKeyChanged(IntPtr window, Key key, int code, KeyState state, KeyModifier mods)
        {
            if (state != KeyState.Repeated)
            {
                _currentKeyStates[key] = state;
            }
            KeyModifiers = mods;
        }


        private void OnMouseButtonChanged(IntPtr window, MouseButton button, MouseButtonState state, KeyModifier mods)
        {
            _currentMouseStates[button] = state;
            KeyModifiers = mods;
        }

        private void OnCursorPosChanged(IntPtr window, double xPos, double yPos)
        {
            var localPos = _invViewTransform.MapPoint((float)xPos, (float)yPos);
            MousePositionX = localPos.X;
            MousePositionY = localPos.Y;
        }
    }
}
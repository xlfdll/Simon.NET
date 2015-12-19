using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Simon.NET
{
    internal class SimonPlayer
    {
        private SimonPlayer() { }

        internal SimonPlayer(PlayerIndex playerIndex)
        {
            _simonPlayerIndex = playerIndex;

            _previousGamePadState = GamePad.GetState(playerIndex);
            _previousKeyboardState = Keyboard.GetState(playerIndex);
            _previousMouseState = Mouse.GetState();

            _score = new SimonScore();
        }

        internal SimonPlayer(PlayerIndex playerIndex, Vector2 scorePosition)
            : this(playerIndex)
        {
            _score.RenderPosition = scorePosition;
        }

        private PlayerIndex _simonPlayerIndex;
        private SimonScore _score;

        private GamePadState _previousGamePadState;
        private KeyboardState _previousKeyboardState;
        private MouseState _previousMouseState;

        private GamePadState _currentGamePadState;
        private KeyboardState _currentKeyboardState;
        private MouseState _currentMouseState;

        internal PlayerIndex SimonPlayerIndex
        {
            get { return _simonPlayerIndex; }
        }

        internal SimonScore Score
        {
            get { return _score; }
            set { _score = value; }
        }

        internal GamePadState PreviousGamePadState
        {
            get { return _previousGamePadState; }
            set { _previousGamePadState = value; }
        }
        internal KeyboardState PreviousKeyboardState
        {
            get { return _previousKeyboardState; }
            set { _previousKeyboardState = value; }
        }
        internal MouseState PreviousMouseState
        {
            get { return _previousMouseState; }
            set { _previousMouseState = value; }
        }

        internal GamePadState CurrentGamePadState
        {
            get { return _currentGamePadState; }
            set { _currentGamePadState = value; }
        }
        internal KeyboardState CurrentKeyboardState
        {
            get { return _currentKeyboardState; }
            set { _currentKeyboardState = value; }
        }
        internal MouseState CurrentMouseState
        {
            get { return _currentMouseState; }
            set { _currentMouseState = value; }
        }

        internal static Keys BaseKey
        {
            get { return Keys.Up; }
        }
    }
}
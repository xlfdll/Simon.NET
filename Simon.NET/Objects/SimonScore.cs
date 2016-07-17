using System;

using Microsoft.Xna.Framework;

namespace Simon.NET
{
    internal class SimonScore
    {
        internal SimonScore()
        {
            _value = 0;
        }

        internal SimonScore(Vector2 position)
            : this()
        {
            _position = position;
        }

        private Int32 _value;
        private Vector2 _position;

        internal Int32 Value
        {
            get { return _value; }
            set { _value = value; }
        }
        internal Vector2 RenderPosition
        {
            get { return _position; }
            set { _position = value; }
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
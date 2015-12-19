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
    internal class SimonPattern
    {
        internal SimonPattern()
        {
            _random = new Random();
            _pattern = new Int32[100];

            for (Int32 i = 0; i < _pattern.Length; i++)
            {
                _pattern[i] = _random.Next(0, 4);
            }
        }

        private Random _random;
        private Int32[] _pattern;

        internal Int32 this[Int32 index]
        {
            get { return _pattern[index]; }
            set { _pattern[index] = value; }
        }
    }
}
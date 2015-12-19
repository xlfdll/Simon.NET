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
    internal class SimonButton
    {
        internal SimonButton()
        {
            _isLight = false;
        }

        internal SimonButton(Texture2D normalButton, Texture2D glowButton)
            : this()
        {
            _normalButton = normalButton;
            _glowButton = glowButton;
        }

        internal SimonButton(Texture2D normalButton, Texture2D glowButton, Rectangle buttonRectangle)
            : this(normalButton, glowButton)
        {
            _buttonRectangle = buttonRectangle;
        }

        private Texture2D _normalButton;
        private Texture2D _glowButton;
        private Rectangle _buttonRectangle;
        private Boolean _isLight;

        internal Texture2D NormalButton
        {
            get { return _normalButton; }
            set { _normalButton = value; }
        }

        internal Texture2D GlowButton
        {
            get { return _glowButton; }
            set { _glowButton = value; }
        }

        internal Rectangle ButtonRectangle
        {
            get { return _buttonRectangle; }
            set { _buttonRectangle = value; }
        }

        internal Boolean IsLight
        {
            get { return _isLight; }
            set { _isLight = value; }
        }
    }
}
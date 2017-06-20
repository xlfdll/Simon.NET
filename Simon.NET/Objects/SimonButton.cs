using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Simon.NET
{
	internal class SimonButton
	{
		internal SimonButton()
		{
			this.IsLight = false;
		}

		internal SimonButton(Texture2D normalButton, Texture2D glowButton)
			: this()
		{
			this.NormalButton = normalButton;
			this.GlowButton = glowButton;
		}

		internal SimonButton(Texture2D normalButton, Texture2D glowButton, Rectangle buttonRectangle)
			: this(normalButton, glowButton)
		{
			this.ButtonRectangle = buttonRectangle;
		}

		internal Texture2D NormalButton { get; set; }
		internal Texture2D GlowButton { get; set; }
		internal Rectangle ButtonRectangle { get; set; }
		internal Boolean IsLight { get; set; }
	}
}
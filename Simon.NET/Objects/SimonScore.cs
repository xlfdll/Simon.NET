using System;

using Microsoft.Xna.Framework;

namespace Simon.NET
{
	internal class SimonScore
	{
		internal SimonScore()
		{
			this.Value = 0;
		}

		internal SimonScore(Vector2 renderPosition)
			: this()
		{
			this.RenderPosition = renderPosition;
		}

		internal Int32 Value { get; set; }
		internal Vector2 RenderPosition { get; set; }

		public override String ToString()
		{
			return this.Value.ToString();
		}
	}
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Simon.NET
{
	internal class SimonPlayer
	{
		internal SimonPlayer(PlayerIndex playerIndex)
		{
			this.SimonPlayerIndex = playerIndex;

			this.PreviousGamePadState = GamePad.GetState(playerIndex);
			this.PreviousKeyboardState = Keyboard.GetState();
			this.PreviousMouseState = Mouse.GetState();

			this.Score = new SimonScore();
		}

		internal SimonPlayer(PlayerIndex playerIndex, Vector2 scorePosition)
			: this(playerIndex)
		{
			this.Score.RenderPosition = scorePosition;
		}

		internal PlayerIndex SimonPlayerIndex { get; private set; }

		internal SimonScore Score { get; set; }

		internal GamePadState PreviousGamePadState { get; set; }
		internal KeyboardState PreviousKeyboardState { get; set; }
		internal MouseState PreviousMouseState { get; set; }

		internal GamePadState CurrentGamePadState { get; set; }
		internal KeyboardState CurrentKeyboardState { get; set; }
		internal MouseState CurrentMouseState { get; set; }

		internal static Keys BaseKey
		{
			get { return Keys.Up; }
		}
	}
}
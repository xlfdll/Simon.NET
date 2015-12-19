using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Simon.NET
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        #region Fields

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Color backgroundColor;

        SpriteFont font;

        SimonPlayer simonPlayer;
        SimonPattern simonPattern;
        SimonButton[] simonButtons;
        SimonAudio simonAudio;

        #endregion

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            IsMouseVisible = true;

            backgroundColor = Color.Gray;

            simonPlayer = new SimonPlayer(PlayerIndex.One, new Vector2(GraphicsDevice.Viewport.Width * 0.05f, GraphicsDevice.Viewport.Height * 0.05f));
            simonPattern = new SimonPattern();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            simonButtons = new SimonButton[4];
            simonAudio = new SimonAudio(@"Content\Audio\Simon.NET.xgs", @"Content\Audio\Default.xwb", @"Content\Audio\Default.xsb");

            font = Content.Load<SpriteFont>(@"Fonts\GameFont");

            for (Int32 i = 0; i < simonButtons.Length; i++)
            {
                simonButtons[i] = new SimonButton
                    (
                    Content.Load<Texture2D>(String.Format(@"Buttons\Normal\{0}", (i + 1).ToString())),
                    Content.Load<Texture2D>(String.Format(@"Buttons\Glow\{0}", (i + 1).ToString()))
                    );
            }

            simonButtons[0].ButtonRectangle = new Rectangle(Convert.ToInt32(GraphicsDevice.Viewport.Width * 0.15), Convert.ToInt32(GraphicsDevice.Viewport.Height * 0.37),
                        Convert.ToInt32(simonButtons[0].NormalButton.Width * 0.8), Convert.ToInt32(simonButtons[0].NormalButton.Height * 0.8));
            simonButtons[1].ButtonRectangle = new Rectangle(Convert.ToInt32(GraphicsDevice.Viewport.Width * 0.40), Convert.ToInt32(GraphicsDevice.Viewport.Height * 0.10),
                        Convert.ToInt32(simonButtons[1].NormalButton.Width * 0.8), Convert.ToInt32(simonButtons[1].NormalButton.Height * 0.8));
            simonButtons[2].ButtonRectangle = new Rectangle(Convert.ToInt32(GraphicsDevice.Viewport.Width * 0.65), Convert.ToInt32(GraphicsDevice.Viewport.Height * 0.37),
                        Convert.ToInt32(simonButtons[2].NormalButton.Width * 0.8), Convert.ToInt32(simonButtons[2].NormalButton.Height * 0.8));
            simonButtons[3].ButtonRectangle = new Rectangle(Convert.ToInt32(GraphicsDevice.Viewport.Width * 0.40), Convert.ToInt32(GraphicsDevice.Viewport.Height * 0.65),
                        Convert.ToInt32(simonButtons[3].NormalButton.Width * 0.8), Convert.ToInt32(simonButtons[3].NormalButton.Height * 0.8));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

            simonAudio.Dispose();

            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            simonPlayer.CurrentGamePadState = GamePad.GetState(simonPlayer.SimonPlayerIndex);

            // Allows the game to exit
            if (simonPlayer.CurrentGamePadState.Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            // Part 1: If Simon is playing

            if (SimonStatus.IsSequencePlaying)
            {
                if (SimonStatus.IsStarting)
                {
                    if (SimonStatus.DelayTime == 30)
                    {
                        SimonStatus.DelayTime = 0;
                        SimonStatus.IsStarting = false;
                    }
                    else
                    {
                        SimonStatus.DelayTime++;
                    }
                }
                else
                {
                    if (SimonStatus.DelayTime == 30)
                    {
                        simonButtons[simonPattern[SimonStatus.CurrentPatternIndex]].IsLight = false;
                        SimonStatus.CurrentPatternIndex++;
                        SimonStatus.DelayTime = 0;

                        if (SimonStatus.CurrentPatternIndex == SimonStatus.CurrentSequenceLength)
                        {
                            SimonStatus.IsSequencePlaying = false;
                            SimonStatus.CurrentPatternIndex = 0;
                            SimonStatus.TimeLimit = 120 * SimonStatus.CurrentSequenceLength;
                            SimonStatus.CurrentTime = 0;
                        }
                    }
                    else
                    {
                        if (SimonStatus.DelayTime == 0)
                        {
                            simonAudio.Play((simonPattern[SimonStatus.CurrentPatternIndex] + 1).ToString());
                            simonButtons[simonPattern[SimonStatus.CurrentPatternIndex]].IsLight = true;
                        }

                        SimonStatus.DelayTime++;
                    }
                }
            }

            // Part 2: If Simon is waiting for user input - Xbox Controller

            else
            {
                if (simonPlayer.CurrentGamePadState.Buttons.X == ButtonState.Pressed &&
                    simonPlayer.PreviousGamePadState.Buttons.X == ButtonState.Released)
                {
                    UpdatePressedToReleased(0);
                }
                if (simonPlayer.CurrentGamePadState.Buttons.X == ButtonState.Released &&
                    simonPlayer.PreviousGamePadState.Buttons.X == ButtonState.Pressed)
                {
                    UpdateReleasedToPressed(0);
                }
                if (simonPlayer.CurrentGamePadState.Buttons.Y == ButtonState.Pressed &&
                    simonPlayer.PreviousGamePadState.Buttons.Y == ButtonState.Released)
                {
                    UpdatePressedToReleased(1);
                }
                if (simonPlayer.CurrentGamePadState.Buttons.Y == ButtonState.Released &&
                    simonPlayer.PreviousGamePadState.Buttons.Y == ButtonState.Pressed)
                {
                    UpdateReleasedToPressed(1);
                }
                if (simonPlayer.CurrentGamePadState.Buttons.B == ButtonState.Pressed &&
                    simonPlayer.PreviousGamePadState.Buttons.B == ButtonState.Released)
                {
                    UpdatePressedToReleased(2);
                }
                if (simonPlayer.CurrentGamePadState.Buttons.B == ButtonState.Released &&
                    simonPlayer.PreviousGamePadState.Buttons.B == ButtonState.Pressed)
                {
                    UpdateReleasedToPressed(2);
                }
                if (simonPlayer.CurrentGamePadState.Buttons.A == ButtonState.Pressed &&
                    simonPlayer.PreviousGamePadState.Buttons.A == ButtonState.Released)
                {
                    UpdatePressedToReleased(3);
                }
                if (simonPlayer.CurrentGamePadState.Buttons.A == ButtonState.Released &&
                    simonPlayer.PreviousGamePadState.Buttons.A == ButtonState.Pressed)
                {
                    UpdateReleasedToPressed(3);
                }
                if (!SimonStatus.IsGameStarted && simonPlayer.CurrentGamePadState.Buttons.Start == ButtonState.Released &&
                    simonPlayer.PreviousGamePadState.Buttons.Start == ButtonState.Pressed)
                {
                    UpdateStartGame();
                }

                simonPlayer.PreviousGamePadState = simonPlayer.CurrentGamePadState;

                // Part 3: If Simon is waiting for user input - Keyboard
#if !XBOX
                simonPlayer.CurrentKeyboardState = Keyboard.GetState();

                if (simonPlayer.CurrentKeyboardState.IsKeyDown(Keys.Left) &&
                    simonPlayer.PreviousKeyboardState.IsKeyUp(Keys.Left))
                {
                    UpdatePressedToReleased(0);
                }
                if (simonPlayer.CurrentKeyboardState.IsKeyUp(Keys.Left) &&
                    simonPlayer.PreviousKeyboardState.IsKeyDown(Keys.Left))
                {
                    UpdateReleasedToPressed(0);
                }
                if (simonPlayer.CurrentKeyboardState.IsKeyDown(Keys.Up) &&
                    simonPlayer.PreviousKeyboardState.IsKeyUp(Keys.Up))
                {
                    UpdatePressedToReleased(1);
                }
                if (simonPlayer.CurrentKeyboardState.IsKeyUp(Keys.Up) &&
                    simonPlayer.PreviousKeyboardState.IsKeyDown(Keys.Up))
                {
                    UpdateReleasedToPressed(1);
                }
                if (simonPlayer.CurrentKeyboardState.IsKeyDown(Keys.Right) &&
                    simonPlayer.PreviousKeyboardState.IsKeyUp(Keys.Right))
                {
                    UpdatePressedToReleased(2);
                }
                if (simonPlayer.CurrentKeyboardState.IsKeyUp(Keys.Right) &&
                    simonPlayer.PreviousKeyboardState.IsKeyDown(Keys.Right))
                {
                    UpdateReleasedToPressed(2);
                }
                if (simonPlayer.CurrentKeyboardState.IsKeyDown(Keys.Down) &&
                    simonPlayer.PreviousKeyboardState.IsKeyUp(Keys.Down))
                {
                    UpdatePressedToReleased(3);
                }
                if (simonPlayer.CurrentKeyboardState.IsKeyUp(Keys.Down) &&
                    simonPlayer.PreviousKeyboardState.IsKeyDown(Keys.Down))
                {
                    UpdateReleasedToPressed(3);
                }
                if (!SimonStatus.IsGameStarted && simonPlayer.CurrentKeyboardState.IsKeyUp(Keys.Space) &&
                    simonPlayer.PreviousKeyboardState.IsKeyDown(Keys.Space))
                {
                    UpdateStartGame();
                }

                simonPlayer.PreviousKeyboardState = simonPlayer.CurrentKeyboardState;

                // Part 4: If Simon is waiting for user input - Mouse

                simonPlayer.CurrentMouseState = Mouse.GetState();

                for (Int32 i = 0; i < simonButtons.Length; i++)
                {
                    if (simonPlayer.CurrentMouseState.X > simonButtons[i].ButtonRectangle.X && simonPlayer.CurrentMouseState.X < (simonButtons[i].ButtonRectangle.X + simonButtons[i].ButtonRectangle.Width)
                            && simonPlayer.CurrentMouseState.Y > simonButtons[i].ButtonRectangle.Y && simonPlayer.CurrentMouseState.Y < (simonButtons[i].ButtonRectangle.Y + simonButtons[i].ButtonRectangle.Height))
                    {
                        if (simonPlayer.CurrentMouseState.LeftButton == ButtonState.Pressed && simonPlayer.PreviousMouseState.LeftButton == ButtonState.Released)
                        {
                            UpdatePressedToReleased(i);
                        }
                        if (simonPlayer.CurrentMouseState.LeftButton == ButtonState.Released && simonPlayer.PreviousMouseState.LeftButton == ButtonState.Pressed)
                        {
                            UpdateReleasedToPressed(i);
                        }
                    }
                }
                if (!SimonStatus.IsGameStarted && simonPlayer.CurrentMouseState.RightButton == ButtonState.Pressed &&
                    simonPlayer.PreviousMouseState.RightButton == ButtonState.Released)
                {
                    UpdateStartGame();
                }

                simonPlayer.PreviousMouseState = simonPlayer.CurrentMouseState;
#endif
                // Part 5: If Simon's time is out

                if (SimonStatus.IsGameStarted && !SimonStatus.IsGameFailed && (SimonStatus.CurrentTime == SimonStatus.TimeLimit))
                {
                    String errorMessage = String.Format("Time is out!{0}You Failed!{0}{0}Hit{0}SPACE (Keyboard){0}START (Xbox){0}Right Button (Mouse){0}to Restart", Environment.NewLine);

                    UpdateFailedGame(errorMessage);
                }

                SimonStatus.CurrentTime++;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here

            GraphicsDevice.Clear(backgroundColor);

            spriteBatch.Begin(); // Required for start drawing.

            for (Int32 i = 0; i < simonButtons.Length; i++)
            {
                spriteBatch.Draw(simonButtons[i].IsLight ? simonButtons[i].GlowButton : simonButtons[i].NormalButton, simonButtons[i].ButtonRectangle, Color.White);
            }

            if (SimonStatus.IsGameStarted && !SimonStatus.IsGameFailed)
            {
                spriteBatch.DrawString(font, String.Format("Score: {0}", simonPlayer.Score.Value.ToString()), simonPlayer.Score.RenderPosition, Color.White);
            }
            else if (SimonStatus.IsGameFailed)
            {
                spriteBatch.DrawString(font, SimonStatus.StatusMessage, simonPlayer.Score.RenderPosition, Color.White);
            }
            else
            {
                spriteBatch.DrawString(font, String.Format("Hit{0}SPACE (Keyboard){0}START (Xbox){0}Right Button (Mouse){0}to Start", Environment.NewLine), simonPlayer.Score.RenderPosition, Color.White);
            }

            spriteBatch.End(); // Required for end drawing.

            base.Draw(gameTime);
        }

        #region Private Methods

        private void UpdateStartGame()
        {
            SimonStatus.IsGameFailed = false;

            simonPlayer.Score.Value = 0;

            simonPattern = new SimonPattern();

            SimonStatus.CurrentSequenceLength = 1;
            SimonStatus.CurrentPatternIndex = 0;

            SimonStatus.DelayTime = 0;

            SimonStatus.StatusMessage = String.Empty;

            SimonStatus.IsSequencePlaying = true;
            SimonStatus.IsStarting = true;

            SimonStatus.IsGameStarted = true;
        }

        private void UpdateReleasedToPressed(Int32 buttonIndex)
        {
            simonButtons[buttonIndex].IsLight = false;

            if (SimonStatus.StartNextSequence)
            {
                SimonStatus.IsSequencePlaying = true;
                SimonStatus.StartNextSequence = false;
            }
        }

        private void UpdatePressedToReleased(Int32 buttonIndex)
        {
            simonButtons[buttonIndex].IsLight = true; // light button
            simonAudio.Play((buttonIndex + 1).ToString()); // play sound

            if (SimonStatus.IsGameStarted)
            {
                if (simonPattern[SimonStatus.CurrentPatternIndex] == buttonIndex) // correct button input
                {
                    SimonStatus.IsGameFailed = false;

                    SimonStatus.CurrentPatternIndex++; // now checking next button in pattern

                    if (SimonStatus.CurrentPatternIndex == SimonStatus.CurrentSequenceLength) // if that was the last button in the current pattern
                    {
                        SimonStatus.CurrentSequenceLength++; // add next button to the current pattern
                        SimonStatus.CurrentPatternIndex = 0; // start playing pattern at button 0
                        SimonStatus.StartNextSequence = true; // start next pattern after button is done being pressed
                        SimonStatus.IsStarting = true; // we are at the start of the pattern

                        simonPlayer.Score.Value++;
                    }
                }
                else // not correct button
                {
                    String errorMessage = String.Format("You Hit the Wrong Button!{0}You Failed!{0}{0}Hit{0}SPACE (Keyboard){0}START (Xbox){0}Right Button (Mouse){0}to Restart", Environment.NewLine);

                    UpdateFailedGame(errorMessage);
                }
            }
        }

        private void UpdateFailedGame(String errorMessage)
        {
            simonAudio.Play("e"); // play "error" sound

            SimonStatus.IsGameFailed = true;
            SimonStatus.IsGameStarted = false;

            SimonStatus.StartNextSequence = false;

            SimonStatus.StatusMessage = errorMessage;
        }

        #endregion
    }
}
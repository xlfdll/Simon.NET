using System;
using System.Collections.Generic;
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
    // AudioEngine, WaveBank, SoundBank are in Microsoft.Xna.Framework.Xact.dll

    internal class SimonAudio : IDisposable
    {
        internal SimonAudio(String engine, String waveBank, String soundBank)
        {
            _audioEngine = new AudioEngine(engine);
            _waveBank = new WaveBank(_audioEngine, waveBank);
            _soundBank = new SoundBank(_audioEngine, soundBank);
        }

        private AudioEngine _audioEngine;
        private WaveBank _waveBank;
        private SoundBank _soundBank;

        internal SoundBank SoundBank
        {
            get { return _soundBank; }
        }

        internal void Play(String cueName)
        {
            _soundBank.PlayCue(cueName);
        }

        #region IDisposable Members

        public void Dispose()
        {
            _soundBank.Dispose();
            _waveBank.Dispose();
            _audioEngine.Dispose();
        }

        #endregion
    }
}
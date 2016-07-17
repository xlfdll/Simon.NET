using System;

namespace Simon.NET
{
    internal static class SimonStatus
    {
        private static Boolean _isGameStarted = false;
        private static Boolean _isSequencePlaying = false;
        private static Boolean _startNextSequence = false;

        private static Int32 _currentSequenceLength = 1;

        private static Int32 _currentTime = 0;
        private static Int32 _timeLimit = 0;

        private static Boolean _isStarting;
        private static Boolean _isGameFailed;

        private static Int32 _currentPatternIndex;
        private static Int32 _delayTime;

        private static String _statusMessage;

        internal static Boolean IsGameStarted
        {
            get { return SimonStatus._isGameStarted; }
            set { SimonStatus._isGameStarted = value; }
        }

        internal static Boolean IsGameFailed
        {
            get { return SimonStatus._isGameFailed; }
            set { SimonStatus._isGameFailed = value; }
        }

        internal static Boolean IsSequencePlaying
        {
            get { return SimonStatus._isSequencePlaying; }
            set { SimonStatus._isSequencePlaying = value; }
        }

        internal static Boolean IsStarting
        {
            get { return SimonStatus._isStarting; }
            set { SimonStatus._isStarting = value; }
        }

        internal static Boolean StartNextSequence
        {
            get { return SimonStatus._startNextSequence; }
            set { SimonStatus._startNextSequence = value; }
        }

        internal static Int32 CurrentSequenceLength
        {
            get { return SimonStatus._currentSequenceLength; }
            set { SimonStatus._currentSequenceLength = value; }
        }

        internal static Int32 CurrentPatternIndex
        {
            get { return SimonStatus._currentPatternIndex; }
            set { SimonStatus._currentPatternIndex = value; }
        }

        internal static Int32 DelayTime
        {
            get { return SimonStatus._delayTime; }
            set { SimonStatus._delayTime = value; }
        }

        internal static Int32 CurrentTime
        {
            get { return SimonStatus._currentTime; }
            set { SimonStatus._currentTime = value; }
        }

        internal static Int32 TimeLimit
        {
            get { return SimonStatus._timeLimit; }
            set { SimonStatus._timeLimit = value; }
        }

        internal static String StatusMessage
        {
            get { return SimonStatus._statusMessage; }
            set { SimonStatus._statusMessage = value; }
        }
    }
}
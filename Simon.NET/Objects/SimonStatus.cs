using System;

namespace Simon.NET
{
	internal static class SimonStatus
	{
		internal static Boolean IsGameStarted { get; set; } = false;
		internal static Boolean IsGameFailed { get; set; }
		internal static Boolean IsStarting { get; set; }
		internal static Boolean IsSequencePlaying { get; set; } = false;
		internal static Boolean StartNextSequence { get; set; } = false;
		internal static Int32 CurrentSequenceLength { get; set; } = 1;
		internal static Int32 CurrentPatternIndex { get; set; }
		internal static Int32 CurrentTime { get; set; } = 0;
		internal static Int32 DelayTime { get; set; }
		internal static Int32 TimeLimit { get; set; } = 0;
		internal static String StatusMessage { get; set; }
	}
}
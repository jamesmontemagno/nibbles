using System;
using Plugin.Settings.Abstractions;
using Plugin.Settings;

namespace Nibbles.Shared.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string HighScoreKey = "high_score_key";
		private static readonly Int64 HighScoreDefault = 0;
		private const string FirstTimeKey = "first_time_key";
		private static readonly bool FirstTimeDefault = true;

		#endregion

		public static bool FirstTime
		{
			get
			{
				return AppSettings.GetValueOrDefault(FirstTimeKey, FirstTimeDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue (FirstTimeKey, value);
			}
		}

		public static Int64 HighScore
		{
			get
			{
				return AppSettings.GetValueOrDefault (HighScoreKey, HighScoreDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue (HighScoreKey, (Int64)value);
			}
		}

	}
}

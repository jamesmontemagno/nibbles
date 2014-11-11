using System;
using Refractored.Xam.Settings.Abstractions;
using Refractored.Xam.Settings;

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

		#endregion


		public static Int64 HighScore
		{
			get
			{
				return AppSettings.GetValueOrDefault(HighScoreKey, (Int64)HighScoreDefault);
			}
			set
			{
				//if value has changed then save it!
				if (AppSettings.AddOrUpdateValue(HighScoreKey, (Int64)value))
					AppSettings.Save();
			}
		}

	}
}

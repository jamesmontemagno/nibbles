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
		private static readonly double HighScoreDefault = 0;

		#endregion


		public static double HighScore
		{
			get
			{
				return AppSettings.GetValueOrDefault(HighScoreKey, HighScoreDefault);
			}
			set
			{
				//if value has changed then save it!
				if (AppSettings.AddOrUpdateValue(HighScoreKey, value))
					AppSettings.Save();
			}
		}

	}
}

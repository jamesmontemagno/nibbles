using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.Xna.Framework;

using CocosSharp;
using Nibbles.Shared;

namespace NibblesAndroid
{
	[Activity (
		Label = "Nibbles",
		AlwaysRetainTaskState = true,
		Icon = "@drawable/ic_launcher",
		Theme = "@android:style/Theme.NoTitleBar",
		ScreenOrientation = ScreenOrientation.Landscape | ScreenOrientation.ReverseLandscape,
		LaunchMode = LaunchMode.SingleInstance,
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden)]
	public class MainActivity : AndroidGameActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var application = new CCApplication ();
			application.ApplicationDelegate = new GameAppDelegate ();
			SetContentView (application.AndroidContentView);
			application.StartGame ();
		}
	}
}



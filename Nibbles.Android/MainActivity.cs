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
			#if !DEBUG
			Xamarin.Insights.Initialize ("24e57f0b30120942dd4c385da58011842fe77c59");
			Xamarin.Insights.ForceDataTransmission = true;
			#endif

		

			var application = new CCApplication ();
			application.ApplicationDelegate = new GameAppDelegate ();
			GameAppDelegate.CurrentActivity = this;
			SetContentView (application.AndroidContentView);
			application.StartGame ();
		}
	}
}



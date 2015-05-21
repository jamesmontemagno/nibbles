using System;
using Foundation;
using UIKit;
using CocosSharp;
using Nibbles.Shared;

namespace Nibbles.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		//
		// This method is invoked when the application has loaded and is ready to run. In this
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override void FinishedLaunching (UIApplication app)
		{
			CCApplication application = new CCApplication ();
			application.ApplicationDelegate = new GameAppDelegate ();

			#if !DEBUG
			Xamarin.Insights.Initialize ("24e57f0b30120942dd4c385da58011842fe77c59");
			Xamarin.Insights.ForceDataTransmission = true;
			#endif

			application.StartGame ();
		}

		static void Main (string[] args)
		{
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}



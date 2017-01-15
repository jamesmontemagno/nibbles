using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.Xna.Framework;

using CocosSharp;
using Nibbles.Shared;
using System.Collections.Generic;
using System;

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
    public class MainActivity : Activity
	{

        CCGameView gameView;
        GameAppDelegate game;
		protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            game = new GameAppDelegate();

            // Get our game view from the layout resource,
            // and attach the view created event to it
            gameView = (CCGameView)FindViewById(Resource.Id.GameView);
            gameView.ViewCreated += LoadGame;
        }

        protected override void OnPause()
        {
            base.OnPause();
            game?.ApplicationDidEnterBackground();
        }

        protected override void OnResume()
        {
            base.OnResume();
            game?.ApplicationWillEnterForeground();
        }
        void LoadGame(object sender, EventArgs e)
        {
            if (gameView == null)
                return;

            game.Load(gameView);
        }

	}
}



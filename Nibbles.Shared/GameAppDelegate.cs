using System;
using CocosSharp;
using Nibbles.Shared.Layers;
using CocosDenshion;


namespace Nibbles.Shared
{
	public class GameAppDelegate : CCApplicationDelegate
	{
		#if __ANDROID__
		public static Android.App.Activity CurrentActivity { get; set; }
		#endif

		public override void ApplicationDidFinishLaunching (CCApplication application, CCWindow mainWindow)
		{
			application.PreferMultiSampling = false;
			application.ContentRootDirectory = "Content";
			application.ContentSearchPaths.Add ("animations");
			application.ContentSearchPaths.Add ("fonts");
			application.ContentSearchPaths.Add ("images");
			application.ContentSearchPaths.Add ("sounds");
			application.ContentSearchPaths.Add ("hd");

			var windowSize = mainWindow.WindowSizeInPixels;
			mainWindow.SetDesignResolutionSize(windowSize.Width, windowSize.Height, CCSceneResolutionPolicy.ExactFit);

			var scene = GameStartLayer.CreateScene (mainWindow);
			CCSimpleAudioEngine.SharedEngine.PlayBackgroundMusic ("sounds/backgroundMusic", true);
			CCSimpleAudioEngine.SharedEngine.PreloadEffect ("pop");
			CCSimpleAudioEngine.SharedEngine.PreloadEffect ("ring0");
			CCSimpleAudioEngine.SharedEngine.PreloadEffect ("ring1");
			CCSimpleAudioEngine.SharedEngine.PreloadEffect ("ring2");
			CCSimpleAudioEngine.SharedEngine.PreloadEffect ("ring3");
			CCSimpleAudioEngine.SharedEngine.PreloadEffect ("ring4");
			CCSimpleAudioEngine.SharedEngine.PreloadEffect ("ring5");
			CCSimpleAudioEngine.SharedEngine.PreloadEffect ("highscore");
			CCSimpleAudioEngine.SharedEngine.BackgroundMusicVolume = .6f;
			CCSimpleAudioEngine.SharedEngine.EffectsVolume = .5f;
			mainWindow.RunWithScene (scene);
		}

		public override void ApplicationDidEnterBackground (CCApplication application)
		{
			application.Paused = true;// if you use SimpleAudioEngine, your music must be paused
			CCSimpleAudioEngine.SharedEngine.PauseBackgroundMusic ();
		}

		public override void ApplicationWillEnterForeground (CCApplication application)
		{
			application.Paused = false;// if you use SimpleAudioEngine, your music must be paused
			CCSimpleAudioEngine.SharedEngine.ResumeBackgroundMusic ();
		}
	}
}

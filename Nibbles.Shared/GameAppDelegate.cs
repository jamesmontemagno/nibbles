using System;
using CocosSharp;
using Nibbles.Shared.Layers;
using CocosDenshion;


namespace Nibbles.Shared
{
	public class GameAppDelegate : CCApplicationDelegate
	{
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

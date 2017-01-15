using System;
using System.Collections.Generic;
using CocosSharp;
using Nibbles.Shared.Layers;


namespace Nibbles.Shared
{
	public class GameAppDelegate
	{
		public const string MainFont = "Roboto-Light.ttf";
        public static CCGameView GameView { get; set; }

        public void Load (CCGameView gameView)
		{
            GameView = gameView;
            GameView.ResolutionPolicy = CCViewResolutionPolicy.ShowAll;
			CCAudioEngine.SharedEngine.PlayBackgroundMusic ("sounds/backgroundMusic", true);
			CCAudioEngine.SharedEngine.PreloadEffect ("pop");
			CCAudioEngine.SharedEngine.PreloadEffect ("ring0");
			CCAudioEngine.SharedEngine.PreloadEffect ("ring1");
			CCAudioEngine.SharedEngine.PreloadEffect ("ring2");
			CCAudioEngine.SharedEngine.PreloadEffect ("ring3");
			CCAudioEngine.SharedEngine.PreloadEffect ("ring4");
			CCAudioEngine.SharedEngine.PreloadEffect ("ring5");
			CCAudioEngine.SharedEngine.PreloadEffect ("highscore");
			CCAudioEngine.SharedEngine.BackgroundMusicVolume = .6f;
			CCAudioEngine.SharedEngine.EffectsVolume = .5f;

            var contentSearchPaths = new List<string>() { "Fonts", "Sounds" };
            CCSizeI viewSize = gameView.ViewSize;

            int width = 1024;
            int height = 768;

            // Set world dimensions
            gameView.DesignResolution = new CCSizeI(width, height);

            // Determine whether to use the high or low def versions of our images
            // Make sure the default texel to content size ratio is set correctly
            // Of course you're free to have a finer set of image resolutions e.g (ld, hd, super-hd)
            if (width < viewSize.Width)
            {
                contentSearchPaths.Add("Images/Hd");
                CCSprite.DefaultTexelToContentSizeRatio = 2.0f;
            }
            else
            {
                contentSearchPaths.Add("Images/Ld");
                CCSprite.DefaultTexelToContentSizeRatio = 1.0f;
            }

            gameView.ContentManager.SearchPaths = contentSearchPaths;

            gameView.RunWithScene(GameStartLayer.CreateScene(gameView));

		}

		public void ApplicationDidEnterBackground ()
		{
            GameView.Paused = true;
			CCAudioEngine.SharedEngine.PauseBackgroundMusic ();
		}

		public void ApplicationWillEnterForeground ()
		{
            GameView.Paused = false;
			CCAudioEngine.SharedEngine.ResumeBackgroundMusic ();
		}
	}
}

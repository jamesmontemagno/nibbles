using System;
using CocosSharp;
using CocosDenshion;
using Nibbles.Shared.Helpers;

namespace Nibbles.Shared.Layers
{
	public class GameStartLayer : CCLayerColor
	{
		CCSprite logo;
		CCRepeatForever repeatedAction;
		CCLabel menuStart, menuTutorial, menuHighScore, developedBy; 

		public GameStartLayer () : base (CCColor4B.White)
		{
			Color = CCColor3B.White;
			Opacity = 255;

			// Define actions
			var moveUp = new CCMoveBy (1.0f, new CCPoint (0.0f, 50.0f));
			var moveDown = moveUp.Reverse ();

			// A CCSequence action runs the list of actions in ... sequence!
			var moveSeq = new CCSequence (new CCEaseBackInOut (moveUp), 
																					 new CCEaseBackInOut (moveDown));

			repeatedAction = new CCRepeatForever (moveSeq);

			CCSimpleAudioEngine.SharedEngine.PlayBackgroundMusic ("backgroundMusic", true);
			CCSimpleAudioEngine.SharedEngine.PreloadEffect ("pop");
			CCSimpleAudioEngine.SharedEngine.PreloadEffect ("ring0");
			CCSimpleAudioEngine.SharedEngine.PreloadEffect ("ring1");
			CCSimpleAudioEngine.SharedEngine.PreloadEffect ("ring2");
			CCSimpleAudioEngine.SharedEngine.PreloadEffect ("ring3");
			CCSimpleAudioEngine.SharedEngine.PreloadEffect ("ring4");
			CCSimpleAudioEngine.SharedEngine.PreloadEffect ("ring5");
			CCSimpleAudioEngine.SharedEngine.PreloadEffect ("highscore");
			CCSimpleAudioEngine.SharedEngine.BackgroundMusicVolume = .6f;
			CCSimpleAudioEngine.SharedEngine.EffectsVolume = .6f;
		}

		protected override void AddedToScene ()
		{
			base.AddedToScene ();

			var textColor = new CCColor3B (52, 152, 219);

			var startListener = new CCEventListenerTouchAllAtOnce ();
			var tutorialListener = new CCEventListenerTouchAllAtOnce ();
			var highScoreListener = new CCEventListenerTouchAllAtOnce ();
			var createdByListener = new CCEventListenerTouchAllAtOnce ();

			startListener.OnTouchesEnded = (touches, ccevent) => {

				if(ccevent.CurrentTarget != menuStart)
					return;
					
				var mainGame = Settings.FirstTime ? 
					GameTutorialLayer.CreateScene(Window, true) : 
					GameMainLayer.CreateScene (Window);

				Settings.FirstTime = false;

				var transitionToGameOver = new CCTransitionMoveInR (0.3f, mainGame);

				Director.ReplaceScene (transitionToGameOver);
			};

			tutorialListener.OnTouchesEnded = (touches, ccevent) => {

				if(ccevent.CurrentTarget != menuTutorial)
					return;

				var layer = GameTutorialLayer.CreateScene (Window);
				var transition = new CCTransitionMoveInR (0.3f, layer);

				Director.ReplaceScene (transition);
			};

			highScoreListener.OnTouchesEnded = (touches, ccevent) => {
			
				if(ccevent.CurrentTarget != menuHighScore)
					return;

				var layer = GameScoresLayer.CreateScene (Window);
				var transition = new CCTransitionMoveInR (0.3f, layer);

				Director.ReplaceScene (transition);
			};

			createdByListener.OnTouchesEnded = (touches, ccevent) => {

				//do stuff here in the future
				if(ccevent.CurrentTarget != developedBy)
					return;

				const string url = "http://mobile.twitter.com/jamesmontemagno";
				#if __ANDROID__

				try {

					var intent = new Android.Content.Intent (Android.Content.Intent.ActionView);
					intent.SetData (Android.Net.Uri.Parse (url));
					Android.App.Application.Context.StartActivity (intent);

				}
				catch (Exception ex) {
				}

				#elif __IOS__
					try {
						MonoTouch.UIKit.UIApplication.SharedApplication.OpenUrl (new MonoTouch.Foundation.NSUrl (url));
					}
					catch (Exception ex) {
					}
				#endif

			};

			CCRect bounds = VisibleBoundsWorldspace;

			developedBy = new CCLabel ("Created by @JamesMontemagno", "Roboto-Light", 36) {
				Position = new CCPoint (bounds.Size.Width / 2, 60),
				Color = textColor,
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle
			};

			AddChild (developedBy);
			AddEventListener (startListener, developedBy);


			menuStart = new CCLabel("START GAME", "Roboto-Light", 48) {
				Position = new CCPoint (bounds.Size.Width - 60, bounds.Size.Height / 2 + 100),
				Color = textColor,
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddleRight
			};

			AddChild (menuStart);
			AddEventListener (startListener, menuStart);

			menuTutorial = new CCLabel("TUTORIAL", "Roboto-Light", 48) {
				Position = new CCPoint (bounds.Size.Width - 60, bounds.Size.Height / 2),
				Color = textColor,
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddleRight
			};

			AddChild (menuTutorial);

			AddEventListener (tutorialListener, menuTutorial);

			menuHighScore = new CCLabel("SCORES", "Roboto-Light", 48) {
				Position = new CCPoint (bounds.Size.Width - 60, bounds.Size.Height / 2 - 100),
				Color = textColor,
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddleRight
			};

			AddChild (menuHighScore);
			AddEventListener (highScoreListener, menuHighScore);

			logo = new CCSprite ("title");

			// Layout the positioning of sprites based on visibleBounds
			logo.AnchorPoint = CCPoint.AnchorMiddleLeft;
			logo.Position = new CCPoint (120, bounds.Size.Height / 2);

			// Run actions on sprite
			// Note: we can reuse the same action definition on multiple sprites!
			logo.RunAction (repeatedAction);

			AddChild (logo);

		}

		public static CCScene CreateScene (CCWindow mainWindow)
		{
			var scene = new CCScene (mainWindow);
			var layer = new GameStartLayer ();

			scene.AddChild (layer);

			return scene;
		}

	
	}
}


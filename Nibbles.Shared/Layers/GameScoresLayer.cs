using System;
using System.Collections.Generic;
using CocosSharp;
using Nibbles.Shared.Helpers;

namespace Nibbles.Shared.Layers
{
	public class GameScoresLayer : CCLayerColor
	{
		CCSprite logo;
		CCRepeatForever repeatedAction;
		string scoreMessage = string.Empty;

		public GameScoresLayer ()
		{

			var touchListener = new CCEventListenerTouchAllAtOnce ();
			touchListener.OnTouchesBegan = (touches, ccevent) => {
				var mainGame = GameStartLayer.CreateScene (Window);
				var transitionToGameOver = new CCTransitionMoveInL (0.3f, mainGame);

				Director.ReplaceScene (transitionToGameOver);
			};

			AddEventListener (touchListener, this);

			scoreMessage = String.Format ("Current High Score");

			Color = new CCColor3B(127, 200, 205);

			Opacity = 255;

			var moveUp = new CCMoveBy (1.0f, new CCPoint (0.0f, 50.0f));
			var moveDown = moveUp.Reverse ();

			// A CCSequence action runs the list of actions in ... sequence!
			CCSequence moveSeq = new CCSequence (new CCEaseBackInOut (moveUp), new CCEaseBackInOut (moveDown));

			repeatedAction = new CCRepeatForever (moveSeq);

		}



		protected override void AddedToScene ()
		{
			base.AddedToScene ();

			Scene.SceneResolutionPolicy = CCSceneResolutionPolicy.ShowAll;
			CCRect bounds = VisibleBoundsWorldspace;
			var scoreLabel = new CCLabel (scoreMessage, GameAppDelegate.MainFont, 48, CCLabelFormat.SystemFont) {
				Position = new CCPoint (bounds.Size.Width / 2, bounds.Size.Height / 2 + 200),
				Color = CCColor3B.White,
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle
			};

			AddChild (scoreLabel);

			Scene.SceneResolutionPolicy = CCSceneResolutionPolicy.ShowAll;

			var scoreLabel2 = new CCLabel (scoreMessage, GameAppDelegate.MainFont, 64, CCLabelFormat.SystemFont) {
				Position = new CCPoint (bounds.Size.Width / 2, bounds.Size.Height / 2 + 100),
				Color = new CCColor3B (52, 152, 219),
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle,
				Text = Settings.HighScore.ToString()
			};

			AddChild (scoreLabel2);

			var playAgainLabel = new CCLabel ("Tap to Return", GameAppDelegate.MainFont, 36, CCLabelFormat.SystemFont) {
				Position = new CCPoint (bounds.Size.Width / 2, 60),
				Color = CCColor3B.White,
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle,
			};

			AddChild (playAgainLabel);

			var shareLabel = new CCLabel ("Share Score", GameAppDelegate.MainFont, 36, CCLabelFormat.SystemFont) {
				Position = new CCPoint (bounds.Size.Width - 120, bounds.Size.Height - 60),
				Color = CCColor3B.White,
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle,
			};

			AddChild (shareLabel);

		


			logo = new CCSprite ("title");
			// Layout the positioning of sprites based on visibleBounds
			logo.AnchorPoint = CCPoint.AnchorMiddle;
			logo.Position = new CCPoint (bounds.Size.Width / 2.0F, bounds.Size.Height / 4.0F + 50);

			// Run actions on sprite
			// Note: we can reuse the same action definition on multiple sprites!
			logo.RunAction (repeatedAction);

			AddChild (logo);

		}

		public static CCScene CreateScene (CCWindow mainWindow)
		{
			var scene = new CCScene (mainWindow);
			var layer = new GameScoresLayer ();

			scene.AddChild (layer);

			return scene;
		}

	}
}

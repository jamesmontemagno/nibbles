using System;
using System.Collections.Generic;
using CocosSharp;
using Nibbles.Shared.Helpers;

namespace Nibbles.Shared.Layers
{
	public class GameOverLayer : CCLayerColor
	{
		CCSprite logo;
		CCRepeatForever repeatedAction;
		string scoreMessage = string.Empty;
		bool highScore;

		public GameOverLayer (Int64 score)
		{

			var touchListener = new CCEventListenerTouchAllAtOnce ();
			touchListener.OnTouchesBegan = (touches, ccevent) => Window.DefaultDirector.ReplaceScene (GameMainLayer.CreateScene(Window));

			AddEventListener (touchListener, this);

			scoreMessage = String.Format ("Game Over.\nYour score: {0}", score);

			if (score > Settings.HighScore) {
				scoreMessage += "\nNew High Score!";
				Settings.HighScore = score;
				highScore = true;
			} else {
				scoreMessage += "\nCurrent High Score : " + Settings.HighScore;
			}

			Color = CCColor3B.White;

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

			var scoreLabel = new CCLabel (scoreMessage, "Roboto-Light", 48) {
				Position = new CCPoint (VisibleBoundsWorldspace.Size.Center.X, VisibleBoundsWorldspace.Size.Center.Y + 150),
				Color = new CCColor3B (52, 152, 219),
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle
			};

			AddChild (scoreLabel);

			var playAgainLabel = new CCLabel ("Tap to Play Again", "Roboto-Light", 36) {
				Position = new CCPoint (VisibleBoundsWorldspace.Size.Center.X, 60),
				Color = new CCColor3B (52, 152, 219),
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle,
			};

			AddChild (playAgainLabel);


			CCRect visibleBounds = VisibleBoundsWorldspace;
			CCPoint centerBounds = visibleBounds.Center;

			logo = new CCSprite ("title");
			// Layout the positioning of sprites based on visibleBounds
			logo.AnchorPoint = CCPoint.AnchorMiddle;
			logo.Position = new CCPoint (centerBounds.X, centerBounds.Y - 100);

			// Run actions on sprite
			// Note: we can reuse the same action definition on multiple sprites!
			logo.RunAction (repeatedAction);

			AddChild (logo);

		}

		public static CCScene CreateScene (CCWindow mainWindow, Int64 score)
		{
			var scene = new CCScene (mainWindow);
			var layer = new GameOverLayer (score);

			scene.AddChild (layer);

			return scene;
		}

	}
}

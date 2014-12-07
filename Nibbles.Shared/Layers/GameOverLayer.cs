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
			touchListener.OnTouchesBegan = (touches, ccevent) => {

				if(touches[0].Location.X < VisibleBoundsWorldspace.Size.Center.X)
					Window.DefaultDirector.ReplaceScene (GameStartLayer.CreateScene (Window));
				else
					Window.DefaultDirector.ReplaceScene (GameMainLayer.CreateScene (Window));
			};

			AddEventListener (touchListener, this);

			scoreMessage = String.Format ("Game Over.\nYour score: {0}", score);

			if (score > Settings.HighScore) {
				scoreMessage += "\nNew High Score!";
				Settings.HighScore = score;
				highScore = true;
			} else {
				scoreMessage += "\nCurrent High Score : " + Settings.HighScore;
			}

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
			var textColor = CCColor3B.White;
			var scoreLabel = new CCLabel (scoreMessage, "Roboto-Light", 48) {
				Position = new CCPoint (VisibleBoundsWorldspace.Size.Center.X, VisibleBoundsWorldspace.Size.Center.Y + 150),
				Color = textColor,
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle
			};

			AddChild (scoreLabel);

			var playAgainLabel = new CCLabel ("Play Again", "Roboto-Light", 36) {
				Position = new CCPoint (VisibleBoundsWorldspace.Size.Width - 120, 60),
				Color = textColor,
				HorizontalAlignment = CCTextAlignment.Left,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle,
			};

			AddChild (playAgainLabel);

			var mainmenu = new CCLabel ("Main Menu", "Roboto-Light", 36) {
				Position = new CCPoint (120, 60),
				Color = textColor,
				HorizontalAlignment = CCTextAlignment.Right,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle,
			};

			AddChild (mainmenu);


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

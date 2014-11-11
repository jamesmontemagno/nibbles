using System;
using CocosSharp;
using CocosDenshion;

namespace Nibbles.Shared.Layers
{
	public class GameStartLayer : CCLayerColor
	{
		CCSprite logo;
		CCRepeatForever repeatedAction;

		public GameStartLayer () : base ()
		{
			logo = new CCSprite ("title");
			var touchListener = new CCEventListenerTouchAllAtOnce ();
			touchListener.OnTouchesEnded = (touches, ccevent) => {
				var mainGame = GameMainLayer.CreateScene (Window);
				var transitionToGameOver = new CCTransitionMoveInR (0.3f, mainGame);

				Director.ReplaceScene (transitionToGameOver);
			};

			AddEventListener (touchListener, this);
			Color = CCColor3B.White;
			Opacity = 255;
			// Define actions
			var moveUp = new CCMoveBy (1.0f, new CCPoint (0.0f, 50.0f));
			var moveDown = moveUp.Reverse ();

			// A CCSequence action runs the list of actions in ... sequence!
			CCSequence moveSeq = new CCSequence (new CCEaseBackInOut (moveUp), new CCEaseBackInOut (moveDown));

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
			CCRect bounds = VisibleBoundsWorldspace;

			var label = new CCLabel("Tap to Start!", "Roboto-Light", 36) {
				Position = new CCPoint (bounds.Size.Width / 2 + 60, bounds.Size.Height / 2 + 60),
				Color = new CCColor3B (52, 152, 219),
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle
			};

			AddChild (label);

			var label2 = new CCLabel ("Created by @JamesMontemagno", "Roboto-Light", 36) {
				Position = new CCPoint (bounds.Size.Width / 2 + 140, 60),
				Color = new CCColor3B (52, 152, 219),
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle
			};

			AddChild (label2);

			var howTo = new CCLabel("How to play:\n1.) Draw lines to same color bubbles to score\n2.) Bubbles stop growing when chained\n3.) Bigger the bubble, bigger the score\n4.) Chain multiple bubbles for score multiplier", "Roboto-Light", 24) {
				Position = new CCPoint (275, bounds.Size.Height - 100),
				Color = new CCColor3B (52, 152, 219),
				HorizontalAlignment = CCTextAlignment.Left,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle
			};

			AddChild (howTo);


			CCRect visibleBounds = VisibleBoundsWorldspace;
			CCPoint centerBounds = visibleBounds.Center;

			// Layout the positioning of sprites based on visibleBounds
			logo.AnchorPoint = CCPoint.AnchorMiddle;
			logo.Position = new CCPoint (centerBounds.X, centerBounds.Y - 125);

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


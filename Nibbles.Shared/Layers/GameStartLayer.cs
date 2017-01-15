using System;
using CocosSharp;
using Nibbles.Shared.Helpers;
using Plugin.Share;

namespace Nibbles.Shared.Layers
{
    public class GameStartLayer : CCLayerColor
    {


        CCSprite logo;
        CCRepeatForever repeatedAction;
        CCLabel menuStart, menuTutorial, menuHighScore, developedBy;

        public GameStartLayer() : base(new CCColor4B(127, 200, 205))
        {
            Color = new CCColor3B(127, 200, 205);
            Opacity = 255;

            // Define actions
            var moveUp = new CCMoveBy(1.0f, new CCPoint(0.0f, 50.0f));
            var moveDown = moveUp.Reverse();

            // A CCSequence action runs the list of actions in ... sequence!
            var moveSeq = new CCSequence(new CCEaseBackInOut(moveUp),
                                                                                     new CCEaseBackInOut(moveDown));

            repeatedAction = new CCRepeatForever(moveSeq);
        }

        void StartGame(object stuff = null)
        {
            var mainGame = Settings.FirstTime ? GameTutorialLayer.CreateScene(GameAppDelegate.GameView, true) : GameMainLayer.CreateScene(GameAppDelegate.GameView);
            Settings.FirstTime = false;
            var transitionToGameOver = new CCTransitionMoveInR(0.3f, mainGame);
            Director.ReplaceScene(transitionToGameOver);
        }

        void StartTutorial(object stuff = null)
        {
            var layer = GameTutorialLayer.CreateScene(GameAppDelegate.GameView);
            var transition = new CCTransitionMoveInR(0.3f, layer);
            Director.ReplaceScene(transition);
        }

        void StartHighScores(object stuff = null)
        {
            var layer = GameScoresLayer.CreateScene(GameAppDelegate.GameView);
            var transition = new CCTransitionMoveInR(0.3f, layer);
            Director.ReplaceScene(transition);
        }

        static void StartDevelopedBy(object stuff = null)
        {
            const string url = "http://www.twitter.com/JamesMontemagno";
            CrossShare.Current.OpenBrowser(url);
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            var textColor = CCColor3B.White;


            CCRect bounds = VisibleBoundsWorldspace;

            developedBy = new CCLabel("Created by @JamesMontemagno", GameAppDelegate.MainFont, 36, CCLabelFormat.SystemFont)
            {
                /*Position = new CCPoint (bounds.Size.Width / 2, 60),*/
                Color = textColor,
                HorizontalAlignment = CCTextAlignment.Center,
                VerticalAlignment = CCVerticalTextAlignment.Center,
                AnchorPoint = CCPoint.AnchorMiddle
            };

            menuStart = new CCLabel("START GAME", GameAppDelegate.MainFont, 48, CCLabelFormat.SystemFont)
            {
                /*Position = new CCPoint (bounds.Size.Width - 60, bounds.Size.Height / 2 + 100),*/
                Color = textColor,
                HorizontalAlignment = CCTextAlignment.Right,
                VerticalAlignment = CCVerticalTextAlignment.Center,
                AnchorPoint = CCPoint.AnchorMiddleRight
            };


            menuTutorial = new CCLabel("TUTORIAL", GameAppDelegate.MainFont, 48, CCLabelFormat.SystemFont)
            {
                /*Position = new CCPoint (bounds.Size.Width - 60, bounds.Size.Height / 2),*/
                Color = textColor,
                HorizontalAlignment = CCTextAlignment.Right,
                VerticalAlignment = CCVerticalTextAlignment.Center,
                AnchorPoint = CCPoint.AnchorMiddleRight
            };


            menuHighScore = new CCLabel("SCORES", GameAppDelegate.MainFont, 48, CCLabelFormat.SystemFont)
            {
                /*Position = new CCPoint (bounds.Size.Width - 60, bounds.Size.Height / 2 - 100),*/
                Color = textColor,
                HorizontalAlignment = CCTextAlignment.Right,
                VerticalAlignment = CCVerticalTextAlignment.Center,

                AnchorPoint = CCPoint.AnchorMiddleRight
            };

            var menuItemStart = new CCMenuItemLabel(menuStart, StartGame);
            var menuItemTutorial = new CCMenuItemLabel(menuTutorial, StartTutorial);
            var menuItemScores = new CCMenuItemLabel(menuHighScore, StartHighScores);
            var menuItemCreatedBy = new CCMenuItemLabel(developedBy, StartDevelopedBy);

            var menu = new CCMenu(menuItemStart, menuItemTutorial, menuItemScores)
            {
                Position = new CCPoint(bounds.Size.Width / 1.5F, bounds.Size.Height / 2),
                AnchorPoint = CCPoint.AnchorMiddleRight
            };
            menu.AlignItemsVertically(50);


            AddChild(menu);

            var menu2 = new CCMenu(menuItemCreatedBy)
            {
                Position = new CCPoint(bounds.Size.Width / 2, 60),
                AnchorPoint = CCPoint.AnchorMiddle,
                Color = CCColor3B.White
            };

            AddChild(menu2);

            logo = new CCSprite("title");

            // Layout the positioning of sprites based on visibleBounds
            logo.AnchorPoint = CCPoint.AnchorMiddle;
            logo.Position = new CCPoint(bounds.Size.Width / 4, bounds.Size.Height / 2);

            // Run actions on sprite
            // Note: we can reuse the same action definition on multiple sprites!
            logo.RunAction(repeatedAction);

            AddChild(logo);

        }

        public static CCScene CreateScene(CCGameView mainWindow)
        {
            var scene = new CCScene(mainWindow);
            var layer = new GameStartLayer();

            scene.AddChild(layer);

            return scene;
        }


    }
}


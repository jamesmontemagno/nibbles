using Foundation;
using System;
using UIKit;
using Nibbles.Shared;

namespace Nibbles.iOS
{
    public partial class GameViewController : UIViewController
    {
        GameAppDelegate game;
        public GameViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            game = new GameAppDelegate();
            if (GameView != null)
            {
                // Set loading event to be called once game view is fully initialised
                GameView.ViewCreated += LoadGame;
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            game.ApplicationDidEnterBackground();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            game.ApplicationWillEnterForeground();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        void LoadGame(object sender, EventArgs e)
        {
            if (GameView == null)
                return;
            
            game.Load(GameView);
        }
    }
}
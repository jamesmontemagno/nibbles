using System;
using CocosSharp;
using System.Collections.Generic;
using CocosDenshion;

namespace Nibbles.Shared.Nodes
{
    public class Bubble : CCDrawNode
    {


        int bubblePoints = 50;
        float bubbleMax;
        float growthTime;
        CCColor4B color;
        CCColor4F colorF;
        CCScaleBy scale;
        CCMoveBy move1, move2;
        CCRepeatForever repeatedAction;

        static CCColor4B color0 = new CCColor4B(119, 208, 101, 255);
        static CCColor4B color1 = new CCColor4B(180, 85, 182, 255);
        static CCColor4B color2 = new CCColor4B(255, 255, 255, 255);//white
        static CCColor4B color3 = new CCColor4B(44, 62, 80, 255);
        static CCColor4B color4 = new CCColor4B(255, 255, 75, 255);//yellow

        public int Points
        {
            get { return (int)(bubblePoints * this.ScaleX); }
        }
        private int id;
        public static int UniversalId;
        public int Id
        {
            get { return id; }
        }

        public CCColor4B ColorId
        {
            get { return color; }
        }

        public CCColor4F ColorF
        {
            get { return colorF; }
        }

        public bool IsFrozen
        {
            get;
            set;
        }

        Random random;
        public Bubble()
        {
            UniversalId++;
            id = UniversalId;
            random = new Random();
            Scale = 0.5f;
            switch (random.Next(0, 5))
            {
                case 0:
                    color = color0;
                    break;
                case 1:
                    color = color1;
                    break;
                case 2:
                    color = color2;
                    break;
                case 3:
                    color = color3;
                    break;
                case 4:
                    color = color4;
                    break;
            }

            colorF = new CCColor4F(color);
            var size = CCRandom.Next(25, 50);
            this.ContentSize = new CCSize(size, size);
            this.DrawSolidCircle(this.Position, (float)size, color);


        }

        protected override void AddedToScene()
        {

            base.AddedToScene();
            bubbleMax = CCRandom.Next(4, 6);
            growthTime = CCRandom.Next(3, 5);
            scale = new CCScaleBy(growthTime, bubbleMax);
            this.AddAction(scale);

            move1 = new CCMoveBy(.2f, new CCPoint(4, 4));
            move2 = new CCMoveBy(.24f, new CCPoint(-4, -4));
            repeatedAction = new CCRepeatForever(move1, move2);
        }


        public void Freeze(int count)
        {
            IsFrozen = true;
            this.StopAllActions();
            try
            {

                CCAudioEngine.SharedEngine.PlayEffect("sounds/ring" + count, false);

            }
            catch
            {
            }

        }

        private void PopAnimation(CCLayer layer)
        {
            //play pop sound here
            this.StopAllActions();
            var pop = new CCParticleExplosion(this.Position);
            pop.EndColor = new CCColor4F(CCColor3B.Yellow);
            pop.AutoRemoveOnFinish = true;
            pop.BlendAdditive = true;
            pop.Life = 1.5F;
            pop.EmissionRate = 80;
            pop.StartColor = new CCColor4F(color);
            layer.AddChild(pop);
            CCAudioEngine.SharedEngine.PlayEffect("sounds/pop");
        }

        public void ForcePop(CCLayer layer)
        {

            PopAnimation(layer);
            this.RemoveFromParent(true);
        }

        public bool Pop(CCLayer layer)
        {

            if (this.NumberOfRunningActions == 1 && this.ScaleX + this.ScaleY > bubbleMax - 1.25)
            {
                RunAction(repeatedAction);
            }

            if (this.ScaleX + this.ScaleY < bubbleMax)
                return false;

            PopAnimation(layer);

            this.RemoveFromParent(true);
            return true;
        }

        public bool ContainsPoint(CCPoint toTest)
        {
            // Is the point inside the circle? Sum the squares of the x-difference and
            // y-difference from the centre, square-root it, and compare with theradius.
            // (This is Pythagoras' theorem.)

            var dX = Math.Abs(toTest.X - this.Position.X);
            var dY = Math.Abs(toTest.Y - this.Position.Y);

            var sumOfSquares = dX * dX + dY * dY;

            int distance = (int)Math.Sqrt(sumOfSquares);

            return (this.ScaledContentSize.Width + 15 >= distance);
        }

    }
}


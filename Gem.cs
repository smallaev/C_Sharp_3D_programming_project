using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class Gem : Collectable
    {
        SceneManager mSceneMgr;
        protected Stat score;
        protected int increase;
        protected Vector3 position;
        


        //public bool RemoveMe
        //{
        //    get { return removeMe; }
        //}

        protected Gem(SceneManager mSceneMgr, Stat score, Vector3 position)
        {
            this.mSceneMgr = mSceneMgr;
            this.score = score;
            this.position = position;
        }

        protected virtual void LoadModel()
        {
            // The link with to phisics engine goes here
            // (ignore until week 8) ...

            

        }

        public override void Update(FrameEvent evt)
        {
            Animate(evt);
            
            remove = isCollidingWith("Player");
            if (remove)
            {
                score.Increase(increase);
            }
            // Collision detection with the player goes here
            // (ignore until week 8) ...
            
        }

        protected bool isCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in physObj.CollisionList)
            {
                if (c.colliderObj.ID == objName || c.collidingObj.ID == objName)
                {
                    isColliding = true;
                    break;
                }
            }
            return isColliding;
        }


        public override void Animate(FrameEvent evt)
        {
            gameNode.Yaw(Mogre.Math.AngleUnitsToRadians(20) * evt.timeSinceLastFrame);
        }

        
    }
}

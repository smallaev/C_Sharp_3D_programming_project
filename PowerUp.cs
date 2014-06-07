using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    abstract class PowerUp:Collectable
    {
        protected Stat stat;
        protected Vector3 position;

        
        
        public Stat Stat            
        {
            set { stat = value; }
        }

        protected PowerUp(SceneManager mSceneMgr, Vector3 position)
        {
            this.mSceneMgr = mSceneMgr;
            this.position = position;
            
        }

        protected int increase;
        virtual protected void LoadModel() {
            
        }

       // public override void Update(FrameEvent evt)
       // {
       //     // Collision detection with the player goes here
       //     // (ignore until week 8) ...
       ////     remove = isCollidingWith("Player");

       // }

       // protected bool isCollidingWith(string objName)
       // {
       //     bool isColliding = false;
       //     foreach (Contacts c in physObj.CollisionList)
       //     {
       //         if (c.colliderObj.ID == objName || c.collidingObj.ID == objName)
       //         {
       //             isColliding = true;
       //             break;
       //         }
       //     }
       //     return isColliding;
       // }
    }
}

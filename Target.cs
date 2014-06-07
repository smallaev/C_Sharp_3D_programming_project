using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class Target : Collectable
    {

        SceneManager mSceneMgr;
        protected Stat score;
        protected Stat health;
        protected Stat lives;
        protected Stat shield;
        protected int increase;
        protected Vector3 position;
        Entity gameEntity;
        SceneNode gameNode;
        public bool remove1 = false;



        public Target(SceneManager mSceneMgr, Stat score, Stat health, Stat lives, Stat shield, Vector3 position)
    //        : base(mSceneMgr, score)
        {
            this.score = score;
            this.health = health;
            this.lives = lives;
            this.shield = shield;
            this.position = position;
            increase = 50;
            this.mSceneMgr = mSceneMgr;
            LoadModel();
        }

        protected void LoadModel()
        {
          
            remove = false;
            gameEntity = mSceneMgr.CreateEntity("Target.mesh");
            gameNode = mSceneMgr.CreateSceneNode();
            gameNode.AttachObject(gameEntity);
            gameNode.Scale(new Vector3(2, 2, 2));
            gameNode.Position = position;
     //       gameEntity.SetMaterialName("bGem");
            mSceneMgr.RootSceneNode.AddChild(gameNode);
            physObj = new PhysObj(10, "Target", 0.1f, 0.5f);
            physObj.SceneNode = gameNode;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));

            Physics.AddPhysObj(physObj);

        }

        public override void Animate(FrameEvent evt)
        {
            gameNode.Yaw(Mogre.Math.AngleUnitsToRadians(20) * evt.timeSinceLastFrame);
        }


        public override void Update(FrameEvent evt)
        {
            Animate(evt);

            remove = isCollidingWith("CannonBall");
            remove1 = isCollidingWith("Player");
            if (remove)
            {
                score.Increase(increase);
            }

            if (remove1)
            {
                score.Decrease(10);
                shield.Decrease(20);
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


        public override void SetPosition(Vector3 position)
        {
            gameNode.Position = position;
            physObj.Position = position;
        }

        public override void Dispose()
        {
            Physics.RemovePhysObj(physObj);
            physObj = null;
            if (gameNode.Parent != null)
                gameNode.Parent.RemoveChild(gameNode);
            gameNode.DetachAllObjects();
            gameNode.Dispose();
            gameEntity.Dispose();

        }

    }
}

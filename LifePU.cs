using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class LifePU : PowerUp
    {

        Entity gameEntity;
        SceneNode gameNode;
        

        public LifePU(SceneManager mSceneMgr, Vector3 position, Stat life)
            : base(mSceneMgr, position)
        {
            this.stat = life;
            increase = 1;
            this.position = position;
            LoadModel();
        }

        protected override void LoadModel()
        {
         //   base.LoadModel();
            remove = false;
            gameEntity = mSceneMgr.CreateEntity("Heart.mesh");
            gameNode = mSceneMgr.CreateSceneNode();
            gameNode.AttachObject(gameEntity);
            gameNode.Scale(new Vector3(7, 7, 7));
            gameNode.Position = position;
            gameEntity.SetMaterialName("rGem"); 
            mSceneMgr.RootSceneNode.AddChild(gameNode);

            physObj = new PhysObj(10, "LifePU", 0.1f, 0.5f);                             ////////// 3
            physObj.SceneNode = gameNode;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));

            Physics.AddPhysObj(physObj);                                                  /////////
        }

        public override void Update(FrameEvent evt)
        {
            Animate(evt);


            remove = isCollidingWith("Player");
            if (remove)
            {
                stat.Increase(increase);
            }
            //   base.Update(evt);
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



        public override void Dispose()
        {
            Physics.RemovePhysObj(physObj);
            physObj = null;
            //gameNode.Parent.RemoveChild(gameNode);
            gameNode.DetachAllObjects();
            gameNode.Dispose();
            gameEntity.Dispose();

        }
    }
}

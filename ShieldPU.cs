﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class ShieldPU : PowerUp
    {

        Entity gameEntity;
        SceneNode gameNode;
        Vector3 position;

        public ShieldPU(SceneManager mSceneMgr, Vector3 position, Stat shield)
            : base(mSceneMgr, position)
        {
            this.stat = shield;
            increase = 30;
            this.position = position;
            LoadModel();
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

        protected override void LoadModel()
        {
            base.LoadModel();
            remove = false;
            gameEntity = mSceneMgr.CreateEntity("shield.mesh");
            gameNode = mSceneMgr.CreateSceneNode();
            gameNode.AttachObject(gameEntity);
            gameNode.Scale(new Vector3(7, 7, 7));
            gameNode.Position = position;
  //          gameEntity.SetMaterialName("wGem");
            mSceneMgr.RootSceneNode.AddChild(gameNode);

            physObj = new PhysObj(10, "ShieldPU", 0.1f, 0.5f);                             ////////// 3
            physObj.SceneNode = gameNode;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));

            Physics.AddPhysObj(physObj);                                                  /////////
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

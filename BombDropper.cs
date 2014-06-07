using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class BombDropper : Gun
    {

  //      ModelElement gunNode;
        Vector3 position;

        Bomb bomb;

        public BombDropper(SceneManager mSceneMgr, Vector3 position)
           
        {
            this.mSceneMgr = mSceneMgr;
            ammo = new Stat();
            maxAmmo = 10;
            ammo.InitValue(maxAmmo);
            LoadModel();
        }

        protected override void LoadModel()
        {
            base.LoadModel();
    
            modelNode = mSceneMgr.CreateSceneNode();
            gameNode = modelNode;
            gameEntity = mSceneMgr.CreateEntity("BombDropperGun.mesh");
   //         modelNode.Scale(new Vector3(3, 3, 3));
            modelNode.AttachObject(gameEntity);
            modelNode.Position = position;            
     
        }

        public override void Fire()
            
        {
           
            if (ammo.Value != 0)
            {
                bomb = new Bomb(mSceneMgr);
                bomb.SetPosition(GunPosition() + 50 * GunDirection());
                
                MainClass.AddToList(bomb);
                ammo.Decrease(1);
            }

        }

        public override void ReloadAmmo() 
        {
            if (ammo.Value + 10 < maxAmmo)
            {
                ammo.Increase(10);
            }
            else
                ammo.Reset();
        }

        public void Update(FrameEvent evt)
        {
            Animate(evt);

            remove = isCollidingWith("Player");
            
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
        public override void Dispose()
        {
            //Physics.RemovePhysObj(physObj);
            //physObj = null;
            //if (modelNode.Parent != null)
            //    modelNode.Parent.RemoveChild(modelNode);
            //modelNode.DetachAllObjects();
            //modelNode.Dispose();
            //gameEntity.Dispose();

        }

    }
}

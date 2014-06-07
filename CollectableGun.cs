using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class CollectableGun : Collectable
    {
        Gun gun;
      
        public Gun Gun
        {
            get { return gun; }
        }

        Armoury playerArmoury;

        public Armoury PlayerArmoury
        {
            set { playerArmoury = value; }
        }

        public CollectableGun(SceneManager mSceneMgr, Gun gun, Armoury playerArmoury, Vector3 position)
        {
            // Initialize here the mSceneMgr, the gun and the playerArmoury fields to the values passed as parameters

            this.mSceneMgr = mSceneMgr;
            this.gun = gun;
            this.playerArmoury = playerArmoury;
            // Initialize the gameNode here, scale it by 1.5f using the Scale funtion, and add as its child the gameNode contained in the Gun object.
            // Finally attach the gameNode to the sceneGraph.
            //gunNode = new ModelElement(mSceneMgr);
            //gunNode.GameNode.Scale(new Vector3(1.5f, 1.5f, 1.5f));
            //gunNode.GameNode.AddChild(gun.GameNode);

            gameNode = mSceneMgr.CreateSceneNode();
            gameNode.AddChild(gun.modelNode);
            gameNode.Position = position;
            mSceneMgr.RootSceneNode.AddChild(gameNode);

            // Here goes the link to the physics engine
            // (ignore until week 8) ...
            physObj = new PhysObj(10, "CollectableGun", 0.01f, 0.7f, 0.3f);
            physObj.SceneNode = gameNode;
            physObj.Position = gameNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            Physics.AddPhysObj(physObj);
        }
        
        public override void Update(FrameEvent evt)
        {
   //         Animate(evt);
            //Here goes the collision detection with the player
            // (ignore until week 8) ...
            remove = isCollidingWith("Player");
            if (remove)
            {
                //
            }
            base.Update(evt);
        }
        
  

        protected bool isCollidingWith(string objName)
        {
            bool isColliding = false;
            foreach (Contacts c in physObj.CollisionList)
            {
                if (c.colliderObj.ID == objName || c.collidingObj.ID == objName)
                {
                    isColliding = true;
                    (gun.modelNode.Parent).RemoveChild(gun.modelNode.Name);

                    playerArmoury.AddGun(gun);
   //                 Dispose();
                    gun.Dispose();
                    break;
                }
            }
            return isColliding;
        }
        public override void Animate(FrameEvent evt)
        {
            gameNode.Rotate(new Quaternion(Mogre.Math.AngleUnitsToRadians(evt.timeSinceLastFrame * 10), Vector3.UNIT_Y));
        }

        public override void Dispose()
        {
            Physics.RemovePhysObj(physObj);
            physObj = null;
        }

    }
}

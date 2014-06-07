using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class CannonBall : Projectile
    {

        Entity projectileEntity;
        SceneNode projectileNode;

        public CannonBall(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            healthDamage = 10;
            shieldDamage = 5;
            speed = 3;
            initialVelocity = speed * initialDirection;
            Load();
        }

        protected override void Load()
        {
            projectileEntity = mSceneMgr.CreateEntity("Bomb.mesh");
            projectileNode = mSceneMgr.CreateSceneNode();
            projectileNode.AttachObject(projectileEntity);

            gameNode = projectileNode;
            mSceneMgr.RootSceneNode.AddChild(projectileNode);
            physObj = new PhysObj(10, "CannonBall", 1.5f, 0.5f);
            physObj.SceneNode = projectileNode;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));

            Physics.AddPhysObj(physObj);
        }

        public override void Update(FrameEvent evt)
        {
            remove = isCollidingWith("Player");
            if (!remove)
                remove = isCollidingWith("Target");
            if (!remove)
                remove = isCollidingWith("BlueGem");
            if (!remove)
                remove = isCollidingWith("RedGem");
            if (!remove)
                remove = isCollidingWith("PowerUp");
            if (!remove)
                remove = isCollidingWith("Robot");
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
            Physics.RemovePhysObj(physObj);
            physObj = null;
            if (projectileNode.Parent != null)
                projectileNode.Parent.RemoveChild(projectileNode);
            projectileNode.DetachAllObjects();
            projectileNode.Dispose();
            projectileEntity.Dispose();

        }

        public override void SetPosition(Vector3 position)
        {
            projectileNode.Position = position;
            physObj.Position = position;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class Bomb : Projectile
    {

        Entity bombEntity;
        SceneNode bombNode;
        SceneManager mSceneMgr;

        protected Stat score;
        protected Stat health;
        protected Stat lives;
        protected Stat shield;
        protected int increase;
        protected bool remove1 = false;



        public Bomb(SceneManager mSceneMgr)
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
            remove = false;
         
            bombEntity = mSceneMgr.CreateEntity("Bomb.mesh");
            bombNode = mSceneMgr.CreateSceneNode();
            bombNode.AttachObject(bombEntity);
            bombNode.Position = new Vector3(500, 300, 500);
            gameNode = bombNode;
            bombNode.Scale(new Vector3(3,3,3));
            mSceneMgr.RootSceneNode.AddChild(bombNode);
            physObj = new PhysObj(10, "Bomb", 0.1f, 0.5f);
            physObj.SceneNode = bombNode;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));

            Physics.AddPhysObj(physObj);
        }

        public override void Update(FrameEvent evt)
        {
            remove1 = isCollidingWith("Player");
            remove = isCollidingWith("Robot");
            //if (remove1)
            //{
            //    if (shield.Value > 0)
            //        shield.Decrease(30);
            //    else

            //        health.Decrease(20);
            //    score.Decrease(15);
            //}

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
            if (bombNode.Parent!=null)
            bombNode.Parent.RemoveChild(bombNode);
            bombNode.DetachAllObjects();
            bombNode.Dispose();
            bombEntity.Dispose();

        }

        public override void SetPosition(Vector3 position)
        {
            bombNode.Position = position;
            physObj.Position = position;
        }

    }
}

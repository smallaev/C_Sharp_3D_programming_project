using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;                                                                           ////// 1

namespace RaceGame
{
    class RedGem : Gem
    {
      
        Entity gemEntity;
        SceneNode gemNode;
        SceneManager mSceneMgr;
       

        public RedGem(SceneManager mSceneMgr, Stat score, Vector3 position)
            : base(mSceneMgr, score, position)
        {
            increase = 10;
            this.mSceneMgr = mSceneMgr;
            LoadModel();
           
        }

        protected override void LoadModel()
        {
            base.LoadModel();
            remove = false;                                                                          ///////2
            gemEntity = mSceneMgr.CreateEntity("Gem.mesh");
            gemNode = mSceneMgr.CreateSceneNode();
            gemNode.AttachObject(gemEntity);
            gemNode.Scale(new Vector3(3, 3, 3));
            gemEntity.SetMaterialName("rGem");
            gemNode.Position = position;
            mSceneMgr.RootSceneNode.AddChild(gemNode);

            physObj = new PhysObj(10, "RedGem", 0.1f, 0.5f);                             ////////// 3
            physObj.SceneNode = gemNode;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));

            Physics.AddPhysObj(physObj);                                                  /////////

        }

        //public override void Update(FrameEvent evt)
        //{
        //    remove = isCollidingWith("Player");                                       ////////4
        //}

        //protected bool isCollidingWith(string objName)                                     ////////////5
        //{
        //    bool isColliding = false;
        //    foreach (Contacts c in physObj.CollisionList)
        //    {
        //        if (c.colliderObj.ID == objName || c.collidingObj.ID == objName)
        //        {
        //            isColliding = true;
        //            break;
        //        }
        //    }
        //    return isColliding;
        //}                                                                               ////////////

        public override void Animate(FrameEvent evt)
        {
            gemNode.Yaw(Mogre.Math.AngleUnitsToRadians(20) * evt.timeSinceLastFrame);
        }



        public void SetPosition(Vector3 position)                                       //////////6
        {
            gemNode.Position = position;
            physObj.Position = position;
        }

        public override void Dispose()
        {
            Physics.RemovePhysObj(physObj);
            physObj = null;
            if (gemNode.Parent != null)
            gemNode.Parent.RemoveChild(gemNode);
            gemNode.DetachAllObjects();
            gemNode.Dispose();
            gemEntity.Dispose();

        }                                                                            /////////
    }
}

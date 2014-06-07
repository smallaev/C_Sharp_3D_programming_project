using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class BlueGem : Gem
    {

        Entity gemEntity;
        SceneNode gemNode;
        SceneManager mSceneMgr;
                 
       

        public BlueGem(SceneManager mSceneMgr, Stat score, Vector3 position)
            : base(mSceneMgr, score, position)
        {
            increase = 30;
            this.mSceneMgr = mSceneMgr;
            LoadModel();
        }

        protected override void LoadModel()
        {
 	         base.LoadModel();
             remove = false;
             gemEntity = mSceneMgr.CreateEntity("Gem.mesh");
             gemNode = mSceneMgr.CreateSceneNode();
             gemNode.AttachObject(gemEntity);
             gemNode.Scale(new Vector3(3, 3, 3));
             gemNode.Position = position;
             gemEntity.SetMaterialName("bGem");
             mSceneMgr.RootSceneNode.AddChild(gemNode);
             physObj = new PhysObj(10, "BlueGem", 0.01f, 0.5f);
             physObj.SceneNode = gemNode;
             physObj.AddForceToList(new WeightForce(physObj.InvMass));

             Physics.AddPhysObj(physObj);

        }

        public override void Animate(FrameEvent evt)
        {
            gemNode.Yaw(Mogre.Math.AngleUnitsToRadians(20) * evt.timeSinceLastFrame);
        }

       


        public void SetPosition(Vector3 position)
        {
            gemNode.Position = position;
            physObj.Position = position;
        }

        public override void Dispose()
        {
            Physics.RemovePhysObj(physObj);
            physObj = null;
            if (gemNode.Parent!=null)
            gemNode.Parent.RemoveChild(gemNode);
            gemNode.DetachAllObjects();
            gemNode.Dispose();
            gemEntity.Dispose();

        }

    }
}

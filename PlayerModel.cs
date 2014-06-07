using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class PlayerModel : CharacterModel
    {
        
        ModelElement hull;
        ModelElement power;
        ModelElement sphere;
        ModelElement hullGroupNode;
        ModelElement modelNode;
        ModelElement weelsGroupNode;
        ModelElement gunsGroupNode;
        ModelElement controlNode;
      //  PhysObj physObj;

        public PlayerModel(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            LoadModelElements();
            AssembleModel();
            
        }

        protected override void LoadModelElements() {
           
            hull = new ModelElement(mSceneMgr, "Main.mesh");
            power = new ModelElement(mSceneMgr, "PowerCells.mesh");
            sphere = new ModelElement(mSceneMgr, "Sphere.mesh");
            hullGroupNode = new ModelElement(mSceneMgr);    
            modelNode = new ModelElement(mSceneMgr);
            weelsGroupNode = new ModelElement(mSceneMgr);
            gunsGroupNode = new ModelElement(mSceneMgr);
            controlNode = new ModelElement(mSceneMgr);

            controlNode.SetPosition(new Vector3(0, 100, 0));
        }

        protected override void AssembleModel() 
        {

            controlNode.AddChild(modelNode.GameNode);
            modelNode.AddChild(hullGroupNode.GameNode);
            hullGroupNode.AddChild(power.GameNode);
            hullGroupNode.AddChild(hull.GameNode);
          
            hullGroupNode.AddChild(gunsGroupNode.GameNode);
            hullGroupNode.AddChild(weelsGroupNode.GameNode);
          
            weelsGroupNode.AddChild(sphere.GameNode);
            
            mSceneMgr.RootSceneNode.AddChild(controlNode.GameNode);
           
            float radius = 10;
       //     controlNode.GameNode.Position += radius * Vector3.UNIT_Y;
     //       modelNode.GameNode.Position += radius * Vector3.UNIT_Y;
            gameNode = controlNode.GameNode;
            physObj = new PhysObj(radius, "Player", 0.01f, 0.7f, 0.3f);
            physObj.SceneNode = controlNode.GameNode;
            physObj.Position = controlNode.GameNode.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            Physics.AddPhysObj(physObj);
        }

       
        public void AttachGun(Gun gun)
        {
            if (gunsGroupNode.GameNode.NumChildren() != 0)
            {
                gunsGroupNode.GameNode.RemoveAllChildren();

            }
            gunsGroupNode.GameNode.AddChild(gun.modelNode);
        }

        public override void DisposeModel()
        {
            hull.Dispose();
            power.Dispose();
            sphere.Dispose();
            gunsGroupNode.Dispose();
            weelsGroupNode.Dispose();
            hullGroupNode.Dispose();

            modelNode.Dispose();
        }
    }
}

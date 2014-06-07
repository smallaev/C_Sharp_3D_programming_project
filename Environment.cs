using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    /// <summary>
    /// This class implements the game environment
    /// </summary>
    class Environment
    {
        SceneManager mSceneMgr;             // This field will contain a reference of the scene managers
        RenderWindow mWindow;               // This field will contain a reference to the rendering window

        Ground ground;                      // This field will contain an istance of the ground object

        Light light;

        Cube cube;
        Entity cubeEntity;
        SceneNode cubeNode;

        Plane wall1;
        Plane wall2;
        Plane wall3;
        Plane wall4;

        Entity flagEntity;
        SceneNode flagNode;
        Plane usa;
        MeshPtr flagMeshPtr;
        Entity fStickEntity;
        SceneNode fStickNode;

       

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference to the scene manager</param>
        public Environment(SceneManager mSceneMgr, RenderWindow mWindow)
        {
            this.mSceneMgr = mSceneMgr;
            this.mWindow = mWindow;

            Load();                                 // This method loads  the environment
        }

        private void Load()
        {
            SetLights();
            SetShadows();

            SetSky();
            SetFog();
            ground = new Ground(mSceneMgr);
            
            usa = new Plane(Vector3.NEGATIVE_UNIT_X, -500);
            flagMeshPtr = MeshManager.Singleton.CreatePlane("flag", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, usa, 60, 100, 10, 6, true, 1, 1, 1, Vector3.UNIT_Z);
            flagEntity = mSceneMgr.CreateEntity("flag");
            flagEntity.SetMaterialName("flagMaterial");
            flagNode = mSceneMgr.CreateSceneNode();
            flagNode.AttachObject(flagEntity);
            flagNode.Position = new Vector3(0, 100, 0);

            fStickEntity = mSceneMgr.CreateEntity("stick.mesh");
            fStickNode = mSceneMgr.CreateSceneNode();
            fStickNode.AttachObject(fStickEntity);
            fStickNode.Position = new Vector3(501, 70, 45);
            mSceneMgr.RootSceneNode.AddChild(flagNode);
            mSceneMgr.RootSceneNode.AddChild(fStickNode);
            #region cube
            cube = new Cube(mSceneMgr);
            MeshPtr cubePtr = cube.getCube("myCube", "Wall", 1000, 100, 1000);
            cubeEntity = mSceneMgr.CreateEntity("Cube_Entity", "myCube");
            cubeNode = mSceneMgr.RootSceneNode.CreateChildSceneNode("Cube_Node");

            cubeNode.AttachObject(cubeEntity);

            wall1 = new Plane(Vector3.NEGATIVE_UNIT_X, -500);
            Physics.AddBoundary(wall1);

            wall2 = new Plane(Vector3.UNIT_X, -500);
            Physics.AddBoundary(wall2);

            wall3 = new Plane(Vector3.NEGATIVE_UNIT_Z, -500);
            Physics.AddBoundary(wall3);

            wall4 = new Plane(Vector3.UNIT_Z, -500);
            Physics.AddBoundary(wall4);

            //cube2 = new Cube(mSceneMgr);
            //MeshPtr cubePtr2 = cube2.getCube("myCube2", "Wall", 100, 100, 100);
            //cubeEntity2 = mSceneMgr.CreateEntity("Cube_Entity2", "myCube");
            //cubeNode2 = mSceneMgr.RootSceneNode.CreateChildSceneNode("Cube_Node");
            //cubeNode2.AttachObject(cubeEntity2);

            //cube3 = new Cube(mSceneMgr);
            //MeshPtr cubePtr3 = cube3.getCube("myCube3", "Wall", 100, 100, 100);
            //cubeEntity3 = mSceneMgr.CreateEntity("Cube_Entity3", "myCube");
            //cubeNode3 = mSceneMgr.RootSceneNode.CreateChildSceneNode("Cube_Node");
            //cubeNode3.AttachObject(cubeEntity3);

            //cube4 = new Cube(mSceneMgr);
            //MeshPtr cubePtr4 = cube4.getCube("myCube4", "Wall", 100, 100, 100);
            //cubeEntity4 = mSceneMgr.CreateEntity("Cube_Entity4", "myCube");
            //cubeNode4 = mSceneMgr.RootSceneNode.CreateChildSceneNode("Cube_Node");
            //cubeNode4.AttachObject(cubeEntity4);

            #endregion

            Physics.AddBoundary(ground.Plane);
     
        }

        /// <summary>
        /// This method dispose of any object instanciated in this class
        /// </summary>
        public void Dispose()
        {
            ground.Dispose();
            
            if (cubeNode != null)
            {
                cubeNode.Parent.RemoveChild(cubeNode);
                cubeNode.DetachAllObjects();
                cubeNode.Dispose();
                cubeNode = null;
            }
            if (cubeEntity != null)
            {
                cubeEntity.Dispose();
                cubeEntity = null;
            }
        }

        private void SetSky()
        {
            //mSceneMgr.SetSkyDome(true, "Sky", 1f, 10, 500, true);

           Plane sky = new Plane(Vector3.NEGATIVE_UNIT_Y, -100);
           mSceneMgr.SetSkyPlane(true, sky, "Sky", 10, 5, true, 0.5f, 100, 100,
                ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);

         //   mSceneMgr.SetSkyBox(true, "SkyBox", 10, true);
        }

        private void SetFog()
        {
            ColourValue fadeColour = new ColourValue(0.9f, 0.9f, 1f);
            //mSceneMgr.SetFog(FogMode.FOG_LINEAR, fadeColour, 0.1f, 100, 1000);
            //mSceneMgr.SetFog(FogMode.FOG_EXP, fadeColour, 0.001f);
            mSceneMgr.SetFog(FogMode.FOG_EXP2, fadeColour, 0.0015f);
            mWindow.GetViewport(0).BackgroundColour = fadeColour;
        }
        private void SetLights()
        {
            mSceneMgr.AmbientLight = new ColourValue(0.3f, 0.3f, 0.3f);                 // Set the ambient light in the scene

            light = mSceneMgr.CreateLight();                                            // Set an instance of a light;

            light.DiffuseColour = ColourValue.White;                                      // Sets the color of the light
            light.Position = new Vector3(0, 100, 0);                                    // Sets the position of the light

            //light.Type = Light.LightTypes.LT_DIRECTIONAL;                               // Sets the light to be a directional Light

            //light.Type = Light.LightTypes.LT_SPOTLIGHT;                                 // Sets the light to be a spot light
            //light.SetSpotlightRange(Mogre.Math.PI / 6, Mogre.Math.PI / 4, 0.001f);      // Sets the spot light parametes

            //light.Direction = Vector3.NEGATIVE_UNIT_Y;                                  // Sets the light direction

            light.Type = Light.LightTypes.LT_POINT;                                     // Sets the light to be a point light

            float range = 1000;                                                         // Sets the light range
            float constantAttenuation = 0;                                              // Sets the constant attenuation of the light [0, 1]
            float linearAttenuation = 0;                                                // Sets the linear attenuation of the light [0, 1]
            float quadraticAttenuation = 0.0001f;                                       // Sets the quadratic  attenuation of the light [0, 1]

            light.SetAttenuation(range, constantAttenuation,
                      linearAttenuation, quadraticAttenuation); // Not applicable to directional ligths
        }

        private void SetShadows()
        {
            //mSceneMgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_MODULATIVE;
            mSceneMgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_ADDITIVE;
        }
    }
}

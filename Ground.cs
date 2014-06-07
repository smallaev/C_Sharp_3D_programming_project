﻿using System;
using Mogre;

namespace RaceGame
{
    /// <summary>
    /// This class implements the ground of the environment
    /// </summary>
    class Ground
    {
        SceneManager mSceneMgr;

        Entity groundEntity;
        SceneNode groundNode;
        Plane plane;
        MeshPtr groundMeshPtr;


        int groundWidth = 10;
        int groundHeight = 10;
        int groundXSegs = 1;
        int groundZSegs = 1;
        int uTiles = 10;
        int vTiles = 10;


        public Plane Plane
        {
            get { return plane; }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference of the scene manager</param>
        public Ground(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            groundWidth = 1000;
            groundHeight = 1000;
            CreateGround();
        }

        /// <summary>
        /// This method initializes the ground mesh and node
        /// </summary>
        private void CreateGround()
        {
            GroundPlane();
            groundNode = mSceneMgr.CreateSceneNode();
            groundNode.AttachObject(groundEntity);
            mSceneMgr.RootSceneNode.AddChild(groundNode);                        
        }

        /// <summary>
        /// This method generate a plane in an Entity which will be used as ground plane
        /// </summary>
        private void GroundPlane()
        {
            plane = new Plane(Vector3.UNIT_Y, 0);
            groundMeshPtr = MeshManager.Singleton.CreatePlane("ground", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane, 10000, 10000, 10, 10, true, 1, 50, 50, Vector3.UNIT_Z);
            groundEntity = mSceneMgr.CreateEntity("ground");
            groundEntity.SetMaterialName("Ground");
        }

        /// <summary>
        /// This method disposes of the scene node and enitity
        /// </summary>
        public void Dispose()
        {
            groundNode.DetachAllObjects();
      //      groundNode.Parent.RemoveChild(groundNode);
            groundNode.Dispose();
            groundEntity.Dispose();
        }
    }
}

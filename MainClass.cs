using Mogre;
using Mogre.TutorialFramework;
using System;
using System.Collections.Generic;
using PhysicsEng;

namespace RaceGame
{
    class MainClass : BaseApplication
    {


        Player player;                        
        SceneNode cameraNode;
        InputsManager inputsManager = InputsManager.Instance;
        Environment environment;
        Physics physics;
        GameInterface gameHMD;
        Random random;

      //  CollectableGun cg;
        /// <summary>
        /// //////////////////////////////Lists declaration

        /// </summary>
        #region =======Lists============

        List<Gem> gemList = new List<Gem>();
        List<Gem> removeList = new List<Gem>();

        List<Target> targetList = new List<Target>();
        List<Target> removeTargetList = new List<Target>();

        List<PowerUp> powerUpList = new List<PowerUp>();
        List<PowerUp> removeListPU = new List<PowerUp>();

        List<Robot> robotList = new List<Robot>();
        List<Robot> removeRobotList = new List<Robot>();

        List<Bomb> bombList = new List<Bomb>();
        List<Bomb> removeListBombs = new List<Bomb>();

        List<Gun> gunList = new List<Gun>();
        List<BombDropper> removeListBD = new List<BombDropper>();

        List<CollectableGun> collectableGunList = new List<CollectableGun>();
        List<CollectableGun> removeCollectableGunList = new List<CollectableGun>();

   static     List<Projectile> projectileList = new List<Projectile>();
   static List<Projectile> removeProjectileList = new List<Projectile>();



        #endregion


       


        public static void Main()
        {
            new MainClass().Go(); 
                      // This method starts the rendering loop

        }
        
        /// <summary>
        /// This method create the initial scene
        /// </summary>
        protected override void CreateScene()
        {
            physics = new Physics();
           
            random = new Random();
            environment = new Environment(mSceneMgr, mWindow);

            

            #region ===========Player========
            player = new Player(mSceneMgr);
            player.Model.SetPosition(new Vector3(0, 50, 100));
            cameraNode = mSceneMgr.CreateSceneNode();
            cameraNode.AttachObject(mCamera);
            inputsManager.PlayerController = (PlayerController)player.Controller;
            player.Model.GameNode.AddChild(cameraNode);
            #endregion

            gameHMD = new GameInterface(mSceneMgr, mWindow, player.Stats);



            #region =================Items generator    LEVEL 1    ==========
            for (int i = 0; i < 3; i++)
            {
                gemList.Add(new BlueGem(mSceneMgr, ((PlayerStats)player.Stats).Score, new Vector3(random.Next(-500, 500), 0, random.Next(-500, 500))));
                gemList.Add(new RedGem(mSceneMgr, ((PlayerStats)player.Stats).Score, new Vector3(random.Next(-500, 500), 0, random.Next(-500, 500))));

               

            }
     //       collectableGunList.Add(new CollectableGun(mSceneMgr, new BombDropper(mSceneMgr, new Vector3(100, 0, 100)), player.PlayerArmoury));
         //   cg = new CollectableGun(mSceneMgr, new BombDropper(mSceneMgr, new Vector3(300, 70, 100)), player.PlayerArmoury);
           
            #endregion
            

            physics.StartSimTimer();
        }


        protected void CreateScene2()
        {
            Globals.currentLevel++;

            #region  ====== Items generator LEVEL 2 ===========
            for (int i = 0; i < 3; i++)
            {
                gemList.Add(new BlueGem(mSceneMgr, ((PlayerStats)player.Stats).Score, new Vector3(random.Next(-500, 500), 0, random.Next(-500, 500))));
                gemList.Add(new RedGem(mSceneMgr, ((PlayerStats)player.Stats).Score, new Vector3(random.Next(-500, 500), 0, random.Next(-500, 500))));

                                   
             //   powerUpList.Add(new HealthPU(mSceneMgr, new Vector3(random.Next(-500, 500), 10, random.Next(-500, 500)), player.Stats.Health));
                powerUpList.Add(new LifePU(mSceneMgr, new Vector3(random.Next(-500, 500), 10, random.Next(-500, 500)), player.Stats.Lives));
                powerUpList.Add(new ShieldPU(mSceneMgr, new Vector3(random.Next(-500, 500), 0, random.Next(-500, 500)), player.Stats.Shield));

                

            }

            for (int i = 0; i < 10; i++)
            {
                targetList.Add(new Target(mSceneMgr, ((PlayerStats)player.Stats).Score, ((PlayerStats)player.Stats).Health, ((PlayerStats)player.Stats).Lives, ((PlayerStats)player.Stats).Shield, new Vector3(random.Next(-500, 500), 0, random.Next(-500, 500))));
            }

             collectableGunList.Add(new CollectableGun(mSceneMgr, new BombDropper(mSceneMgr, new Vector3(300, 30, 100)), player.PlayerArmoury, new Vector3(300, 30, 100)));

            collectableGunList.Add(new CollectableGun(mSceneMgr, new Cannon(mSceneMgr, new Vector3(400, 30, 200)), player.PlayerArmoury, new Vector3(400, 30, 100)));
            
         //   gunList.Add(new BombDropper(mSceneMgr, new Vector3(random.Next(-500, 500), 10, random.Next(-500, 500))));
            #endregion

        }



                protected void CreateScene3()
                {
                    Globals.currentLevel++;

                    #region  ====== Items generator LEVEL 3 ===========
                    for (int i = 0; i < 10; i++)
                    {
                        gemList.Add(new BlueGem(mSceneMgr, ((PlayerStats)player.Stats).Score, new Vector3(random.Next(-500, 500), 0, random.Next(-500, 500))));
                        gemList.Add(new RedGem(mSceneMgr, ((PlayerStats)player.Stats).Score, new Vector3(random.Next(-500, 500), 0, random.Next(-500, 500))));
                        

                        powerUpList.Add(new HealthPU(mSceneMgr, new Vector3(random.Next(-500, 500), 10, random.Next(-500, 500)), player.Stats.Health));
                        powerUpList.Add(new LifePU(mSceneMgr, new Vector3(random.Next(-500, 500), 10, random.Next(-500, 500)), player.Stats.Lives));
                        powerUpList.Add(new ShieldPU(mSceneMgr, new Vector3(random.Next(-500, 500), 0, random.Next(-500, 500)), player.Stats.Shield));

                        robotList.Add(new Robot(mSceneMgr, ((PlayerStats)player.Stats).Score, ((PlayerStats)player.Stats).Health, ((PlayerStats)player.Stats).Lives, ((PlayerStats)player.Stats).Shield, new Vector3(random.Next(-500, 500), 0, random.Next(-500, 500))));

                    }



                    //   gunList.Add(new BombDropper(mSceneMgr, new Vector3(random.Next(-500, 500), 10, random.Next(-500, 500))));
                    #endregion

                }




        /// <summary>
        /// This method destrois the scene
        /// </summary>
        protected override void DestroyScene()
        {
            base.DestroyScene();
            gameHMD.Dispose();
            player.Model.Dispose();
            environment.Dispose();
            foreach (Gem g in gemList)
            {
                g.Dispose();
            }
            foreach (PowerUp p in powerUpList)
            {
                p.Dispose();
            }

        } 

        /// <summary>
        /// This method create a new camera
        /// </summary>
        protected override void CreateCamera()
        {     
            mCamera = mSceneMgr.CreateCamera("PlayerCam");
            mCamera.Position = new Vector3(0, 100, -200);
            mCamera.LookAt(new Vector3(0, 0, 0));
            mCamera.NearClipDistance = 5;
            mCamera.FarClipDistance = 1000;
            mCamera.FOVy = new Degree(70);
            mCameraMan = new CameraMan(mCamera);
            mCameraMan.Freeze = true;
            
        }

        /// <summary>
        /// This method create a new viewport
        /// </summary>
        protected override void CreateViewports()
        {      
            Viewport vieport = mWindow.AddViewport(mCamera);
            vieport.BackgroundColour = ColourValue.Black;
            mCamera.AspectRatio = vieport.ActualWidth / vieport.ActualHeight;
           
        }


       
        /// <summary>
        /// This method update the scene after a frame has finished rendering
        /// </summary>
        /// <param name="evt"></param>
        protected override void UpdateScene(FrameEvent evt)                                     /////////////Update scene
        {


           




            if (Globals.freezeGame == false)
            {
                physics.UpdatePhysics(0.01f);
                base.UpdateScene(evt);

                gameHMD.Update(evt);
                player.Update(evt);
                mCamera.LookAt(player.Position);

                ////////////////////////////

                if (Globals.currentLevel == 1)
                {

                    if (gemList.Count == 0)
                    {
                        Globals.freezeGame = true;
                        gameHMD.changeLevel(2);
                        
                    }                               
                    
                }
                if (Globals.currentLevel == 2)
                {

                    if (targetList.Count == 0)
                    {
                        Globals.freezeGame = true;
                        gameHMD.changeLevel(3);
                        
                    }

                }

                if (Globals.currentLevel == 3)
                {

                    if (robotList.Count == 0)
                    {
                        Globals.freezeGame = true;
                        gameHMD.changeLevel(4);

                    }

                }
                if (inputsManager.HideMessage && gemList.Count == 0 && Globals.currentLevel == 1)
                {
                   
                   
                        inputsManager.HideMessage = false;
                        CreateScene2();                        
                        Globals.freezeGame = false;
                        gameHMD.Time.Reset();
                        gameHMD.hideMessage();                   

                   
                }


                if (inputsManager.HideMessage && targetList.Count == 0 && Globals.currentLevel == 2)
                {


                    inputsManager.HideMessage = false;
                    CreateScene3();
                    Globals.freezeGame = false;
                    gameHMD.Time.Reset();
                    gameHMD.hideMessage();


                }

                if (inputsManager.HideMessage && robotList.Count == 0 && Globals.currentLevel == 3)
                {


               //     inputsManager.HideMessage = false;
               ////     CreateScene3();
               //     Globals.freezeGame = false;
               //     gameHMD.Time.Reset();
               //     gameHMD.hideMessage();


                }



                #region ================   Adding to Remove list  =================
                foreach (Gem gems in gemList)
                {
                    gems.Update(evt);
                    if (gems.RemoveMe)
                    {
                        removeList.Add(gems);
                    }


                }

                foreach (PowerUp lpu in powerUpList)
                {
                    lpu.Update(evt);
                    if (lpu.RemoveMe)
                        removeListPU.Add(lpu);


                }

                foreach (Target tr in targetList)
                {
                    tr.Update(evt);
                    if (tr.RemoveMe || tr.remove1)
                    {
                        removeTargetList.Add(tr);
                    }


                }

              //  cg.Update(evt);

                foreach (Bomb b in bombList)
                {
                    b.Update(evt);
                    if (b.RemoveMe)
                        removeListBombs.Add(bombList[0]);
                }

                foreach (CollectableGun cg in collectableGunList)
                {
                    cg.Update(evt);
                    if (cg.RemoveMe)
                    {
                        removeCollectableGunList.Add(cg);
                    }


                }
                foreach (Projectile cg in projectileList)
                {
                    cg.Update(evt);
                    if (cg.RemoveMe)
                    {
                        removeProjectileList.Add(cg);
                    }


                }


                foreach (Robot rb in robotList)
                {
                    rb.Update(evt);
                    if (rb.RemoveMe || rb.TouchesPlayer)
                        removeRobotList.Add(rb);


                }
                #endregion






                #region =============  Removing items ===================

                foreach (Bomb b in removeListBombs)
                {
                    bombList.Remove(b);
                    b.Dispose();
                }
                removeListBombs.Clear();

                foreach (Gem remGems in removeList)
                {
                    gemList.Remove(remGems);
                    remGems.Dispose();
                }
                removeList.Clear();


                foreach (PowerUp lpu in removeListPU)
                {
                    powerUpList.Remove(lpu);
                    lpu.Dispose();
                }
                removeListPU.Clear();

                foreach (BombDropper bd in removeListBD)
                {
                     gunList.Remove(bd);
                    bd.Dispose();
                }
                removeListBD.Clear();

                foreach (CollectableGun cgr in removeCollectableGunList)
                {
                    collectableGunList.Remove(cgr);
                    cgr.Dispose();
                }
                removeList.Clear();

                foreach (Projectile cgr in removeProjectileList)
                {
                    projectileList.Remove(cgr);
                    cgr.Dispose();
                }
                removeList.Clear();
                foreach (Target tr in removeTargetList)
                {
                    targetList.Remove(tr);
                    tr.Dispose();
                }
                removeTargetList.Clear();

                foreach (Robot rb in removeRobotList)
                {
                    robotList.Remove(rb);
                    rb.Dispose();
                }
                removeRobotList.Clear();

                #endregion
            }

            else
            {
                //if (inputsManager.CanRestart)
                //{
                //    DestroyScene();
                //    Globals.freezeGame = false;
                //    CreateScene();
                //}

            }
            
            

        }

        public static void AddToList(Projectile proj)
        {
            projectileList.Add(proj);
        }

      

       
            

        /// <summary>
        /// This method set create a frame listener to handle events before, during or after the frame rendering
        /// </summary>
        protected override void CreateFrameListeners()
        {
            base.CreateFrameListeners();
            mRoot.FrameRenderingQueued +=
                new FrameListener.FrameRenderingQueuedHandler(inputsManager.ProcessInput);
        }

        /// <summary>
        /// This method initilize the inputs reading from keyboard adn mouse
        /// </summary>
        protected override void InitializeInput()
        {
            base.InitializeInput();

            int windowHandle;
            mWindow.GetCustomAttribute("WINDOW", out windowHandle);
            inputsManager.InitInput(ref windowHandle);
        }
    }
}
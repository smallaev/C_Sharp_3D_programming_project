using System;
using System.Collections.Generic;
using Mogre;

namespace RaceGame
{
    /// <summary>
    /// This class implements an example of interface
    /// </summary>
    class GameInterface:HMD     // Game interface inherits form the Head Mounted Dispaly (HMD) class
    {
        private PanelOverlayElement panel;
        private PanelOverlayElement panelGameMessage;
        private OverlayElement scoreText;
        private OverlayElement timeText;
        private OverlayElement gameOver;
        private OverlayElement scoreGameOver;
        private OverlayElement levelCompleted;
        private OverlayElement newLevel;
        
        private OverlayElement healthBar;
        private OverlayElement shieldBar;
        private Overlay overlay3D;
        private Entity lifeEntity;
        private List<SceneNode> lives;
        private CharacterStats playerStats;
        private Timer time;
        public Timer Time
        {
            set { time = value; }
             get { return time; }
        }
        
        private float hRatio;
        private float sRatio;
        private string score = "Score: ";
        private string timer = "Timer: ";
        private string gover = "Game Over";
        private string levelCompletedText = "Level Completed! ";
        private string newLevelText = "Press F to go to level  ";
      
      


        static int i = 0;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference of a scene manager</param>
        /// <param name="playerStats">A reference to a character stats</param>
        public GameInterface(SceneManager mSceneMgr, 
            RenderWindow mWindow, CharacterStats playerStats)
            : base(mSceneMgr, mWindow, playerStats)  // this calls the constructor of the parent class
        {
            this.playerStats = playerStats;
            Load("GameInterface");
        }

        /// <summary>
        /// This method initializes the element of the interface
        /// </summary>
        /// <param name="name"> A name to pass to generate the overaly </param>
        protected override void Load(string name)
        {
            base.Load(name);
            time = new Timer();
            lives = new List<SceneNode>();
           
            healthBar = OverlayManager.Singleton.GetOverlayElement("HealthBar");
            hRatio = healthBar.Width / (float)characterStats.Health.Max;
            
            shieldBar = OverlayManager.Singleton.GetOverlayElement("ShieldBar");
            sRatio = shieldBar.Width / (float)characterStats.Shield.Max;

            scoreText = OverlayManager.Singleton.GetOverlayElement("ScoreText");
            scoreText.Caption = score;
            scoreText.Left = mWindow.Width * 0.5f;

            timeText = OverlayManager.Singleton.GetOverlayElement("TimerText");
            timeText.Caption = timer;
            timeText.Left = mWindow.Width * 0.5f;

            gameOver = OverlayManager.Singleton.GetOverlayElement("GameOver");
            gameOver.Caption = gover;
            gameOver.Left = mWindow.Width * 0.5f;
            gameOver.Hide();

            scoreGameOver = OverlayManager.Singleton.GetOverlayElement("ScoreGameOver");
          
            scoreGameOver.Left = mWindow.Width * 0.5f;
            gameOver.Hide();

            levelCompleted = OverlayManager.Singleton.GetOverlayElement("LevelCompleted");
            levelCompleted.Caption = levelCompletedText;
            levelCompleted.Left = mWindow.Width * 0.5f;
            levelCompleted.Hide();

            newLevel = OverlayManager.Singleton.GetOverlayElement("NewLevel");
            newLevel.Caption = newLevelText;
            newLevel.Left = mWindow.Width * 0.5f;
            newLevel.Hide();
            


            panelGameMessage =
        (PanelOverlayElement)OverlayManager.Singleton.GetOverlayElement("GameMessage");
            panelGameMessage.Width = mWindow.Width;
            panelGameMessage.Hide();

            panel = 
           (PanelOverlayElement) OverlayManager.Singleton.GetOverlayElement("GreenBackground");
            panel.Width = mWindow.Width;
            LoadOverlay3D();

          
       
        }

        /// <summary>
        /// This method initalize a 3D overlay
        /// </summary>
        private void LoadOverlay3D()
        {
            overlay3D = OverlayManager.Singleton.Create("3DOverlay"+(i++));
            overlay3D.ZOrder = 15000;

            CreateHearts();

            overlay3D.Show();
        }

        /// <summary>
        /// This method generate as many hearts as the number of lives left
        /// </summary>
        private void CreateHearts()
        {
            for (int i = 0; i < characterStats.Lives.Value; i++)
                AddHeart(i);
        }
        
        /// <summary>
        /// This method add an heart to the 3D overlay
        /// </summary>
        /// <param name="n"> A numeric tag</param>
        private void AddHeart(int n)
        {
            SceneNode livesNode = CreateHeart(n);
            lives.Add(livesNode);
            overlay3D.Add3D(livesNode);
        }

        /// <summary>
        /// This method remove from the 3D overlay and destries the passed scene node
        /// </summary>
        /// <param name="life"></param>
        private void RemoveAndDestroyLife(SceneNode life)
        {
            overlay3D.Remove3D(life);
            lives.Remove(life);
            MovableObject heart = life.GetAttachedObject(0);
            life.DetachAllObjects();
            life.Dispose();
            heart.Dispose();
        }

        /// <summary>
        /// This method initializes the heart node and entity
        /// </summary>
        /// <param name="n"> A numeric tag used to determine the heart postion on sceen </param>
        /// <returns></returns>
        private SceneNode CreateHeart(int n)
        {
            lifeEntity = mSceneMgr.CreateEntity("Heart.mesh");
            lifeEntity.SetMaterialName("HeartHMD");
            SceneNode livesNode;
            livesNode = new SceneNode(mSceneMgr);
            livesNode.AttachObject(lifeEntity);
            livesNode.Scale(new Vector3(0.15f, 0.15f, 0.15f));
            livesNode.Position = new Vector3(4f, 5f, -8) - n * 0.5f * Vector3.UNIT_X; ;
            livesNode.SetVisible(true);
            return livesNode;
        }

        /// <summary>
        /// This method converts milliseconds in to minutes and second format mm:ss
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private string convertTime(float time)
        {
            string convTime;
            float secs = time / 1000f;
            int min = (int)(secs / 60);
            secs = (int) secs % 60f;
            if (secs < 10)
                convTime = min + ":0" + secs;
            else
                convTime = min + ":" + secs;
            return convTime;
        }


        private string DecreaseTime(float time)
        {
            string convTime;
            float secs = time / 1000f;

            int min =  - (int)(secs / 60);

            secs = 59 - (int)secs % 60f;

            if (secs < 10)


                convTime = min + ":0" + secs;

            else

                convTime = min + ":" + secs;

            return convTime;

        }

        /// <summary>
        /// This method updates the interface
        /// </summary>
        /// <param name="evt"></param>
        public override void Update(FrameEvent evt)                                                      ////////////Update
        {

            if (Globals.freezeGame == false)
            {

                base.Update(evt);

                Animate(evt);

                if (lives.Count > characterStats.Lives.Value && characterStats.Lives.Value >= 0)
                {
                    SceneNode life = lives[lives.Count - 1];
                    RemoveAndDestroyLife(life);

                }
                if (lives.Count < characterStats.Lives.Value)
                {
                    AddHeart(characterStats.Lives.Value);
                }

                healthBar.Width = hRatio * characterStats.Health.Value;
                shieldBar.Width = sRatio * characterStats.Shield.Value;
                scoreText.Caption = score + ((PlayerStats)characterStats).Score.Value;


                timeText.Caption = timer + DecreaseTime(time.Milliseconds);

                if (timeText.Caption.Contains("-") || (playerStats.Lives.Value==0 && playerStats.Health.Value<=0))
                {
                    //
                    //    timeText.Caption = "-time's up-";
                    timeText.Hide();

                    panel.Hide();
                    panelGameMessage.Show();
                    gameOver.Show();
                    scoreGameOver.Caption = score + ((PlayerStats)characterStats).Score.Value;
                    scoreGameOver.Show();
                    Globals.freezeGame = true;


                }
            }

        }


        public void changeLevel(int level)
        {
          //  this.level = level;
            if (level == 4)
            {
                panel.Hide();
                panelGameMessage.Show();
                gameOver.Show();
                gameOver.Caption = "You won!";
                scoreGameOver.Caption = score + ((PlayerStats)characterStats).Score.Value;
                scoreGameOver.Show();
                Globals.freezeGame = true;
            }
            else
            {

                newLevel = OverlayManager.Singleton.GetOverlayElement("NewLevel");
                newLevel.Caption = newLevelText;
                newLevel.Left = mWindow.Width * 0.5f;
                newLevel.Caption = newLevelText + level;
                panelGameMessage.Show();
                levelCompleted.Show();
                newLevel.Show();
            }
        }
        public void hideMessage()
        {
            //  this.level = level;
            
            panelGameMessage.Hide();
            levelCompleted.Hide();
            newLevel.Hide();

        }

        /// <summary>
        /// This method animates the heart rotation
        /// </summary>
        /// <param name="evt"></param>
        protected override void Animate(FrameEvent evt)
        {
            foreach (SceneNode sn in lives)
                sn.Yaw(evt.timeSinceLastFrame);
        }


        /// <summary>
        /// This method disposes of the elements generated in the interface
        /// </summary>
        public override void Dispose()
        {
            List<SceneNode> toRemove = new List<SceneNode>();
            foreach (SceneNode life in lives)
            {
                toRemove.Add(life);
            }
            foreach (SceneNode life in toRemove)
            {
                RemoveAndDestroyLife(life);
            }
            if (lifeEntity!=null)
                lifeEntity.Dispose();
            toRemove.Clear();
            shieldBar.Dispose();
            healthBar.Dispose();
            scoreText.Dispose();
            panel.Dispose();
            overlay3D.Dispose();
            base.Dispose();
        }
    }
}

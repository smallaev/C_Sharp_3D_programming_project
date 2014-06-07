using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    /// <summary>
    /// This class implements a robot
    /// </summary>
    class Robot : CharacterModel
    {
        SceneManager mSceneMgr;     // A reference to the scene manager

        Entity robotEntity;         // The entity which will contain the robot mesh
        SceneNode robotNode;        // The node of the scene graph for the robot
        Vector3 position;
        Radian angle;           // Angle for the mesh rotation
        Vector3 direction;      // Direction of motion of the mesh for a single frame
        float radius;           // Radius of the circular trajectory of the mesh
        protected Stat score;
        protected Stat health;
        protected Stat lives;
        protected Stat shield;
        protected int increase;
        private bool touchesPlayer;

        public bool TouchesPlayer
        {
            get { return touchesPlayer; }
        }
        #region Part2
        const float maxTime = 2000f;        // Time when the animation have to be changed
        Timer time;                         // Timer for animation changes
        AnimationState animationState;      // Animation state, retrieves and store an animation from an Entity
        bool animationChanged;              // Flag which tells when the mesh animation has changed

        string animationName;               // Name of the animation to use
        public string AnimationName
        {
            set
            {
                HasAnimationChanged(value);
                if (IsValidAnimationName(value))
                    animationName = value;
                else
                    animationName = "Idle";
            }
        }
        #endregion

       
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference to the scene manager</param>
        public Robot(SceneManager mSceneMgr, Stat score, Stat health, Stat lives, Stat shield, Vector3 position)
        {
            increase = 100;
            this.score = score;
            this.health = health;
            this.lives = lives;
            this.shield = shield;
            this.position = position;
            this.mSceneMgr = mSceneMgr;
            Load();
            AnimationSetup();
        }

        /// <summary>
        /// This method loads the mesh and attaches it to a node and to the schenegraph
        /// </summary>
        private void Load()
        {
            robotEntity = mSceneMgr.CreateEntity("robot.mesh"); //make sure the mesh is in the Media/models folder 
            robotNode = mSceneMgr.CreateSceneNode();
            robotNode.AttachObject(robotEntity);
            robotNode.Position = position;
            mSceneMgr.RootSceneNode.AddChild(robotNode);

            physObj = new PhysObj(10, "Robot", 0.01f, 0.5f);
            physObj.SceneNode = robotNode;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));

            Physics.AddPhysObj(physObj);
        }


        public  void Update(FrameEvent evt)
        {
            Animate(evt);

            remove = isCollidingWith("CannonBall");
            if (!remove)
                remove = isCollidingWith("Bomb");
            if (remove)
            {
                score.Increase(increase);
            }
            touchesPlayer = isCollidingWith("Player");
            if (touchesPlayer)
            {
           
                if (shield.Value > 0)
                    shield.Decrease(35);
                else
                   
                    health.Decrease(30);
                score.Decrease(35);
            }

            if (health.Value <= 0)
            {
                lives.Decrease(1);
                if (lives.Value>0)
                health.Reset();
            }
            // Collision detection with the player goes here
            // (ignore until week 8) ...

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
        private void AnimationSetup()
        {
            radius = 0.01f;
            direction = Vector3.ZERO;
            angle = 0f;

            #region Part2: Uncomment for Part 3 of Lab 2
            time = new Timer();
            PrintAnimationNames();
            animationChanged = false;
            animationName = "Walk";
            LoadAnimation();
            #endregion
        }

        public override void Animate(FrameEvent evt)
        {
            CircularMotion(evt);
            AnimateMesh(evt);
        }
        private void CircularMotion(FrameEvent evt)
        {
            angle += (Radian)evt.timeSinceLastFrame;
            direction = radius * new Vector3(Mogre.Math.Cos(angle), 0, Mogre.Math.Sin(angle));
         //  direction = new Vector3(0.5f, 0, 0);
            robotNode.Translate(direction);
            robotNode.Yaw(-evt.timeSinceLastFrame);
        }

        #region Part2
        /// <summary>
        /// This method sets the animationChanged field to true whenever the animation name changes
        /// </summary>
        /// <param name="newName"> The new animation name </param>
        private void HasAnimationChanged(string newName)
        {
            if (newName != animationName)
                animationChanged = true;
        }

        /// <summary>
        /// This method prints on the console the list of animation tags
        /// </summary>
        private void PrintAnimationNames()
        {
            AnimationStateSet animStateSet = robotEntity.AllAnimationStates;     // Getd the set of animation states in the Entity
            AnimationStateIterator animIterator = animStateSet.GetAnimationStateIterator();  // Iterates through the animation states

            while (animIterator.MoveNext())                                       // Gets the next animation state in the set
            {
                Console.WriteLine(animIterator.CurrentKey);                      // Print out the animation name in the current key
            }
        }

        /// <summary>
        /// This method deternimes whether the name inserted is in the list of valid animation names
        /// </summary>
        /// <param name="newName">An animation name</param>
        /// <returns></returns>
        private bool IsValidAnimationName(string newName)
        {
            bool nameFound = false;

            AnimationStateSet animStateSet = robotEntity.AllAnimationStates;
            AnimationStateIterator animIterator = animStateSet.GetAnimationStateIterator();

            while (animIterator.MoveNext() && !nameFound)
            {
                if (newName == animIterator.CurrentKey)
                {
                    nameFound = true;
                }
            }

            return nameFound;
        }

        /// <summary>
        /// This method changes the animation name randomly
        /// </summary>
        private void changeAnimationName()
        {
            switch (0)       // Gets a random number between 0 and 4.5f
            {
                case 0:
                    {
                        AnimationName = "Walk";                 // I use the porperty here instead of the field to determine whether I am actualy changing the animation
                        break;
                    }
                case 1:
                    {
                        AnimationName = "Shoot";
                        break;
                    }
                case 2:
                    {
                        AnimationName = "Idle";
                        break;
                    }
                case 3:
                    {
                        AnimationName = "Slump";
                        break;
                    }
                case 4:
                    {
                        AnimationName = "Die";
                        break;
                    }
            }
        }

        /// <summary>
        /// This method loads the animation from the animation name
        /// </summary>
        private void LoadAnimation()
        {
            animationState = robotEntity.GetAnimationState(animationName);
            animationState.Loop = true;
            animationState.Enabled = true;
        }

        /// <summary>
        /// This method puts the mesh in motion
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the animation timings</param>
        private void AnimateMesh(FrameEvent evt)
        {
            if (time.Milliseconds > maxTime)
            {
                changeAnimationName();
                time.Reset();
            }

            if (animationChanged)
            {
                LoadAnimation();
                animationChanged = false;
            }

            animationState.AddTime(evt.timeSinceLastFrame);
        }
        #endregion
        /// <summary>
        /// This method detaches the robot node from the scene graph and destroies it and the robot enetity
        /// </summary>
        public override void Dispose()
        {
            Physics.RemovePhysObj(physObj);
            physObj = null;
            robotNode.Parent.RemoveChild(robotNode);
            robotNode.DetachAllObjects();
            robotNode.Dispose();
            robotEntity.Dispose();
        }
    }
}

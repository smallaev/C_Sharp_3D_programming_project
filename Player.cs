using System;
using Mogre;
using PhysicsEng;

namespace RaceGame
{
    class Player : Character
    {

        bool canMove = false;
        Armoury playerArmoury;


        public Armoury PlayerArmoury
        {
            get { return playerArmoury; }
            
        }
        public bool CanMove
        {
            get { return canMove; }
            set { canMove = value; }
        }
        public Player(SceneManager mSceneMgr)
        {

            model = new PlayerModel(mSceneMgr);

            controller = new PlayerController(this);

            stats = new PlayerStats();

            playerArmoury = new Armoury();
        }

        public override void Update(FrameEvent evt) 
        {
            model.Animate(evt);
            if (playerArmoury.GunChanged)
            {
                ((PlayerModel)model).AttachGun(playerArmoury.ActiveGun);

                playerArmoury.GunChanged = false;
            }
            controller.Update(evt);
        }

        public override void Shoot()
        {
          //  base.Shoot();
            if (playerArmoury.ActiveGun != null)
            {
                playerArmoury.ActiveGun.Fire();
            }
            controller.Shoot = false;

        
            
        }
    }
}
using System;
using System.Collections.Generic;
using Mogre;

namespace RaceGame
{
    class Armoury
    {
        bool gunChanged;
        Gun activeGun;
        //a list of gun object List<Gun> named collectedGuns with get property.
        List<Gun> collectedGuns = new List<Gun>();

        public bool GunChanged
        {
            get { return gunChanged; }
            set { gunChanged = value; }
        }
        public Gun ActiveGun
        {
            get { return activeGun; }
            set { activeGun = value; }
        }
        public List<Gun> CollectedGuns
        {
            get { return collectedGuns; }
        }

        public Armoury()
        {
            collectedGuns = new List<Gun>();
        }

        //Implement a public void Dispose method which dispose of each gun in the collectedGuns list and if the activeGun is not null disposes of it as well.
        public void Dispose()
        {
            for (int i = 0; i < collectedGuns.Count; i++) // Loop through List with for
            {
                collectedGuns[i].Dispose();
                if (collectedGuns[i] == activeGun)
                { 
                    if (activeGun != null)
                        activeGun.Dispose();
                }
            }
        }

        //Implement a public void ChangeGun method which takes as parameter a Gun and stores it in activeGun, it should also set the bool gunChanged to true.
        public void ChangeGun(Gun activeGun)
        {
            this.activeGun = activeGun;
            gunChanged = true;
        }

        //Implement a public void SwapGun method, which takes as parameter an integer index and if the collectedGuns 
        //and the activeGun are not null calls the ChangeGun method passing the gun in the collectedGuns in the list 
        //which has the index passed as parameter to the method (use the modulo operator and the .Count property of the 
        //list to make sure that the index stays in the limits).
        public void SwapGun(int index)
        {
            if (collectedGuns != null)
            {
                if (activeGun != null)
                {
                    ChangeGun(collectedGuns[index % collectedGuns.Count]);
                }
            }
        }

        public void AddGun(Gun gun)
        {
            //set a local bool variable named add to true
            bool add = true;
         //   ChangeGun(gun);
            //and then for each gun g in the collectedGuns list check whether add is true and whether the type of gun you are passing 
            //is in the collected gun list (g.GetType()==gun.GetType()), 
            for (int g = 0; g < collectedGuns.Count; g++) // Loop through List with for
            {
                if (g.GetType() == gun.GetType() && add)
                {
                    //if they are both true then it calls the reloadAmmo mehtod for g, 
                    //call the ChangeGun method passing g to it and then set add to false.
                    collectedGuns[g].ReloadAmmo();
                    ChangeGun(collectedGuns[g]);
                    add = false;
                }
            }
            //Once the for each loop finished check the add variable, if true then call CangeGun method and pass gun to 
            //it else call the Dispose method from gun.
            if (add)
            {
                ChangeGun(gun);
            }
            else
            {
                gun.Dispose();
            }
        }
    }
}
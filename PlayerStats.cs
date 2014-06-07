using System;
using Mogre;

namespace RaceGame
{
    class PlayerStats : CharacterStats
    {
        private Stat score;

        public Stat Score
        { 
        get {return score;}
        }


        public PlayerStats()
        {

            InitStats();
        }

        protected override void InitStats()
        {
            base.InitStats();
            score = new Score();
            score.InitValue(0);
            health.InitValue(100);
            shield.InitValue(100);
            lives.InitValue(5);
            lives.Decrease(5);
            shield.Decrease(90);

            
        }

    }
}

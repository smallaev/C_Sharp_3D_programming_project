using System;
using Mogre;

namespace RaceGame
{
    class Score : Stat
    {
        public override void Increase(int val)
        {
            value += val;
        }

      

    }
}

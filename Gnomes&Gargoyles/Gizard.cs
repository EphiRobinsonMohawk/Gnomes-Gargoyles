using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnomes_Gargoyles
{
    class Gizard
    {
        public int Lane { get; }
        public int Row { get; set; }
        public int Health { get; set; }

        public bool IsAlive = true;

        public bool HasPlayed = false;

        public Gizard(int lane, int row, int health)
        {
            Lane = lane;
            Row = row;
            Health = health;
        }
    }
}

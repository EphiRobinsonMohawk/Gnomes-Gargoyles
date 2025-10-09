using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnomes_Gargoyles
{
    public class Gnome
    {
        public int Lane { get;  }
        public int Row { get; set; }
        public int Health { get; set; }

        public bool IsAlive = true;

        public Gnome(int lane, int row, int health)
        {
            Lane = lane;
            Row = row;
            Health = health;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnomes_Gargoyles
{
    class Projectile
    {
        public int Lane { get; }
        public bool hasHit;
        public int Stage;

        public Projectile(int lane, int stage)
        {
            Lane = lane;
            Stage = stage;
        }
    }
}

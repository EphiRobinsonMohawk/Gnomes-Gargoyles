using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnomes_Gargoyles
{
    class EnemyProjectile
    {
        public int Lane { get; }
        public bool hasHit = false;
        public int Stage;

        public EnemyProjectile(int lane, int stage)
        {
            Lane = lane;
            Stage = stage;
        }
    }
}

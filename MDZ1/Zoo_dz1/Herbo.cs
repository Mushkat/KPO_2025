using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo_dz1
{
    public abstract class Herbo : Animal
    {
        public int LevelOfKindness { get; }
        protected Herbo(string name, int kindness) : base(name)
            => LevelOfKindness = kindness;
    }
}

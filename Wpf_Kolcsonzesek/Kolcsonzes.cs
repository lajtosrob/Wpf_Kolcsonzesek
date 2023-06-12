using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Kolcsonzesek
{
    class Kolcsonzes
    {
        string nev;
        char jarmuId;
        int elvitelOra;
        int elvitelPerc;
        int visszaOra;
        int visszaPerc;

        public Kolcsonzes(string nev, char jarmuId, int elvitelOra, int elvitelPerc, int visszaOra, int visszaPerc)
        {
            this.nev = nev;
            this.jarmuId = jarmuId;
            this.elvitelOra = elvitelOra;
            this.elvitelPerc = elvitelPerc;
            this.visszaOra = visszaOra;
            this.visszaPerc = visszaPerc;
        }

        public string Nev { get => nev;  }
        public char JarmuId { get => jarmuId;  }
        public int ElvitelOra { get => elvitelOra;  }
        public int ElvitelPerc { get => elvitelPerc;  }
        public int VisszaOra { get => visszaOra;  }
        public int VisszaPerc { get => visszaPerc;  }
    }

}

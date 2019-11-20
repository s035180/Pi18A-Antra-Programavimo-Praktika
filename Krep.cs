using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktinis2._2
{
    class Krep : Item
    {
        protected string busena;
        public void SetBus(string busena)
        {
            this.busena = busena;
        }
        public string GetBus()
        {
            return busena;
        }

    }
}

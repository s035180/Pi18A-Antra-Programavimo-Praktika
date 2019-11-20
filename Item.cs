using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktinis2._2
{
    class Item
    {
        protected string pavadinimas;
        protected int kiekis;
        protected double kaina;
        protected string CPU;
        protected string GPU;
        protected double memory;
        public void SetPav(string pavadinimas)
        {
            this.pavadinimas = pavadinimas;
        }
        public string GetPav()
        {
            return pavadinimas;
        }
        public void SetKiek(int kiekis)
        {
            this.kiekis = kiekis;
        }
        public int GetKiek()
        {
            return kiekis;
        }
        public void SetKaina(double kaina)
        {
            this.kaina = kaina;
        }
        public double GetKaina()
        {
            return kaina;
        }
        public void SetCPU(string CPU)
        {
            this.CPU = CPU;
        }
        public string GetCPU()
        {
            return CPU;
        }
        public void SetGPU(string GPU)
        {
            this.GPU = GPU;
        }
        public string GetGPU()
        {
            return GPU;
        }
        public void SetMem(double memory)
        {
            this.memory = memory;
        }
        public double GetMem()
        {
            return memory;
        }


    }
}

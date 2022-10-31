using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Vending_Machine
{
    internal class Drink
    {
        private string name;

        public string Name { get => this.name; set => this.name = value; }
        public double Price { get; private set; }

        public Drink(string name)
        {
            Random random = new Random();
                    this.Name = name;
                    this.Price = Math.Round(random.NextDouble() + 1,2);
        }

    }
}

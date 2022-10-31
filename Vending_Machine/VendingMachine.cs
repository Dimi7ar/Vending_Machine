using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_Machine
{
    internal class VendingMachine
    {
        private List<Drink> drinks = new List<Drink>()
        {
            new Drink("Water"),
            new Drink("Coffee"),
            new Drink("Coca cola"),
            new Drink("Pepsi"),
            new Drink("Sprite"),
            new Drink("Fanta")
        };

        private double moneyPut = 0;

        public double MoneyPut { get => this.moneyPut; set => moneyPut = value; }

        public List<Drink> Drinks { get => this.drinks; set => this.drinks = value; }
    }
}

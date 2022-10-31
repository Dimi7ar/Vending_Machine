using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;

namespace Vending_Machine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            VendingMachine machine = new VendingMachine();


            Console.WriteLine("Input:");
            Console.WriteLine("Number - to add money");
            Console.WriteLine("Add - to add a desired drink into the machine");
            Console.WriteLine("Buy - to buy a drink from the machine");
            Console.WriteLine("End - to stop the program and get your change");

            MachineEngine(machine);

            ChangeColor("white");
            Console.WriteLine($"Your change is: {machine.MoneyPut}$");
            ChangeColor("black");

            System.Threading.Thread.Sleep(10000);
        }

        static void AddDrink(VendingMachine machine)
        {
            Console.Write("Input the name of your desired drink, that you want to add: ");

            ChangeColor("white");
            Console.WriteLine("Input must be between 1 and 10 symbols.");
            ChangeColor("black");
            string drinkName;
            while ((drinkName = Console.ReadLine()).Length > 10 || drinkName.Length <= 0)
            {
                Console.WriteLine("Input must be between 1 and 10 symbols.");
            }


            machine.Drinks.Add(new Drink(drinkName));
            Console.Clear();

            ChangeColor("white");
            Console.WriteLine($"{drinkName} was added to the machine.");
            ChangeColor("black");
        }

        static void BuyDrink(VendingMachine machine)
        {
            Console.WriteLine("Input the number of the drink you want to buy: ");
            int howManyAreInTheMachine = machine.Drinks.Count;
            if (howManyAreInTheMachine == 0)
            {
                Console.Clear();
                Console.WriteLine("There is nothing in the machine!");
                return;
            }
            bool flag = false;
            int input = 0;
            try
            {
                input = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid input. Valid numbers are 1-{howManyAreInTheMachine}");

                flag = true;
            }

            while (input > howManyAreInTheMachine || input <= 0 || flag)
            {
                if (!flag)
                {
                   Console.WriteLine($"Invalid input. Valid numbers are 1-{howManyAreInTheMachine}");
                }

                try
                {
                    flag = false;
                    input = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine($"Invalid input. Valid numbers are 1-{howManyAreInTheMachine}");

                    flag = true;
                }
            }



            Drink boughtDrink = machine.Drinks[input - 1];
            if (boughtDrink.Price > machine.MoneyPut)
            {
                ChangeColor("white");
                Console.WriteLine("Insufficient funds, please insert more money into the machine");
                ChangeColor("black");
            }
            else
            {
                machine.MoneyPut -= boughtDrink.Price;
                machine.Drinks.Remove(boughtDrink);
                Console.Clear();
                ChangeColor("white");
                Console.WriteLine($"You bought {boughtDrink.Name} for {boughtDrink.Price}$. Enjoy your drink!");
                ChangeColor("black");
            }

        }

        static void MachineEngine(VendingMachine machine)
        {
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                Console.Clear();
                MachineArt(machine);

                int command = double.TryParse(input, out _) ? 1 : input.ToLower() == "add" ? 2 : input.ToLower() == "buy" ? 3 : -1;
                switch (command)
                {
                    case 1:
                        double number = double.Parse(input);
                        if (number <= 0)
                        {
                            ChangeColor("white");
                            Console.WriteLine("Invalid money inserted, please add a positive amount.");
                            ChangeColor("black");
                            break;
                        }

                        machine.MoneyPut += number;
                        Console.Clear();
                        ChangeColor("white");
                        Console.WriteLine($"{number:F2}$ added.");
                        ChangeColor("black");
                        MachineArt(machine);
                        break;
                    case 2:
                        if (machine.Drinks.Count == 9)
                        {
                            ChangeColor("white");
                            Console.WriteLine("Machine can hold only 9 drinks!");
                            ChangeColor("black");
                        }
                        else
                        {
                           AddDrink(machine);
                           MachineArt(machine);
                        }
                        break;
                    case 3:
                        BuyDrink(machine);
                        MachineArt(machine);
                        break;
                    case -1:
                        Console.WriteLine("Invalid input, please use a valid command");
                        break;
                }
            }
        }

        static void MachineArt(VendingMachine machine)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ˍˍˍˍˍˍˍˍˍˍˍˍˍˍˍˍˍˍˍˍˍˍˍˍˍˍ");
            for (int i = 0; i < machine.Drinks.Count; i++)
            {
                Drink drink = machine.Drinks[i];
                int number = drink.Name.Length - 10;
                number = Math.Abs(number);
                sb.Append($"| {i + 1} - {drink.Name}");
                sb.Append(string.Concat(Enumerable.Repeat(" ", number)));
                sb.Append($"  o {drink.Price:F2} |");
                sb.AppendLine();
            }

            sb.AppendLine("|                        |");
            sb.AppendLine("|                        |");
            sb.AppendLine("|                        |");
            sb.AppendLine("|                        |");
            sb.AppendLine("|         ______         |");
            sb.AppendLine("|        |______|        |");
            sb.AppendLine("|                        |");
            sb.AppendLine("ˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉˉ");
            sb.AppendLine($"Total money: {machine.MoneyPut:F2}$");
            Console.WriteLine(sb.ToString().TrimEnd());
        }

        static void ChangeColor(string color)
        {
            switch (color)
            {
                case "white":
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case "black":
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
    }
}

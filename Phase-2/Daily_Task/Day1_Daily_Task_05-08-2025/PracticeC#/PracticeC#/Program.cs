using System;
using System.Collections.Generic;
using System.Linq;

namespace Day1_Practice
{
    public enum SnackType
    {
        Chips,
        Chocolate,
        Biscuits,
        Juice
    }

    struct Snack
    {
        public string name;
        public int price;
        public SnackType type;

        public Snack(string name, int price, SnackType type)
        {
            this.name = name;
            this.price = price;
            this.type = type;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Snack mySnack = new Snack();
            mySnack.name = " Lays Classic ,";
            mySnack.price = 30;
            mySnack.type = SnackType.Chips;

            Snack friendSnack = new Snack("KitKat", 45, SnackType.Chocolate);

            Snack officeSnack = new Snack()
            {
                name = "Oreo",
                price = 20,
                type = SnackType.Biscuits
            };

            List<Snack> allSnacks = new List<Snack>();
            allSnacks.Add(mySnack);
            allSnacks.Add(friendSnack);
            allSnacks.Add(officeSnack);

            Func<List<Snack>, int> totalSnacks = (List<Snack> snacks) => snacks.Count;

            var cheapSnacks = allSnacks.Where(s => s.price < 30);

            Console.WriteLine("My Snack: " + mySnack.name + " Price:" + mySnack.price + " Type: " + mySnack.type);
            Console.WriteLine("Friend's Snack: " + friendSnack.name + " Price:" + friendSnack.price);
            Console.WriteLine("Office Snack: " + officeSnack.name + " Price:" + officeSnack.price);

            Console.WriteLine("\nTotal Snacks: " + totalSnacks(allSnacks));

            Console.WriteLine("\nCheap Snacks less than 30:");
            foreach (var snack in cheapSnacks)
            {
                Console.WriteLine("- " + snack.name + " (" + snack.price + ")");
            }
        }
    }
}

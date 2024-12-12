using System;
using System.Collections.Generic;
using System.Linq;

namespace SoldiersTransfer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int firstPlatoonCount = 20;
            int secondPlatoonCount = 5;
            List<string> possibleNames = new List<string>
            {
                "Богдан", "Борис", "Баян",
                "Бронислав", "Бонифатий", "Богумил",
                "Саня", "Лёха", "Максимыч",
                "Михал", "Костян", "Спиридон"
            };
            SoldiersFactory soldiersFactory = new SoldiersFactory(possibleNames);
            Transfer transfer = new Transfer(soldiersFactory.Create(firstPlatoonCount), soldiersFactory.Create(secondPlatoonCount));

            transfer.Execute();
        }
    }

    class Transfer
    {
        private List<Soldier> _platoon1;
        private List<Soldier> _platoon2;

        public Transfer(List<Soldier> platoon1, List<Soldier> platoon2)
        {
            _platoon1 = platoon1;
            _platoon2 = platoon2;
        }

        public void Execute()
        {
            string firstPlatoonName = "Взвод 1";
            string secondPlatoonName = "Взвод 2";
            string requiredLiteral = "Б";

            Console.WriteLine("BEFORE");
            ShowInfo(_platoon1, firstPlatoonName);
            ShowInfo(_platoon2, secondPlatoonName);

            var transferedSoldiers = _platoon1.Where(soldier => soldier.Name.StartsWith(requiredLiteral));

            _platoon1 = _platoon1.Except(transferedSoldiers).ToList();
            _platoon2 = _platoon2.Union(transferedSoldiers).ToList();

            Console.WriteLine("AFTER");
            ShowInfo(_platoon1, firstPlatoonName);
            ShowInfo(_platoon2, secondPlatoonName);
        }

        private void ShowInfo(List<Soldier> platoon, string platoonName)
        {
            int count = 1;

            Console.WriteLine(platoonName);

            foreach (Soldier soldier in platoon)
            {
                Console.WriteLine($"{count++} Имя - {soldier.Name}.");
                
            }
        }
    }

    class SoldiersFactory
    {
        private List<string> _names;

        public SoldiersFactory(List<string> names)
        {
            _names = names;
        }

        public List<Soldier> Create(int count)
        {
            List<Soldier> soldiers = new List<Soldier>();

            for (int i = 0; i < count; i++)
            {
                soldiers.Add(new Soldier(_names[UserUtills.GenerateRandomPosiriveLimitedNumber(_names.Count)]));
            }

            return soldiers;
        }
    }

    class Soldier
    {
        public string Name { get; }

        public Soldier(string name)
        {
            Name = name;
        }
    }

    static class UserUtills
    {
        private static Random s_random = new Random();

        public static int GenerateRandomPosiriveLimitedNumber(int maxValueExclusive)
        {
            return s_random.Next(maxValueExclusive);
        }
    }
}
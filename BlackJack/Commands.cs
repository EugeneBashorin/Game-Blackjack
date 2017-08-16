using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public static class Commands
    {
        public static string AskName()
        {
            var entered = false;
            string name = string.Empty;

            while (!entered)
            {
                Console.Write("Введите имя: ");
                name = Console.ReadLine();

                if (!string.IsNullOrEmpty(name))
                {
                    entered = true;
                }
            }
            return name;
        }

        public static bool AskStart(string name)
        {
            var started = false;

            while (!started)
            {
                Console.WriteLine(name + ", для начала игры нажмите ENTER");
                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("Игра начата! Да победит сильнейший!");
                    started = true;
                }
            }
            return started;
        }

        public static void Warn(string message)
        {
            Console.WriteLine(message);
        }

        public static void FirstDeal()
        {
            Console.WriteLine("Игроки получают по две карты");
        }

        public static bool AskPlayerToTakeACard(string name)
        {
            var answer = string.Empty;

            while (!answer.Equals("yes") && !answer.Equals("no"))
            {
                Console.Write(name + ", ещё карту? (yes/no)");
                answer = Console.ReadLine();
            }

            return answer.Equals("yes");
        }

        public static void AskComputerToTakeACard(string name)
        {
                Console.WriteLine(name + ", ещё карту? (yes/no)");
        }

        public static void Congrat(string name)
        {
            Console.Write(name + ", победа Ваша!");
        }

        public static void BothLose()
        {
            Console.Write("Печалька, перебор у обоих!");
        }
        public static void NoWinner()
        {
            Console.Write("Победила дружба!");
        }
    }
}

using BlackJack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Services
{
    public static class ConsoleServices
    {
        public static string AskName()
        {
            var entered = false;
            string name = string.Empty;

            while (!entered)
            {
                Console.Write("Enter your name: ");
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
                Console.WriteLine(name + ", to start press ENTER, to exit press ESC");
                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("The game started! Let the strongest win!");
                    started = true;
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Good Night Geek!");
                    break;
                }
            }
            return started;
        }

        public static void Warn(string message)
        {
            Console.WriteLine(message);
        }
        public static void CountMessage()
        {
            Console.WriteLine("Let's count!");
        }

        public static void CardsInDeck(int cardsAmount)
        {
            Console.WriteLine("Deck has " + cardsAmount + " cards");
        }

        public static void FirstDeal()
        {
            Console.WriteLine("Both players have received a card");
        }

        public static bool AskPlayerToTakeACard(string name)
        {
            var answer = string.Empty;

            while (!answer.Equals("yes") && !answer.Equals("no"))
            {
                Console.Write(name + ", card? (yes/no)");
                answer = Console.ReadLine();
            }

            return answer.Equals("yes");
        }

        public static void AskComputerToTakeACard(string name)
        {
                Console.WriteLine(name + ", card? (yes/no)");
        }

        public static void Congrat(string name)
        {
            Console.WriteLine("Congratulations!" + name + ", IS WINNER!");
        }

        public static void BothLose()
        {
            Console.WriteLine("Both Players Lose!");
        }
        public static void NoWinner()
        {
            Console.WriteLine("Drawn game!");
        }

        public static void Holder()
        {
            Console.ReadLine();
        }
    }
}

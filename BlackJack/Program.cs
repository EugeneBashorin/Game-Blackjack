using System;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("To start the game, press Enter");
            Console.ReadLine();
            ShowCardsValue();
            while (true)
            {                       
                Game g = new Game("Eugene", "Computer");

                Console.WriteLine("To exit, enter \"exit\":");
                string commands = Console.ReadLine();
                if (commands.ToLower() == "exit")
                {
                    return;
                }
                Console.ReadLine();
            }
        }

        public static void ShowCardsValue()
        {
            for (int i = 1; i <= 13; i++)
            {
                if (i % 5 == 0)
                {
                    Console.WriteLine((Values)i + "-" + i + "  ");
                }
                else
                {
                    Console.Write((Values)i + "-" + i+ "  ");
                }               
            }
            Console.WriteLine("\n");
        }
    }
}

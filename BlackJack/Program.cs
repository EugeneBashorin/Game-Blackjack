using BlackJack.Services;
using System;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameService = new GameService();

            gameService.Run();

            Console.ReadLine();
        }
    }
}

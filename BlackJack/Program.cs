using BlackJack.Services;
using System;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            GameService gameService = new GameService();
            gameService.Run();
        }
    }
}

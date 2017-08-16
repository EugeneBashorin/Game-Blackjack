using BlackJack.Entities;
using System;
using System.Linq;

namespace BlackJack.Services
{
    public class GameService
    {
        private readonly Game _game;

        private readonly DeckService _deckService;

        public const int MAX_VALUE = 21;
        public const int ACCEPTABLE_RISK = 15;

        public GameService()
        {
            _game = new Game
            {
                Player2 = new Player { Name = "Компьютер" }
            };

            _deckService = new DeckService(_game);
        }

        public void Run()
        {
            var name = string.Empty;
            bool nameIsValid = false;

            while (!nameIsValid)
            {
                name = Commands.AskName();

                try
                {
                    _game.Player1 = new Player { Name = name };
                    nameIsValid = true;
                }
                catch (Exception ex)
                {
                    Commands.Warn(ex.Message);
                }
            }

            bool start = Commands.AskStart(name);

            if (start)
            {
                _deckService.Initialize();

                int quantCardsToFirstDeal = 2;
                _deckService.Deal(_game.Player1, quantCardsToFirstDeal);
                _deckService.Deal(_game.Player2, quantCardsToFirstDeal);

                Commands.FirstDeal();

                bool finished = false;

                while (!finished)
                {
                    int quantCardsToDeal = 1;

                    string player1Cards = _deckService.Hand(_game.Player1);
                    string player2Cards = _deckService.Hand(_game.Player2);
                    int player1Points = _game.Player1.Cards.Sum(c => c.Points);
                    int player2Points = _game.Player2.Cards.Sum(c => c.Points);

                    Commands.Warn(GetPlayerInfo(_game.Player1));
                    Commands.Warn(GetPlayerInfo(_game.Player2));

                    bool takeFrstPlayer = Commands.AskPlayerToTakeACard(_game.Player1.Name);

                    if (player1Points <= MAX_VALUE)
                    {
                        if (takeFrstPlayer)
                        {
                            player1Points = _game.Player1.Cards.Sum(c => c.Points);
                            _deckService.Deal(_game.Player1, quantCardsToDeal);
                            player1Cards = _deckService.Hand(_game.Player1);
                            player1Points = _game.Player1.Cards.Sum(c => c.Points);
                        }
                    }

                    bool takeSecondPlayer = true;

                    if (player2Points <= MAX_VALUE || takeSecondPlayer || player2Points != ACCEPTABLE_RISK)
                    {
                        Commands.AskComputerToTakeACard(_game.Player2.Name);
                        if (player2Points <= ACCEPTABLE_RISK)
                        {
                            _deckService.Deal(_game.Player2, quantCardsToDeal);
                            player2Cards = _deckService.Hand(_game.Player2);
                            player2Points = _game.Player2.Cards.Sum(c => c.Points);
                        }
                        if (player2Points >= ACCEPTABLE_RISK)
                        {
                            takeSecondPlayer = false;
                        }

                    }
                    if (!takeFrstPlayer & !takeSecondPlayer || player1Points > MAX_VALUE || player2Points > MAX_VALUE)
                    {
                        Commands.Warn("Подсчитаем!");
                        Commands.Warn(GetPlayerInfo(_game.Player1));
                        Commands.Warn(GetPlayerInfo(_game.Player2));

                        GetWinner(player1Points, player2Points);
                        finished = true;
                    }
                }
            }
        }

        public string GetPlayerInfo(Player player)
        {
            string playerCards = _deckService.Hand(player);
            int playerPoints = player.Cards.Sum(c => c.Points);
            string res = player.Name + "_" + playerCards + "_" + playerPoints + "_" + "очков.";
            return res;
        }

        public void GetWinner(int player1Points, int player2Points)
        {
            if (player1Points <= MAX_VALUE & player2Points > MAX_VALUE)
            {
                Commands.Congrat(_game.Player1.Name);
            }
            if (player2Points <= MAX_VALUE & player1Points > MAX_VALUE)
            {
                Commands.Congrat(_game.Player2.Name);
            }
            if (player2Points > MAX_VALUE & player1Points > MAX_VALUE)
            {
                Commands.BothLose();
            }

            if (player1Points > player2Points & player1Points <= MAX_VALUE)
            {
                Commands.Congrat(_game.Player1.Name);
            }

            if (player1Points < player2Points & player2Points <= MAX_VALUE)
            {
                Commands.Congrat(_game.Player2.Name);
            }

            if (player1Points == player2Points & player1Points <= MAX_VALUE)
            {
                Commands.NoWinner();
            }
        }
    }
}

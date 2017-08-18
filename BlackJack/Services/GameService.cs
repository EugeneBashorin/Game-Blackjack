using BlackJack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.Services
{
    public class GameService
    {
        private Game _game;
        private Deck _deck;
        private DeckService _deckService;

        public const int MAX_VALUE = 21;
        public const int ACCEPTABLE_RISK = 15;
        public const int QUANTITY_CARDS_TO_DEAL = 1;
        public bool takeFirstPlayer = true;
        public bool takeSecondPlayer = true;
        public string name = string.Empty;

        public void StartGame()
        {
            _deck = new Deck();
            _deckService = new DeckService(_deck);
            _game = new Game
            {
                Player1 = new Player { Name = name, Cards = new List<Card>() },
                Player2 = new Player { Name = "Компьютер", Cards = new List<Card>() },
                Deck = new Deck() { Cards = new List<Card>() },
            };
            _game.Deck = _deck;
        }

        public void Run()
        {
            StartGame();
            SetPlayersName();
            bool start = ConsoleServices.AskStart(name);
            if (start)
            {
                _deckService.Initialize();
                int cardsInDeck = CountDecksCards(_game.Deck);
                ConsoleServices.CardsInDeck(cardsInDeck);
                _deckService.Deal(_game.Player1);
                _deckService.Deal(_game.Player2);
                ConsoleServices.FirstDeal();

                bool finished = false;

                while (!finished)
                {
                    string player1Cards = GetHand(_game.Player1);
                    string player2Cards = GetHand(_game.Player2);
                    int player1Points = GetPlayerScore(_game.Player1);
                    int player2Points = GetPlayerScore(_game.Player2);

                    cardsInDeck = CountDecksCards(_game.Deck);
                    ConsoleServices.CardsInDeck(cardsInDeck);
                    string infoPlayer1 = GetPlayerInfo(_game.Player1);
                    string infoPlayer2 = GetPlayerInfo(_game.Player2);

                    takeFirstPlayer = GetTheDealForPlayers(_game.Player1, takeFirstPlayer);
                    takeSecondPlayer = GetTheDealForComputer(_game.Player2, takeSecondPlayer);

                    if (player1Points > MAX_VALUE || player2Points > MAX_VALUE || !takeFirstPlayer & !takeSecondPlayer)
                    {
                        GetWinner(player1Points, player2Points);
                        bool newStart = ConsoleServices.AskStart(name);
                        StartNewGame(newStart);
                        finished = true;
                    }
                }
            }
            ConsoleServices.Holder();
        }

        public void SetPlayersName()
        {
            bool nameIsValid = false;
            while (!nameIsValid)
            {
                if (name == string.Empty)
                {
                    name = ConsoleServices.AskName();
                }
                if (name != string.Empty)
                {
                    try
                    {
                        _game.Player1.Name = name;
                        nameIsValid = true;
                    }
                    catch (Exception ex)
                    {
                        ConsoleServices.Warn(ex.Message);
                    }
                }
            }
        }

        public bool GetTheDealForPlayers(Player player, bool takeFirstPlayer)
        {
            int player1Points = GetPlayerScore(_game.Player1);
            string infoPlayer1 = GetPlayerInfo(_game.Player1);
            ConsoleServices.Warn(infoPlayer1);
            while (takeFirstPlayer)
            {
                player1Points = GetPlayerScore(player);
                if (player1Points > MAX_VALUE)
                {
                    takeFirstPlayer = false;
                }
                takeFirstPlayer = ConsoleServices.AskPlayerToTakeACard(_game.Player1.Name);
                if (player1Points <= MAX_VALUE & takeFirstPlayer)
                {
                    _deckService.Deal(_game.Player1);
                    player1Points = GetPlayerScore(_game.Player1);
                    infoPlayer1 = GetPlayerInfo(_game.Player1);
                    ConsoleServices.Warn(infoPlayer1);
                }
            }
            return takeFirstPlayer = false;
        }

        public bool GetTheDealForComputer(Player player, bool takeSecondPlayer)
        {
            int player2Points = GetPlayerScore(_game.Player2);
            string infoPlayer2 = GetPlayerInfo(_game.Player2);
            ConsoleServices.Warn(infoPlayer2);
            while (takeSecondPlayer)
            {
                ConsoleServices.AskComputerToTakeACard(_game.Player2.Name);
                player2Points = GetPlayerScore(player);
                if (player2Points <= MAX_VALUE & takeSecondPlayer || player2Points != ACCEPTABLE_RISK)
                {
                    if (player2Points <= ACCEPTABLE_RISK)
                    {
                        _deckService.Deal(_game.Player2);
                        infoPlayer2 = GetPlayerInfo(_game.Player2);
                        player2Points = GetPlayerScore(_game.Player2);
                        ConsoleServices.Warn(infoPlayer2);
                    }
                    if (player2Points >= ACCEPTABLE_RISK)
                    {
                        takeSecondPlayer = false;
                    }
                }
            }
            return takeSecondPlayer = false;
        }

        public string GetPlayerInfo(Player player)
        {
            string playerCards = GetHand(player);
            int playerPoints = GetPlayerScore(player);
            string res = player.Name + ": " + playerCards + " (" + playerPoints + " " + "points)";
            return res;
        }

        public string GetHand(Player player)
        {
            string handsCard = "";
            foreach (var cards in player.Cards)
            {
                handsCard += cards.Value.ToString() + "_" + cards.Suit.ToString() + " ";
            }
            return handsCard;
        }

        public int GetPlayerScore(Player player)
        {
            int playerScore = player.Cards.Sum(c => c.Point);
            return playerScore;
        }

        public int CountDecksCards(Deck deck)
        {
            int countDecksCards = deck.Cards.Count;
            return countDecksCards;
        }

        public void GetWinner(int player1Points, int player2Points)
        {
            ConsoleServices.CountMessage();
            ConsoleServices.Warn(GetPlayerInfo(_game.Player1));
            ConsoleServices.Warn(GetPlayerInfo(_game.Player2));

            if (player1Points <= MAX_VALUE & player2Points > MAX_VALUE)
            {
                ConsoleServices.Congrat(_game.Player1.Name);
            }
            if (player2Points <= MAX_VALUE & player1Points > MAX_VALUE)
            {
                ConsoleServices.Congrat(_game.Player2.Name);
            }
            if (player2Points > MAX_VALUE & player1Points > MAX_VALUE)
            {
                ConsoleServices.BothLose();
            }
            if (player1Points > player2Points & player1Points <= MAX_VALUE)
            {
                ConsoleServices.Congrat(_game.Player1.Name);
            }
            if (player1Points < player2Points & player2Points <= MAX_VALUE)
            {
                ConsoleServices.Congrat(_game.Player2.Name);
            }
            if (player1Points == player2Points & player1Points <= MAX_VALUE)
            {
                ConsoleServices.NoWinner();
            }
        }

        public void StartNewGame(bool ask)
        {
            if (ask)
            {
                takeFirstPlayer = true;
                takeSecondPlayer = true;
                Run();
            }
        }
    }
}

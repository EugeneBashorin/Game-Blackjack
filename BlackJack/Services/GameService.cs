using BlackJack.Configurations;
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
 
        public string name = string.Empty;

        public void StartGame()
        {
            _deck = new Deck();
            _deckService = new DeckService(_deck);
            _game = new Game();

            _game.Player1 = new Player();
            _game.Player1.Name = name;
            _game.Player1.Cards = new List<Card>();

            _game.Player2 = new Player();
            _game.Player2.Name = Configuration.COMPUTER_NAME;
            _game.Player2.Cards = new List<Card>();

            _game.Deck = new Deck();
            _game.Deck.Cards = new List<Card>();
            _game.Deck = _deck;
        }

        public void Run()
        {
            StartGame();
            SetPlayersName();
            bool start = ConsoleService.AskStart(name);
            if (start)
            {
                _deckService.Initialize();
                int cardsInDeck = CountDecksCards(_game.Deck);
                ConsoleService.CardsInDeck(cardsInDeck);
                _deckService.Deal(_game.Player1);
                _deckService.Deal(_game.Player2);
                ConsoleService.FirstDeal();

                bool finished = false;

                while (!finished)
                {
                    bool takeFirstPlayer = true;
                    bool takeSecondPlayer = true;

                    string player1Cards = ConsoleService.GetHand(_game.Player1);
                    string player2Cards = ConsoleService.GetHand(_game.Player2);
                    int player1Points = ConsoleService.GetPlayerScore(_game.Player1);
                    int player2Points = ConsoleService.GetPlayerScore(_game.Player2);

                    cardsInDeck = CountDecksCards(_game.Deck);
                    ConsoleService.CardsInDeck(cardsInDeck);
                    string infoPlayer1 = ConsoleService.GetPlayerInfo(_game.Player1);
                    string infoPlayer2 = ConsoleService.GetPlayerInfo(_game.Player2);

                    takeFirstPlayer = GetTheDealForPlayers(_game.Player1, takeFirstPlayer);
                    takeSecondPlayer = GetTheDealForComputer(_game.Player2, takeSecondPlayer);

                    if (player1Points > Configuration.MAX_VALUE || player2Points > Configuration.MAX_VALUE || !takeFirstPlayer & !takeSecondPlayer)
                    {
                        GetWinner(player1Points, player2Points);
                        finished = true;
                    }
                }
            }
            ConsoleService.Holder();
        }

        public void SetPlayersName()
        {
            bool nameIsValid = false;
            while (!nameIsValid)
            {
                if (name == string.Empty)
                {
                    name = ConsoleService.AskName();
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
                        ConsoleService.Warn(ex.Message);
                    }
                }
            }
        }

        public bool GetTheDealForPlayers(Player player, bool takeFirstPlayer)
        {
            int player1Points = ConsoleService.GetPlayerScore(_game.Player1);
            string infoPlayer1 = ConsoleService.GetPlayerInfo(_game.Player1);
            ConsoleService.Warn(infoPlayer1);
            while (takeFirstPlayer)
            {
                player1Points = ConsoleService.GetPlayerScore(player);
                if (player1Points > Configuration.MAX_VALUE)
                {
                    takeFirstPlayer = false;
                }
                takeFirstPlayer = ConsoleService.AskPlayerToTakeACard(_game.Player1.Name);
                if (player1Points <= Configuration.MAX_VALUE & takeFirstPlayer)
                {
                    _deckService.Deal(_game.Player1);
                    player1Points = ConsoleService.GetPlayerScore(_game.Player1);
                    infoPlayer1 = ConsoleService.GetPlayerInfo(_game.Player1);
                    ConsoleService.Warn(infoPlayer1);
                }
            }
            return takeFirstPlayer = false;
        }

        public bool GetTheDealForComputer(Player player, bool takeSecondPlayer)
        {
            int player2Points = ConsoleService.GetPlayerScore(_game.Player2);
            string infoPlayer2 = ConsoleService.GetPlayerInfo(_game.Player2);
            ConsoleService.Warn(infoPlayer2);
            while (takeSecondPlayer)
            {
                ConsoleService.AskComputerToTakeACard(_game.Player2.Name);
                player2Points = ConsoleService.GetPlayerScore(player);
                if (player2Points < Configuration.ACCEPTABLE_RISK & takeSecondPlayer)
                {
                    _deckService.Deal(_game.Player2);
                    infoPlayer2 = ConsoleService.GetPlayerInfo(_game.Player2);
                    player2Points = ConsoleService.GetPlayerScore(_game.Player2);
                    ConsoleService.Warn(infoPlayer2);
                    if (player2Points >= Configuration.ACCEPTABLE_RISK)
                    {
                        takeSecondPlayer = false;
                    }
                }
            }
            return takeSecondPlayer = false;
        }

        public int CountDecksCards(Deck deck)
        {
            int countDecksCards = deck.Cards.Count;
            return countDecksCards;
        }

        public void GetWinner(int player1Points, int player2Points)
        {
            ConsoleService.CountMessage();
            ConsoleService.Warn(ConsoleService.GetPlayerInfo(_game.Player1));
            ConsoleService.Warn(ConsoleService.GetPlayerInfo(_game.Player2));

            if (player1Points <= Configuration.MAX_VALUE & player2Points > Configuration.MAX_VALUE)
            {
                ConsoleService.Congrat(_game.Player1.Name);
            }
            if (player2Points <= Configuration.MAX_VALUE & player1Points > Configuration.MAX_VALUE)
            {
                ConsoleService.Congrat(_game.Player2.Name);
            }
            if (player2Points > Configuration.MAX_VALUE & player1Points > Configuration.MAX_VALUE)
            {
                ConsoleService.BothLose();
            }
            if (player1Points > player2Points & player1Points <= Configuration.MAX_VALUE)
            {
                ConsoleService.Congrat(_game.Player1.Name);
            }
            if (player1Points < player2Points & player2Points <= Configuration.MAX_VALUE)
            {
                ConsoleService.Congrat(_game.Player2.Name);
            }
            if (player1Points == player2Points & player1Points <= Configuration.MAX_VALUE)
            {
                ConsoleService.NoWinner();
            }
            StartNewGame();
        }

        public void StartNewGame()
        {
                Run();
        }
    }
}

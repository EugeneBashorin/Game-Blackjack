using System;
using System.Collections.Generic;

namespace BlackJack
{
    class Game
    {
        protected List<Player> players;
        private Deck stock;

        public Game(string playerName, string opponentName)
        {
            players = new List<Player>();                      //добавить и собрать все в Main
            players.Add(new PersonPlayer(playerName));        //Add players(name)
            players.Add(new ComputerPlayer(opponentName));
            foreach (var player in players)
            {
                if (player is IPlayerAble)
                {
                    Console.WriteLine("Enter your name");
                    string newName = Console.ReadLine();
                    ((IPlayerAble)player).ApplyName(newName);
                }
            }
            stock = new Deck();
            Deal();
            GetWinnerName();
        }

        private void Deal()
        {
            stock.ShuffleDeck();
            Console.WriteLine("Two cards were dealt");
            for (int i = 0; i < 2; i++)
            {
                foreach (Player player in players)
                {
                    player.TakeCard(stock.Deal()); //Берет верхнюю карту  
                }
            }
            foreach (Player player in players)
            {
                CountPoints(player);
            }
            Console.WriteLine(DescribePlayerHands());   //Карты на руках игроков
            foreach (var player in players)
            {
                if (player is IPlayerAble)
                {

                    DealPlayersCards(player);
                }
                else
                {
                    DealComputerCards(player);
                }
            }
        }

        public int CountPoints(Player player)
        {
            player.GetSumPoints();
            return player.Points;
        }

        public void DealPlayersCards(Player player)
        {
            bool choise = true;
            if (player.Points < 22)
            {
                while (choise)
                {
                    Console.WriteLine(player.Name + " has: " + player.Points + " points, " + player.ShowCardsOnHands());
                    Console.WriteLine("One more card? Press Y or N");
                    string key = Console.ReadLine();
                    switch (key.ToLower())
                    {
                        case "y":
                        case "н":
                            player.TakeCard(stock.Deal());
                            Console.WriteLine("Added " + player.ShowAddedCard());
                            CountPoints(player);
                            if (player.Points > 21)
                            {
                                Console.WriteLine("Too much!");
                                choise = false;
                            }
                            break;
                        case "n":
                        case "т":
                            Console.WriteLine("Exit");
                            choise = false;
                            break;
                        default:
                            Console.WriteLine("Exit");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine(player.Name + " - too much!");
                return;
            }
        }

        public void DealComputerCards(Player player)
        {
            bool choise = true;
            if (player.Points < 22)
            {
                while (choise)
                {
                    Console.WriteLine(player.Name + " has: " + player.Points + " points, " + player.ShowCardsOnHands());
                    if (player.Points <= 16)
                    {
                        Console.WriteLine(" - Give me a card!");
                        player.TakeCard(stock.Deal());
                        Console.WriteLine("Added " + player.ShowAddedCard());
                        CountPoints(player);
                        if (player.Points > 21)
                        {
                            Console.WriteLine("Too much!");
                            choise = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine(" - Enough!");
                        choise = false;
                    }
                }
            }
            else
            {
                Console.WriteLine(player.Name + " - too much!");
                return;
            }
        }

        public void GetWinnerName()
        {
            if (players[0].Points <= 21 & players[0].Points > players[1].Points)
                Console.WriteLine("Winner is: " + players[0].Name + " with " + players[0].ShowCardsOnHands() + " " + players[0].Points + " points");
            else if (players[0].Points <= 21 & players[1].Points > 21)
                Console.WriteLine("Winner is: " + players[0].Name + " with " + players[0].ShowCardsOnHands() + " " + players[0].Points + " points");
            else if (players[1].Points <= 21 & players[1].Points > players[0].Points)
                Console.WriteLine("Winner is: " + players[1].Name + " with " + players[1].ShowCardsOnHands() + " " + players[1].Points + " points");
            else if (players[1].Points <= 21 & players[0].Points > 21)
                Console.WriteLine("Winner is: " + players[1].Name + " with " + players[1].ShowCardsOnHands() + " " + players[1].Points + " points");
            else if (players[0].Points <= 21 & players[0].Points == players[1].Points)
                Console.WriteLine("dead heat");
            else
                Console.WriteLine("Both players are lose");
        }

        public string DescribePlayerHands()
        {
            string description = "\n";
            for (int i = 0; i < players.Count; i++)
            {
                description += players[i].Name + ": has " + players[i].CardCount + " cards(" + players[i].ShowCardsOnHands() + ") " + players[i].Points + " points.\n";
            }
            description += "The stock has " + stock.Count + " cards.\n";

            return description;
        }
    }
}

using BlackJack.Entities;
using BlackJack.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.Services
{
    /// <summary>
    /// Deck manage
    /// </summary>
    public class DeckService
    {
        private readonly Game _game;

        /// <summary>
        /// Deck manage
        /// </summary>
        /// <param name="game">context of game</param>
        public DeckService(Game game)
        {
            _game = game;
        }

        /// <summary>
        /// Create a new Deck
        /// </summary>
        public void Initialize(bool shuffle = true)
        {
            DeckCreator();
            if (shuffle)
            {
                Shuffle();
            }
        }

        public void DeckCreator()
        {
            _game.Deck.Cards = new List<Card>();
            foreach (var suit in Enum.GetNames(typeof(Suits)))
            {
                foreach (var value in Enum.GetNames(typeof(Values)))
                {
                    _game.Deck.Cards.Add(new Card
                    {
                        Suit = (Suits)Enum.Parse(typeof(Suits), suit),
                        Value = (Values)Enum.Parse(typeof(Values), value)
                    });
                }
            }
        }

        public void Shuffle()
        {
            var random = new Random();
            var shuffleDeck = new List<Card>();

            while (_game.Deck.Cards.Count > 0)
            {
                int cardToMove = random.Next(_game.Deck.Cards.Count);

                shuffleDeck.Add(_game.Deck.Cards[cardToMove]);
                _game.Deck.Cards.RemoveAt(cardToMove);
            }

            _game.Deck.Cards = shuffleDeck;
        }

        /// <summary>
        /// Add card on the top of deck
        /// </summary>
        /// <param name="card">Card wich we need to add to the deck</param>
        public void Put(Card card)
        {
            _game.Deck.Cards.Add(card);
        }

        /// <summary>
        /// Deal
        /// </summary>
        /// <param name="player">player</param>
        /// <param name="num">quantity of cards to deal</param>
        public void Deal(Player player, int num)
        {
            var cards = _game.Deck.Cards.Take(num);

            _game.Deck.Cards.RemoveRange(0, num);

            player.Cards.AddRange(cards);
        }

        /// <summary>
        /// Hands cards
        /// </summary>
        /// <param name="player">player</param>
        /// <returns>string with list of hand's cards</returns>
        public string Hand(Player player)
        {
            string handsCard = "";
            foreach (var cards in player.Cards)
            {
                handsCard += cards.ToString() + " ";
            }
            return handsCard;
        }
    }
}

using BlackJack.Entities;
using BlackJack.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.Services
{
    public class DeckService
    {
        private Deck _deck;
        public DeckService(Deck deck)
        {
            _deck = deck;
        }
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
            int MAX_SCORE = 10;
            int FIRST_VALUES_ELEMENT = 1;
            int LENGTH_OF_ENUM_VALUES = Enum.GetValues(typeof(Values)).Length;
            _deck.Cards = new List<Card>();

            foreach (var suit in Enum.GetNames(typeof(Suits)))
            {
                for (int i = FIRST_VALUES_ELEMENT; i <= LENGTH_OF_ENUM_VALUES; i++)
                {
                    _deck.Cards.Add(new Card
                    {
                        Suit = (Suits)Enum.Parse(typeof(Suits), suit),
                        Value = (Values)i,
                        Point = i <= MAX_SCORE ? i : MAX_SCORE
                    });
                }
            }
        }

        public void Shuffle()
        {
            var random = new Random();
            var shuffleDeck = new List<Card>();

            while (_deck.Cards.Count > 0)
            {
                int cardToMove = random.Next(_deck.Cards.Count);

                shuffleDeck.Add(_deck.Cards[cardToMove]);
                _deck.Cards.RemoveAt(cardToMove);
            }

            _deck.Cards = shuffleDeck;
        }

        public void Put(Card card)
        {
            _deck.Cards.Add(card);
        }

        public void Deal(Player player)
        {
            var cards = _deck.Cards.First();

            _deck.Cards.Remove(_deck.Cards.First());

            player.Cards.Add(cards);
        }
    }
}
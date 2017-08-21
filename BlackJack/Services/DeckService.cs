using BlackJack.Configurations;
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
        public void Initialize()
        {
            DeckCreator();
        }

        public void DeckCreator()
        {           
            int firstEnumsElementIndex = 0;

        _deck.Cards = new List<Card>();

            for (int k = firstEnumsElementIndex; k < Enum.GetValues(typeof(Suits)).Length; k++)
            {
                int minValuePoints = Configuration.MIN_VALUE_POINTS;

                for (int i = firstEnumsElementIndex; i < Enum.GetValues(typeof(Values)).Length; i++)
                {
                    if (i < Configuration.MIN_INDEX_PICTURES_CARD)
                    {
                        _deck.Cards.Add(new Card
                        {
                            Suit = (Suits)k,
                            Value = (Values)i,
                            Point = minValuePoints,
                        });
                        minValuePoints++;
                    }
                    if (i >= Configuration.MIN_INDEX_PICTURES_CARD)
                    {
                        _deck.Cards.Add(new Card
                        {
                            Suit = (Suits)k,
                            Value = (Values)i,
                            Point = (Values)i == (Values)Enum.Parse(typeof(Values), Values.Ace.ToString()) ? Configuration.ACE_VALUE : Configuration.PICTURES_CARDS_VALUE
                        });
                    }
                }
            }
            Shuffle();
        }

        public void Shuffle()
        {
            var random = new Random();
            var shuffleDeck = new List<Card>();

            while (_deck.Cards.Count > 0)
            {
                int cardToMove = random.Next(_deck.Cards.Count);

                shuffleDeck.Add(_deck.Cards.ElementAt(cardToMove));
                _deck.Cards.RemoveAt(cardToMove);
            }

            _deck.Cards = shuffleDeck;
        }

        public void Deal(Player player)
        {
            var cards = _deck.Cards.First();

            _deck.Cards.Remove(_deck.Cards.First());

            player.Cards.Add(cards);
        }
    }
}
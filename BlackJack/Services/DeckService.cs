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
            int lengthSuitsEnum = Enum.GetValues(typeof(Suits)).Length;
            int lengthValuesEnum = Enum.GetValues(typeof(Values)).Length;
            int minValuePoints;
            int minIndexPicturesCard = 9;

            _deck.Cards = new List<Card>();

            for (int k = Configuration.INDEX_OF_FIRST_ENUMS_ELEMENT; k < lengthSuitsEnum; k++)
            {
                minValuePoints = 2;

                for (int i = Configuration.INDEX_OF_FIRST_ENUMS_ELEMENT; i < lengthValuesEnum; i++)
                {
                    if (i < minIndexPicturesCard)
                    {
                        _deck.Cards.Add(new Card
                        {
                            Suit = (Suits)k,
                            Value = (Values)i,
                            Point = minValuePoints,
                        });
                        minValuePoints++;
                    }
                    if (i >= minIndexPicturesCard)
                    {
                        _deck.Cards.Add(new Card
                        {
                            Suit = (Suits)k,
                            Value = (Values)i,
                            Point = (Values)i == (Values)Enum.Parse(typeof(Values), "Ace") ? Configuration.ACE_VALUE : Configuration.PICTURES_CARDS_VALUE
                        });
                    }
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
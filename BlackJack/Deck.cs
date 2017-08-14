using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack
{
    class Deck
    {
        private List<Cards> cards;
        private Random random = new Random();

        public Deck()                                     //Create Deck with 52 cards
        {
            cards = new List<Cards>();
            for (int suit = 0; suit <= 3; suit++)
            {
                for (int value = 1; value <= 13; value++)
                {
                    cards.Add(new Cards((Suits)suit, (Values)value));
                }
            }
        }

        public Deck(IEnumerable<Cards> initialCards)  //Инициализация
        {
            cards = new List<Cards>(initialCards);
        }

        public int Count        // количество карт в колоде
        {                            
            get
            {
                return cards.Count();
            }
        }

        public void Add(Cards addedCard)              //добавление карт в коллекцию
        {
            cards.Add(addedCard);
        }

        public Cards Deal(int indexOfCard)            //Take a one card with index and remove
        {
            Cards CardToDeal = cards[indexOfCard];
            cards.RemoveAt(indexOfCard);
            return CardToDeal;
        }

        public Cards Deal()  //Take a one card with index 0 and remove Верхняя карта
        {
            return Deal(0);
        }

        public List<Cards> ShuffleDeck()
        {
            random = new Random();
            List<Cards> shuffleDeck = new List<Cards>();
            while (cards.Count > 0)
            {
                int CardToMove = random.Next(cards.Count);
                shuffleDeck.Add(cards[CardToMove]);
                cards.RemoveAt(CardToMove);
            }
            cards = shuffleDeck;
            return cards;
        }

        public Cards Peek(int cardNumber)               //Get info about a card by Index
        {
            return cards[cardNumber];
        }
    }
}

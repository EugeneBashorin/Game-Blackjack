namespace BlackJack
{
    abstract class Player
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int points;
        public int Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
            }
        }

        private Deck cards;

        public Player(string name)
        {
            this.name = name;
            Points = points;
            this.cards = new Deck(new Cards[] { });
        }

        public int GetSumPoints()
        {
            Points = 0;
            for (int card = 0; card < cards.Count; card++)
            {
                Points += (int)cards.Peek(card).Values;
            }
            return Points;
        }

        public string ShowAddedCard()
        {
            string coverCard = cards.Peek(cards.Count-1).CardName;

            return coverCard;
        }

        public string ShowCardsOnHands()
        {
            string cardsList = "";

            for (int card = 0; card < cards.Count; card++)
            {
                cardsList += cards.Peek(card).CardName + " ";

            }
            return cardsList;
        }
        
        public int CardCount //считает кол-во карт в колоде игрока
        {
            get { return cards.Count; }
        }

        public void TakeCard(Cards card) // берет карту из колоды 
        {
            cards.Add(card);
        }
    }
}
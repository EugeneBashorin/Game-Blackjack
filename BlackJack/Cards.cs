namespace BlackJack
{
    class Cards
    {
        public Suits Suits { get; set; }
        public Values Values { get; set; }

        public Cards(Suits suit, Values salue)
        {
            Suits = suit;
            Values = salue;
        }

        public string CardName
        {
            get
            {
                return Values.ToString() + "_" + Suits.ToString();
            }
        }
    }
}

using BlackJack.Enums;

namespace BlackJack.Entities
{
    public class Card
    {
        public Suits Suit { get; set; }

        public Values Value { get; set; }

        public int Point { get; set; }      
    }
}

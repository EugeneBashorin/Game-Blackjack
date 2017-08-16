using System.Collections.Generic;

namespace BlackJack.Entities
{
     public class Deck
    {
        /// <summary>
        /// Collection of cardS in Deck
        /// </summary>
        public List<Card> Cards { get; set; } = new List<Card>();

        /// <summary>
        /// Discription of deck
        /// </summary>
        /// <returns>Discription of deck</returns>
        public override string ToString()
        {
            return "В колоде " + Cards.Count + " карт";
        }
    }
}

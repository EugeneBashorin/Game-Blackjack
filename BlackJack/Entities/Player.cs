using System.Collections.Generic;

namespace BlackJack.Entities
{
    public class Player
    {
        public string Name { get; set; }

        public List<Card> Cards { get; set; }
    }
}

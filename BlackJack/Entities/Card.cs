using BlackJack.Enums;
using System;

namespace BlackJack.Entities
{
    public class Card
    {
        public Suits Suit { get; set; }

        public Values Value { get; set; }

        public int Points
        {
            get
            {
                switch (Value)
                {
                    case Values.Ace:
                        return 1;
                    case Values.Two:
                        return 2;
                    case Values.Three:
                        return 3;
                    case Values.Four:
                        return 4;
                    case Values.Five:
                        return 5;
                    case Values.Six:
                        return 6;
                    case Values.Seven:
                        return 7;
                    case Values.Eight:
                        return 8;
                    case Values.Nine:
                        return 9;
                    case Values.Ten:
                    case Values.Jack:
                    case Values.Queen:
                    case Values.King:
                        return 10;
                    default:
                        throw new ArgumentException("Масть не определена", nameof(Value));
                }
            }
        }

        /// <summary>
        /// Return discription of card
        /// </summary>
        /// <returns>discription of card</returns>
        public override string ToString()
        {
            return Value + "_" + Suit;
        }
    }
}

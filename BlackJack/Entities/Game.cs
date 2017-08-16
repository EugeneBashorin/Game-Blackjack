using System;

namespace BlackJack.Entities
{
    /// <summary>
    /// Context of game
    /// </summary>
    public class Game
    {
        Player _player1;

        Player _player2;

        /// <summary>
        /// Deck of cards
        /// </summary>
        public Deck Deck { get; set; } = new Deck();

         public Player Player1
        {
            get { return _player1; }
            set
            {
                if (value.Name.Equals(Player2?.Name))
                {
                    throw new ArgumentException("Игрок с имененем " + value.Name + " уже существует");
                }

                _player1 = value;
            }
        }

         public Player Player2
        {
            get { return _player2; }
            set
            {
                if (value.Name.Equals(Player1?.Name))
                {
                    throw new ArgumentException("Игрок с имененем " + value.Name + " уже существует");
                }

                _player2 = value;
            }
        }
    }
}

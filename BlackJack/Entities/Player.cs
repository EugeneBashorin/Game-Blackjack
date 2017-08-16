using System.Collections.Generic;

namespace BlackJack.Entities
{
    /// <summary>
    /// Игрок
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Имя игрока
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Карты на руках
        /// </summary>
        public List<Card> Cards { get; set; } = new List<Card>();

        /// <summary>
        /// Возвращает описание игрока
        /// </summary>
        /// <returns>Описание игрока</returns>
        public override string ToString()
        {
           return "У " + Name + " : " + Cards.Count + " карт";
        }
    }
}

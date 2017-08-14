namespace BlackJack
{
    class ComputerPlayer : Player
    {
        private Deck cards;
        public ComputerPlayer(string name) : base(name)
        {
            Name = name;
            cards = new Deck(new Cards[] { });
        }
    }
}

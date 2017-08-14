namespace BlackJack
{
    class PersonPlayer : Player, IPlayerAble
    {
        private Deck cards;
        public PersonPlayer(string name) : base(name)
        {
            Name = name;
            cards = new Deck(new Cards[] { });
        }

        public string ApplyName(string newName)
        {
            return Name = newName;
        }

    }
}

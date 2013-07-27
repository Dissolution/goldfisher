using System;
using System.Collections.Generic;
using System.Linq;
using Goldfisher.Cards;

namespace Goldfisher
{
	public class BoardState
	{
		private List<string> _log;
	    private Random _random;
        
        public int Seed { get; set; }

		public List<Card> Library { get; set; } 
		public List<Card> Hand { get; set; }
		public List<Card> Graveyard { get; set; }
		public List<Card> Exile { get; set; }
		public List<Card> Battlefield { get; set; }

		public int Storm { get; set; }

		public Manapool Manapool { get; set; }
		public int LedMana { get; set; }

		public WinConditionType GoalType { get; set; }
		public WinConditionType WinConditionType { get; set; }

        #region Constructors
        public BoardState(List<Card> library)
		{
			this.Library = library.Copy();
            this.Seed = unchecked((int) DateTime.Now.Ticks);

            _random = new Random();
            Reset();
		}

        public BoardState(List<Card> library, int seed)
        {
            this.Library = library.Copy();
            this.Seed = seed;

            _random = new Random();
            Reset();
        }
        #endregion

        #region Private Methods
        private void Reset()
        {
            Hand = new List<Card>();
            Graveyard = new List<Card>();
            Exile = new List<Card>();
            Battlefield = new List<Card>();
            _log = new List<string>();
            Manapool = new Manapool();
            LedMana = 0;
            Storm = 0;

            GoalType = WinConditionType.None;
            WinConditionType = WinConditionType.None;
        }
        #endregion

        #region Public Methods
        public void Log(Usage usage, Card card, string effect)
		{
			_log.Add("({0}): {1} {2} - {3}".FormatWith(Manapool, usage, card.Name, effect));
		}
		public void Log(Usage usage, Card card)
		{
			_log.Add("({0}): {1} {2}".FormatWith(Manapool, usage, card.Name));
		}
		public void Log(string text)
		{
			_log.Add(text);
		}

		public List<Card> DrawCards(int number)
		{
			if (number < 0)
				throw new ArgumentOutOfRangeException("number");

			var cards = Library.Take(number).ToList().Copy();
			Library.RemoveRange(0, number);
			Hand.AddRange(cards);
			return cards;
		}

        public void Shuffle()
        {
            //Fisher-Yates Shuffle
            var count = Library.Count - 1;
            for (var x = count; x > 1; x--)
            {
                var y = _random.Next(x + 1);
                var value = Library[y];
                Library[y] = Library[x];
                Library[x] = value;
            }
        }

		public void Mulligan()
		{
			var cards = Hand.Count;
		    Library.AddRange(Hand);
			Hand.Clear();
			Shuffle();
			DrawCards(cards - 1);
			Log("Mulliganned to {0}".FormatWith(cards-1));
		}

        public BoardState Copy()
        {
            var state = new BoardState(Library, Seed)
                {
                    Hand = Hand.Copy(),
                    Graveyard = Graveyard.Copy(),
                    Exile = Exile.Copy(),
                    Battlefield = Battlefield.Copy(),
                    Storm = Storm,
                    Manapool = Manapool.Copy(),
                    LedMana = LedMana,
                    GoalType = GoalType,
                    WinConditionType = WinConditionType
                };
            return state;
        }
        #endregion
    }
}

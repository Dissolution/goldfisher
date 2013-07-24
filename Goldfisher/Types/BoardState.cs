using System;
using System.Collections.Generic;
using System.Linq;
using Jay;

namespace Goldfisher
{
	public class BoardState
	{
		private readonly List<string> _log; 

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

		public BoardState()
		{
			Library = new List<Card>();
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

		public BoardState(List<Card> library)
			: this()
		{
			this.Library = library.Copy();
		}

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

		public void Mulligan()
		{
			var cards = Hand.Count;
			Library.AddRange(Hand.Copy());
			Hand.Clear();
			Library.Randomize();
			DrawCards(cards - 1);
			Log("Mulliganned to {0}".FormatWith(cards-1));
		}
	}
}

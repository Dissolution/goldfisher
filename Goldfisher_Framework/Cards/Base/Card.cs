using System;

namespace Goldfisher.Cards
{
	public abstract class Card : IEquatable<Card>
	{
		#region Properties
        /// <summary>
        /// Card's Name
        /// </summary>
		public string Name { get; protected set; }

        /// <summary>
        /// Short/Abbreviated Name
        /// </summary>
        public string ShortName { get; protected set; }

        /// <summary>
        /// Card's Role
        /// </summary>
		public CardType Type { get; protected set; }

        /// <summary>
        /// Card's color(s)
        /// </summary>
		public Color Color { get; protected set; }

        /// <summary>
        /// Card's mana cost
        /// </summary>
		public Manacost Cost { get; protected set; }

        /// <summary>
        /// Card's cast priority (lower is earlier)
        /// </summary>
		public decimal Priority { get; protected set; }
		#endregion

		#region Constructors
		protected Card()
		{
            //No defaults, each card must specify.
		}
		#endregion

		#region Abstract Methods
		public abstract bool CanCast(BoardState boardState);
		public abstract bool Resolve(BoardState boardState);
		#endregion

		#region Public Methods
		public bool Equals(Card other)
		{
			if (ReferenceEquals(this, other))
				return true;
			if (ReferenceEquals(other, null))
				return false;

			return Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
		}
		#endregion

		#region Overrides
		public override bool Equals(object obj)
		{
			return Equals(obj as Card);
		}
		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}
		public override string ToString()
		{
			return Name;
		}
		#endregion
	}
}

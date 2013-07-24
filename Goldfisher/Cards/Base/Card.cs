using System;

namespace Goldfisher
{
	public abstract class Card : IEquatable<Card>
	{
		#region Properties
		public string Name { get; protected set; }
		public CardType Type { get; protected set; }
		public Color Color { get; protected set; }
		public Manacost Cost { get; protected set; }
		public decimal Priority { get; protected set; }

		public int StartMana { get; protected set; }
		public int EndMana { get; protected set; }
		public int AddMana
		{
			get { return EndMana - StartMana; }
		}
		#endregion

		#region Constructors
		protected Card()
		{
			Name = string.Empty;
			Type = CardType.None;
			Color = Color.None;
			Cost = Manacost.None;
			Priority = 10.0m;	//Last
			StartMana = 0;
			EndMana = 0;
		}
		#endregion

		#region Abstract Methods
		public abstract bool CanCast(BoardState boardState);
		public abstract void Resolve(BoardState boardState);
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

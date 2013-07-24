using System.Collections.Generic;
using System.Text;
using Jay;

namespace Goldfisher
{
	public class Manacost
	{
		#region Fields
		protected readonly Dictionary<Color, int> _mana = new Dictionary<Color, int>();
		#endregion

		#region Properties
		public int Colorless
		{
			get { return _mana[Color.None]; }
		}

		public int White
		{
			get { return _mana[Color.White]; }
		}
		public int Blue
		{
			get { return _mana[Color.Blue]; }
		}
		public int Black
		{
			get { return _mana[Color.Black]; }
		}
		public int Red
		{
			get { return _mana[Color.Red]; }
		}
		public int Green
		{
			get { return _mana[Color.Green]; }
		}

		public virtual int Total
		{
			get { return Colorless + White + Blue + Black + Red + Green; }
		}

		public int this[Color color]
		{
			get { return _mana[color]; }
		}
		#endregion

		#region Static Properties
		public static Manacost None
		{
			get { return new Manacost();}
		}
		#endregion

		#region Constructors
		public Manacost()
		{
			foreach (var color in EnumExtensions.GetValues<Color>())
			{
				_mana.Add(color, 0);
			}
		}

		public Manacost(string castingCost)
			: this()
		{
			castingCost = castingCost.ToUpper();
			var numbers = castingCost.Numbers();
			if (!numbers.IsJunk())
				_mana[Color.None] = numbers.ToInt();
			_mana[Color.White] = castingCost.CountChar('W');
			_mana[Color.Blue] = castingCost.CountChar('U');
			_mana[Color.Black] = castingCost.CountChar('B');
			_mana[Color.Red] = castingCost.CountChar('R');
			_mana[Color.Green] = castingCost.CountChar('G');
		}
		#endregion

		#region Overrides
		public override string ToString()
		{
			var text = new StringBuilder();
			if (Colorless > 0)
				text.Append(Colorless);
			text.Append('W', White);
			text.Append('U', Blue);
			text.Append('B', Black);
			text.Append('R', Red);
			text.Append('G', Green);

			return text.ToString();
		}
		#endregion
	}
}

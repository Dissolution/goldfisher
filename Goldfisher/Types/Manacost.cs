using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

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
			foreach (var color in Enum.GetValues(typeof(Color)))
			{
				_mana.Add((Color)color, 0);
			}
		}

		public Manacost(string castingCost)
			: this()
		{
			castingCost = castingCost.ToUpper();
		    var numbers = new string(castingCost.Where(char.IsNumber).ToArray());
		    if (!string.IsNullOrWhiteSpace(numbers))
		        _mana[Color.None] = Convert.ToInt32(numbers);
			_mana[Color.White] = castingCost.Count(c => c == 'W');
            _mana[Color.Blue] = castingCost.Count(c => c == 'U');
            _mana[Color.Black] = castingCost.Count(c => c == 'B');
            _mana[Color.Red] = castingCost.Count(c => c == 'R');
            _mana[Color.Green] = castingCost.Count(c => c == 'G');
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

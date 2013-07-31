using System.Linq;

namespace Goldfisher.Cards
{
	public class DesperateRitual : ManaSource
	{
		public DesperateRitual()
		{
		    Name = "Desperate Ritual";
            ShortName = "Desperate";
            Type = CardType.Ramp;
            Color = Color.Red;
            Cost = new Manacost("1R");
            Produces = new Manapool("RRR");

		    Priority = 2.5m;
		}

        public override bool CanCast(BoardState boardState)
        {
            return boardState.Manapool.CanPay(Cost);
        }

        public override bool Resolve(BoardState boardState)
        {
            //Check if we have another Desperate Ritual and 2RR in pool
            if (boardState.Hand.Count(c => c.Name == Name) >= 2 &&
                boardState.Manapool.CanPay(new Manacost("2RR")))
            {
                //Splice the other onto this!

                //Pay costs, put on stack
                boardState.Manapool.Pay(new Manacost("2RR"));
                boardState.Hand.Remove(this);       //Leave the other

                //Resolve
                boardState.Manapool.Add(new Manapool("RRRRRR"));
                boardState.Storm += 1;
                boardState.Graveyard.Add(this);

                //Log
                boardState.Log(Usage.Cast, this, "Spliced");
            }
            else
            {
                //Pay costs, put on stack.
                boardState.Manapool.Pay(Cost);
                boardState.Hand.Remove(this);

                //Resolve
                boardState.Manapool.Add(Produces);
                boardState.Storm += 1;
                boardState.Graveyard.Add(this);

                //Log
                boardState.Log(Usage.Cast, this);
            }
			return true;
        }
	}
}

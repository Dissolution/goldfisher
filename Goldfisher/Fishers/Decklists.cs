using System;
using System.Collections.Generic;
using Goldfisher.Cards;

namespace Goldfisher
{
	public static class Decklists
	{
		public static List<Card> AStormBrewing()
		{
			var deck = new List<Card>();
			for (var i = 0; i < 4; i++)
			{
				if (i == 0)		//1-ofs
				{
					deck.Add(new Taiga());
					}
				
				
				if (i <= 1)		//2-ofs
				{
                    deck.Add(new GrimMonolith());
				}

				if (i <= 2)		//3-ofs
				{
					deck.Add(new ChromeMox());
                    deck.Add(new BurningWish());
					deck.Add(new EmptyTheWarrens());
				}

				//All others are 4-ofs
				deck.Add(new GitaxianProbe());

				deck.Add(new LotusPetal());
				deck.Add(new SimianSpiritGuide());
				deck.Add(new ElvishSpiritGuide());
				deck.Add(new LandGrant());
				
				//deck.Add(new Manamorphose());
				deck.Add(new DesperateRitual());
				deck.Add(new RiteOfFlame());
				deck.Add(new PyreticRitual());
				deck.Add(new SeethingSong());
				deck.Add(new TinderWall());

				deck.Add(new LionsEyeDiamond());
				deck.Add(new GoblinCharbelcher());
				
			}

			if (deck.Count != 60)
				throw new Exception("DeckSize");

			return deck;
		}

		public static List<Card> Dissolution()
		{
			var deck = new List<Card>();
			for (var i = 0; i < 4; i++)
			{
				if (i == 0)		//1-ofs
				{
					deck.Add(new Taiga());
				}


				if (i <= 1)		//2-ofs
				{
				}

				if (i <= 2)		//3-ofs
				{
					deck.Add(new EmptyTheWarrens());
				}

				//All others are 4-ofs
				deck.Add(new BurningWish());
				deck.Add(new ChromeMox());

				deck.Add(new GitaxianProbe());
				//deck.Add(new StreetWraith());
				//deck.Add(new GrimMonolith());

				deck.Add(new LotusPetal());
				deck.Add(new SimianSpiritGuide());
				deck.Add(new ElvishSpiritGuide());
				deck.Add(new LandGrant());

				deck.Add(new Manamorphose());
				deck.Add(new DesperateRitual());
				deck.Add(new RiteOfFlame());
				//deck.Add(new PyreticRitual());
				deck.Add(new SeethingSong());
				deck.Add(new TinderWall());

				deck.Add(new LionsEyeDiamond());
				deck.Add(new GoblinCharbelcher());

			}

			if (deck.Count != 60)
				throw new Exception("DeckSize");

			return deck;
		}

        public static List<Card> BestFound()
        {
            var deck = new List<Card>();
            for (var i = 0; i < 4; i++)
            {
                if (i == 0)		//1-ofs
                {
                    deck.Add(new Taiga());
                }


                if (i <= 1)		//2-ofs
                {
                    
                    
                }

                if (i <= 2)		//3-ofs
                {
                    
                    
                    deck.Add(new EmptyTheWarrens());
                }

                //All others are 4-ofs
                //deck.Add(new StreetWraith());
               // deck.Add(new GitaxianProbe());
                deck.Add(new GrimMonolith());
                deck.Add(new ChromeMox());
                deck.Add(new BurningWish());
                
                deck.Add(new LotusPetal());
                deck.Add(new SimianSpiritGuide());
                deck.Add(new ElvishSpiritGuide());
                deck.Add(new LandGrant());

                //deck.Add(new Manamorphose());
                deck.Add(new DesperateRitual());
                deck.Add(new RiteOfFlame());
                deck.Add(new PyreticRitual());
                deck.Add(new SeethingSong());
                deck.Add(new TinderWall());

                deck.Add(new LionsEyeDiamond());
                deck.Add(new GoblinCharbelcher());

            }

            if (deck.Count != 60)
                throw new Exception("DeckSize");

            return deck;
        }
	}
}

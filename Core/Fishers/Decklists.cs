using Jay.Goldfisher.Cards.ManaSources.Initial;
using Jay.Goldfisher.Cards.ManaSources.Ramp;
using Jay.Goldfisher.Cards.Setup;
using Jay.Goldfisher.Cards.WinCons;

using System.Diagnostics;

namespace Jay.Goldfisher.Fishers;

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
        var deck = new List<Card>(60);

        // 1-ofs
        var ones = new Type[] { typeof(Taiga) };
        foreach (var one in ones)
        {
            deck.Add((Activator.CreateInstance(one) as Card)!);
        }
        // 2-ofs
        var twos = new Type[] { };
        foreach (var two in twos)
        {
            deck.Add((Activator.CreateInstance(two) as Card)!);
            deck.Add((Activator.CreateInstance(two) as Card)!);
        }
        // 3-ofs
        var threes = new Type[] { typeof(EmptyTheWarrens) };
        foreach (var three in threes)
        {
            deck.Add((Activator.CreateInstance(three) as Card)!);
            deck.Add((Activator.CreateInstance(three) as Card)!);
            deck.Add((Activator.CreateInstance(three) as Card)!);
        }
        // 4-ofs
        var fours = new Type[]
        {
            typeof(BurningWish),
            typeof(ChromeMox),
            typeof(GitaxianProbe),
            typeof(LotusPetal),
            typeof(SimianSpiritGuide),
            typeof(ElvishSpiritGuide),
            typeof(LandGrant),
            typeof(Manamorphose),
            typeof(DesperateRitual),
            typeof(RiteOfFlame),
            typeof(SeethingSong),
            typeof(TinderWall),
            typeof(LionsEyeDiamond),
            typeof(GoblinCharbelcher),
        };
        foreach (var four in fours)
        {
            deck.Add((Activator.CreateInstance(four) as Card)!);
            deck.Add((Activator.CreateInstance(four) as Card)!);
            deck.Add((Activator.CreateInstance(four) as Card)!);
            deck.Add((Activator.CreateInstance(four) as Card)!);
        }

        Debug.Assert(deck.Count == 60);

        return deck;
    }


    public static List<Card> BestFound()
    {
        var deck = new List<Card>();
        for (var i = 0; i < 4; i++)
        {
            if (i == 0)     //1-ofs
            {
                deck.Add(new Taiga());
            }


            if (i <= 1)     //2-ofs
            {


            }

            if (i <= 2)     //3-ofs
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
﻿namespace Jay.Goldfisher.Cards.ManaSources.Ramp;

public class RiteOfFlame : ManaSource
{
    public RiteOfFlame()
    {
        Name = "Rite of Flame";
        ShortName = "Rite";
        Type = CardRole.Ramp;
        Color = Color.Red;
        Cost = new ManaValue("R");
        Produces = new ManaPool("RR");      //Could be more

        Priority = 2.2m;
    }


    public override bool CanCast(BoardState boardState)
    {
        return boardState.Manapool.CanPay(Cost);
    }

    public override bool Resolve(BoardState boardState)
    {
        //Pay costs, put on stack.
        boardState.Manapool.Pay(Cost);
        boardState.Hand.Remove(this);

        //Resolve
        boardState.Manapool.Add(Produces);
        //Add extra red for other copies in graveyard
        boardState.Manapool.Add(
            boardState.Graveyard.Count(c => c.Name == Name), 
            Color.Red);
        boardState.Storm += 1;
        boardState.Graveyard.Add(this);

        //Log
        boardState.Log(Usage.Cast, this);
        return true;
    }
}
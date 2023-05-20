namespace Goldfisher.Cards
{
    public abstract class ManaSource : Card
    {
        #region Properties
        public Manapool Produces { get; protected set; }
        #endregion

        #region Constructors
        protected ManaSource()
        {
            //Produces nothing
            this.Produces = Manapool.Empty;
        }
        #endregion
    }
}

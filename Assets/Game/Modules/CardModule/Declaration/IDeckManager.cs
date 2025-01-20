using ObservableCollections;

namespace Game.Modules.CardModule.Declaration
{
    public interface IDeckManager
    {
        public ObservableList<CardEffect> DrawPile { get; }
        public ObservableList<CardEffect> DiscardPile { get; }
    }
}
namespace Model.Cards
{
    public interface ICardPlaceChangedHandler
    {
        void OnCardPlaceChanged(Card newPlace);
    }
}
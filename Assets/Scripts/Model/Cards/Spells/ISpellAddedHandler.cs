using UnityEngine.EventSystems;

namespace Model.Cards.Spells
{
    public interface ISpellAddedHandler : IEventSystemHandler
    {
        void OnSpellAdded(Spell spell);
    }
}
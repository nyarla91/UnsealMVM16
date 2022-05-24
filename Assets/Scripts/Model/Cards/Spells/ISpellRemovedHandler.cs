using UnityEngine.EventSystems;

namespace Model.Cards.Spells
{
    public interface ISpellRemovedHandler : IEventSystemHandler
    {
        void OnSpellRemoved();
    }
}
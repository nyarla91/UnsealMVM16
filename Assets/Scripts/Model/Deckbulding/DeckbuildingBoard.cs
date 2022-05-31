using UnityEngine;

namespace Model.Deckbulding
{
    public class DeckbuildingBoard : MonoBehaviour
    {
        [SerializeField] private BuidledDeck _buidledDeck;
        [SerializeField] private Libary _libary;

        public BuidledDeck BuidledDeck => _buidledDeck;
        public Libary Libary => _libary;
    }
}
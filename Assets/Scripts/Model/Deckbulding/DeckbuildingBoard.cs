using UnityEngine;

namespace Model.Deckbulding
{
    public class DeckbuildingBoard : MonoBehaviour
    {
        [SerializeField] private BuildedDeck buildedDeck;
        [SerializeField] private Libary _libary;

        public BuildedDeck BuildedDeck => buildedDeck;
        public Libary Libary => _libary;
    }
}
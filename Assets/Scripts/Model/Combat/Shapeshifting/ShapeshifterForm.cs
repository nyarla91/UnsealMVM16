using Model.Combat.GameAreas;
using UnityEngine;
using Zenject;

namespace Model.Combat.Shapeshifting
{
    public abstract class ShapeshifterForm : MonoBehaviour
    {
        [Inject]
        protected GameBoard GameBoard { get; private set; }
        
        public abstract void OnEnter();
        public abstract void OnExit();
    }
}
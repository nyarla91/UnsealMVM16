using UnityEngine;
using View.Combat.Characters;

namespace View.Combat
{
    public class FormView : Token
    {
        protected override bool Move => false;

        private void Start()
        {
            Descend();
        }
    }
}
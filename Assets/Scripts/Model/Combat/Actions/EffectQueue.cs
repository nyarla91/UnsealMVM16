using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Combat.Actions
{
    public class EffectQueue : MonoBehaviour
    {
        private List<Effect> _effects = new List<Effect>();

        private float _delayLeft;

        public bool EffectInProgress => _delayLeft <= 0;
        
        public void AddEffect(Effect effectToAdd, bool beforeOther)
        {
            if (beforeOther)
                _effects.Insert(0, effectToAdd);
            else
                _effects.Add(effectToAdd);
        }

        private void FixedUpdate()
        {
            if (_delayLeft >= 0)
                _delayLeft -= Time.fixedDeltaTime;
            else if (_effects.Count > 0)
            {
                _effects[0].Execute();
                _delayLeft = _effects[0].DelayAfter;
                _effects.RemoveAt(0);
            }
        }
    }
}
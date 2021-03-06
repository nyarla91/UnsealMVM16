using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Combat.GameAreas;
using UnityEngine;
using Zenject;

namespace Model.Combat.Effects
{
    public class EffectQueue : MonoBehaviour
    {
        [SerializeField] private GameBoard _gameBoard;
        [SerializeField] private AudioSource _audioSource;

        [Inject] private Pause _pause;
        
        private List<Effect> _effects = new List<Effect>();
        private float _delayLeft;

        public bool EffectInProgress => _delayLeft > 0 || _effects.Count > 0;
        
        public void Delay(float delay)
        {
            if (delay > _delayLeft)
                _delayLeft = delay;
        }

        public void AddEffect(Effect effectToAdd, int index)
        {
            Delay(0.1f);
            _effects.Insert(index, effectToAdd);
        }
        
        public void AddEffect(Effect effectToAdd)
        {
            Delay(0.1f);
            _effects.Add(effectToAdd);
        }

        public async Task WaitForEffects()
        {
            while (EffectInProgress)
            {
                await Task.Delay(50);
            }
        }

        private void FixedUpdate()
        {
            if (_delayLeft >= 0  && !_pause.IsPaused)
                _delayLeft -= Time.fixedDeltaTime;
            else if (_effects.Count > 0 && !_gameBoard.TargetChooser.ChooseActive)
            {
                Effect currentEffect = _effects[0];
                _effects.RemoveAt(0);
                currentEffect.Execute();
                if (currentEffect.Sounds != null && currentEffect.Sounds.Length > 0)
                {
                    _audioSource.clip = SoundRandomizer.LoadAudio(currentEffect.Sounds);
                    _audioSource.Play();
                }
                _delayLeft = currentEffect.DelayAfter;
            }
        }
    }
}
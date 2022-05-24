using System;
using System.Collections;
using UnityEngine;

namespace Essentials
{
    public class Timer
    {

        private MonoBehaviour _container;
        private float _timeElapsed;
        private bool _active;
        private Coroutine _tickingCoroutine;
        private float _length;

        public float Length
        {
            get => _length;
            set => _length = Mathf.Max(value, 0);
        }

        public bool Loop { get; set; }
        public bool FixedTime { get; set; } = true;

        public float TimeElapsed => _timeElapsed;
        public float TimeLeft => Length - TimeElapsed;

        public event Action OnStarted;
        public event Action<float> OnTick;
        public event Action OnExpired;

        public bool IsExpired => _tickingCoroutine == null;

        public Timer(MonoBehaviour container, float length, bool loop, bool fixedTime)
        {
            _container = container;
            Length = length;
            Loop = loop;
            FixedTime = fixedTime;
            Init();
        }

        public Timer(MonoBehaviour container, float length, bool loop)
        {
            _container = container;
            Length = length;
            Loop = loop;
            Init();
        }
        
        public Timer(MonoBehaviour container, float length)
        {
            _container = container;
            Length = length;
            Init();
        }

        public Timer(MonoBehaviour container)
        {
            _container = container;
            Init();
        }

        public void Start()
        {
            if (Length == 0)
                Debug.LogWarning("Timer length is 0");
            _tickingCoroutine = _container.StartCoroutine(Ticking());
        }

        public void Restart()
        {
            Stop();
            Start();
        }

        public void Reset()
        {
            Stop();
            _timeElapsed = 0;
        }

        public void Stop()
        {
            if (_tickingCoroutine == null)
                return;
            
            _container.StopCoroutine(_tickingCoroutine);
            _tickingCoroutine = null;
        }

        private void Init()
        {
            
        }

        private IEnumerator Ticking()
        {
            OnStarted?.Invoke();
            for (_timeElapsed = 0;
                _timeElapsed < Length;
                _timeElapsed += FixedTime ? Time.fixedDeltaTime : Time.deltaTime)
            {
                if (FixedTime)
                    yield return new WaitForFixedUpdate();
                else
                    yield return null;
                OnTick?.Invoke(TimeLeft);
            }
            
            OnExpired?.Invoke();
            
            if (Loop)
                Start();
            else
                Stop();
        }
    }
}
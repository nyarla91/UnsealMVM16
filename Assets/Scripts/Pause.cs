using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private readonly List<MonoBehaviour> _pauseSources = new List<MonoBehaviour>();

    public bool IsPaused => _pauseSources.Count > 0;

    public void Begin(MonoBehaviour source)
    {
        _pauseSources.Add(source);
        ValidateSources();
    }

    public void End(MonoBehaviour source)
    {
        _pauseSources.Remove(source);
        ValidateSources();
    }

    private void ValidateSources()
    {
        for (int i = _pauseSources.Count - 1; i >= 0; i--)
        {
            if (_pauseSources[i] == null)
                _pauseSources.RemoveAt(i);
        }
    }
}
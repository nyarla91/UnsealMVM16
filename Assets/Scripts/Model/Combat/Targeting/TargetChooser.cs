using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Localization;
using UnityEngine;

namespace Model.Combat.Targeting
{
    public class TargetChooser : MonoBehaviour
    {
        private readonly List<TargetToChoose> _targets = new List<TargetToChoose>();
        private List<TargetToChoose> _chosenTargets = new List<TargetToChoose>();
        private Type _currentlyChosenType;
        private int _targetsReuired;
        public bool ChooseActive { get; private set; }
        public bool ExactNumber { get; private set; }
        public bool SkipButtonPressed { get; set; }

        public event Action<Transform, LocalizedString> OnChooseStart;
        public event Action OnChooseEnd;

        public async Task<T> StartTargetChoose<T>(Transform origin, LocalizedString message, bool compulsory)  where T : MonoBehaviour
        {
            List<T> targets = await StartTargetsChoose<T>(origin, message, 1, compulsory);
            if (targets.Count > 0)
                return targets[0];
            return null;
        }

        public async Task<List<T>> StartTargetsChoose<T>(Transform origin, LocalizedString message, int ammount, bool exactNumber)  where T : MonoBehaviour
        {
            SkipButtonPressed = false;
            ExactNumber = exactNumber;
            ChooseActive = true;
            _targetsReuired = ammount;
            OnChooseStart?.Invoke(origin, message);
            _chosenTargets = new List<TargetToChoose>();
            _currentlyChosenType = typeof(T);
            List<TargetToChoose> properTargets = _targets.Where(target => 
                    target.GetComponent<T>() != null && !(target.transform.Equals(origin))).ToList();
            if (exactNumber && properTargets.Count <= ammount)
            {
                ChooseActive = false;
                OnChooseEnd?.Invoke();
                return TargetsToComponents<T>(properTargets);
            }

            foreach (var target in properTargets)
            {
                target.StartChoose();
            }
            while (_chosenTargets.Count != ammount && _chosenTargets.Count < properTargets.Count && (exactNumber || !SkipButtonPressed))
            {
                await Task.Delay(100);
            }
            SkipButtonPressed = false;
            await Task.Delay(125);
            ChooseActive = false;
            foreach (var target in properTargets)
            {
                target.EndChoose();
            }
            OnChooseEnd?.Invoke();
            return TargetsToComponents<T>(_chosenTargets);
        }

        public bool TryAddChosenTarget(TargetToChoose targetToAdd)
        {
            if (!ChooseActive || targetToAdd.GetComponent(_currentlyChosenType) == null || _chosenTargets.Count >= _targetsReuired)
                return false;
            _chosenTargets.Add(targetToAdd);
            return true;
        }

        public void RemoveChosenTarget(TargetToChoose targetToAdd)
        {
            _chosenTargets.Remove(targetToAdd);
        }

        public bool TargetChosen(TargetToChoose targetToCheck)
        {
            return _chosenTargets.Contains(targetToCheck);
        }
        
        public void AddTarget(TargetToChoose targetToAdd)
        {
            _targets.Add(targetToAdd);
        }

        public void RemoveTarget(TargetToChoose targetToRemove)
        {
            _targets.Remove(targetToRemove);
        }

        private List<T> TargetsToComponents<T>(List<TargetToChoose> targets)
        {
            return targets.Select(target => target.GetComponent<T>()).ToList();
        }
    }
}
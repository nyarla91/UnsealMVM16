using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Model.Combat.TargetChoose
{
    public class TargetChooser : MonoBehaviour
    {
        private readonly List<TargetToChoose> _targets = new List<TargetToChoose>();
        private List<TargetToChoose> _chosenTargets = new List<TargetToChoose>();
        private Type _currentlyChosenType;
        private int _targetsReuired;
        public bool ChooseActive { get; private set; }

        public async Task<List<T>> StartTargetsChoose<T>(int ammount, bool exactNumber)
        {
            return await StartTargetsChoose<T>(ammount, exactNumber, null);
        }
        
        public async Task<List<T>> StartTargetsChoose<T>(int ammount, bool exactNumber, TargetToChoose ignoredTarget)
        {
            ChooseActive = true;
            _targetsReuired = ammount;
            _chosenTargets = new List<TargetToChoose>();
            _currentlyChosenType = typeof(T);
            List<TargetToChoose> properTargets = _targets.Where(target => 
                    target.GetComponent<T>() != null && (ignoredTarget == null || !ignoredTarget.Equals(target))).ToList();
            if (properTargets.Count <= ammount)
            {
                ChooseActive = false;
                return TargetsToComponents<T>(properTargets);
            }

            foreach (var target in properTargets)
            {
                target.StartChoose();
            }
            while (_chosenTargets.Count != ammount)
            {
                await Task.Delay(100);
            }
            ChooseActive = false;
            await Task.Delay(150);
            foreach (var target in properTargets)
            {
                target.EndChoose();
            }
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
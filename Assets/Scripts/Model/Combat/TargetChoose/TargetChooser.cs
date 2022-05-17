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
        public bool ChooseActive { get; private set; }

        public async Task<List<T>> StartTargetsChoose<T>(int ammount, bool exactNumber)
        {
            return await StartTargetsChoose<T>(ammount, exactNumber, null);
        }
        
        public async Task<List<T>> StartTargetsChoose<T>(int ammount, bool exactNumber, TargetToChoose ignoredTarget)
        {
            ChooseActive = true;
            _chosenTargets = new List<TargetToChoose>();
            await Task.Delay(500);
            _currentlyChosenType = typeof(T);
            List<TargetToChoose> properTargets =
                _targets.Where(target => target.GetComponent<T>() != null && !ignoredTarget.Equals(target)).ToList();
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
            await Task.Delay(250);
            foreach (var target in properTargets)
            {
                target.EndChoose();
            }
            return TargetsToComponents<T>(_chosenTargets);
        }

        public void AddChosenTarget(TargetToChoose targetToAdd)
        {
            if (!ChooseActive || targetToAdd.GetComponent(_currentlyChosenType) == null)
                return;
            _chosenTargets.Add(targetToAdd);
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
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Game.Engine
{
    public sealed class HarvestComponent : MonoBehaviour
    {
        public event Action OnStarted;
        public event Action OnEnded;

        public event Action<string> OnStartedID;

        public bool IsHarvesting => _coroutine != null;

        [SerializeField]
        private float _duration = 0.5f;

        [SerializeField]
        private float _postDelay = 0.25f;

        private readonly AndCondition _condition = new();
        
        private Coroutine _coroutine;
        private Action _processAction;
        private Action<string> _processActionID;

        public void SetProcessAction(Action action)
        {
            _processAction = action;
        }

        public void SetProcessActionID(Action<string> action)
        {
            _processActionID = action;
        }

        public void AddCondition(Func<bool> condition)
        {
            _condition.AddCondition(condition);
        }

        public void AddCondition(string id, Func<bool> condition)
        {
            _condition.AddCondition(id, condition);
        }

        public bool StartHarvest()
        {
            if (_coroutine != null)
            {
                return false;
            }

            if (!_condition.Invoke())
            {
                return false;
            }

            _coroutine = StartCoroutine(HarvestRoutine());
            OnStarted?.Invoke();
            return true;
        }

        public bool StartHarvest(string id)
        {
            if (_coroutine != null)
            {
                return false;
            }

            if (!_condition.Invoke(id))
            {
                return false;
            }

            _coroutine = StartCoroutine(HarvestRoutineID(id));
            //OnStarted?.Invoke();
            OnStartedID?.Invoke(id);
            return true;
        }

        public bool CancelHarvest()
        {
            if (_coroutine == null)
            {
                return false;
            }

            StopCoroutine(_coroutine);
            _coroutine = null;
            return true;
        }
        
        private IEnumerator HarvestRoutine()
        {
            yield return new WaitForSeconds(_duration);
            _processAction?.Invoke();
            yield return new WaitForSeconds(_postDelay);
            
            _coroutine = null;
            OnEnded?.Invoke();
        }

        private IEnumerator HarvestRoutineID(string id)
        {
            yield return new WaitForSeconds(_duration);
            _processActionID?.Invoke(id);
            yield return new WaitForSeconds(_postDelay);

            _coroutine = null;
            OnEnded?.Invoke();
        }
    }
}
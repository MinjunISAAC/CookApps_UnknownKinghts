using InGame.ForUnit.ForData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForUnit
{
    public class Unit : MonoBehaviour
    {
        // --------------------------------------------------
        // Unit State
        // --------------------------------------------------
        public enum EState
        {
            Unknown = 0,
            Intro   = 1,
            Run     = 2,
            Idle    = 3,
            Attack  = 4,
            Skill   = 5,
            Die     = 6,
        }

        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Unit Data Group")]
        [SerializeField] private UnitData _unitData = null;

        [Header("Animate Group")]
        [SerializeField] private Animator _animator = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Private
        [SerializeField] private EState    _unitState       = EState.Unknown;
        private Coroutine _co_CurrentState = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public UnitData UnitData => _unitData;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void ChangeToUnitData(UnitData unitData) 
        {
            _unitData = unitData;
        }

        // --------------------------------------------------
        // Functions - State
        // --------------------------------------------------
        // ----- Public
        public void ChangeToUnitState(EState state, float duration = 0.0f, Action doneCallBack = null)
        => _ChangeToState(state, duration, doneCallBack);

        // ----- Private
        private void _ChangeToState(EState state, float duration, Action doneCallBack)
        {
            if (!Enum.IsDefined(typeof(EState), state))
            {
                Debug.LogError($"<color=red>[Unit._ChangeToState] {Enum.GetName(typeof(EState), state)}은 정의되어있지 않은 Enum 값입니다.</color>");
                return;
            }

            if (_unitState == state)
                return;

            _unitState = state;

            if (_co_CurrentState != null)
                StopCoroutine(_co_CurrentState);

            switch (_unitState)
            {
                case EState.Intro  : _State_Intro (duration, doneCallBack); break;
                case EState.Run    : _State_Run   (); break;
                case EState.Idle   : _State_Idle  (); break;
                case EState.Attack : _State_Attack(); break;
                case EState.Skill  : _State_Skill (); break;
                case EState.Die    : _State_Die   (); break;
            }
        }

        private void _State_Intro (float duration, Action doneCallBack) => _co_CurrentState = StartCoroutine(_Co_Intro (duration, doneCallBack));
        private void _State_Run   () => _co_CurrentState = StartCoroutine(_Co_Run   ());
        private void _State_Idle  () => _co_CurrentState = StartCoroutine(_Co_Idle  ());
        private void _State_Attack() => _co_CurrentState = StartCoroutine(_Co_Attack());
        private void _State_Skill () => _co_CurrentState = StartCoroutine(_Co_Skill ());
        private void _State_Die   () => _co_CurrentState = StartCoroutine(_Co_Die   ());

        private IEnumerator _Co_Intro(float duration, Action doneCallBack)
        {
            var sec      = 0.0f;
            var startPos = transform.position;
            var endPos   = transform.position + transform.forward * 3f;

            Debug.Log($"Forward Check {gameObject.name} | {transform.forward}");
            _animator.SetTrigger($"{EState.Run}");
            while (sec < duration)
            {
                sec += Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, endPos, sec / duration);
                yield return null;
            }

            _animator.SetTrigger($"{EState.Idle}");

            transform.position = endPos;
            doneCallBack?.Invoke();
        }

        private IEnumerator _Co_Run()
        {

            yield return null;
        }

        private IEnumerator _Co_Idle()
        {

            yield return null;
        }

        private IEnumerator _Co_Attack()
        {

            yield return null;
        }

        private IEnumerator _Co_Skill()
        {

            yield return null;
        }

        private IEnumerator _Co_Die()
        {

            yield return null;
        }
    }
}
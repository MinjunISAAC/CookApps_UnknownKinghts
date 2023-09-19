// ----- C#
using System;
using System.Collections;

// ----- Unity
using UnityEngine;

// ----- User Defined

namespace InGame.ForCam
{
    public class CamController : MonoBehaviour
    {
        // --------------------------------------------------
        // Camera State Enum
        // --------------------------------------------------
        public enum ECamState
        {
            Unknown     = 0,
            Origin      = 1,
            BattleStart = 2,
            Battle      = 3,
            BattleEnd   = 4,
        }

        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Target Camera")]
        [SerializeField] private Camera    _targetCam      = null;

        [Header("Origin Transform")]
        [SerializeField] private Transform _originTrans    = null;
        [SerializeField] private Transform _movePointer    = null;

        [Header("Transform Offset Group")]
        [SerializeField] private Vector3   _positionOffset = Vector3.zero;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Private
        private ECamState _camState        = ECamState.Unknown;
        private Transform _targetTransform = null;
        private Coroutine _co_CurrentState = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void OnInit() { ChangeToCamState(ECamState.Origin); }
        public void ChangeToCamState(ECamState camState, Action doneCallBack = null)                 
        => _ChangeToCamState(camState, 0.0f, doneCallBack);
        
        public void ChangeToCamState(ECamState camState, float duration, Action doneCallBack = null) 
        => _ChangeToCamState(camState, duration, doneCallBack);

        // ----- Private
        private void _ChangeToCamState(ECamState camState, float duration = 0.0f, Action doneCallBack = null, Transform followTarget = null)
        {
            if (!Enum.IsDefined(typeof(ECamState), camState))
            {
                Debug.LogError($"[CamController._ChangeToCamState] {Enum.GetName(typeof(ECamState), camState)}은 정의되어있지 않은 Enum 값입니다.");
                return;
            }

            _camState = camState;

            if (_co_CurrentState != null)
                StopCoroutine(_co_CurrentState);

            switch (_camState)
            {
                case ECamState.Origin      : _State_Origin();                                          break;
                case ECamState.BattleStart : _State_BattleStart(duration, doneCallBack); break;
                case ECamState.Battle      : _Co_Battle();                                             break;
                case ECamState.BattleEnd   : _Co_Battle();                                             break;
            }
        }

        private void _State_Origin()
        {
            _targetTransform                   = null;
            _targetCam.transform.position      = _originTrans.position;
            _targetCam.transform.localRotation = _originTrans.localRotation;
        }

        private void _State_BattleStart(float duration, Action doneCallBack)
        => _co_CurrentState = StartCoroutine(_Co_BattleStart(duration, doneCallBack));

        /*
        private void _State_UnFollowUnit()
        {
            if (_targetUnit == null)
            {
                Debug.LogError($"[CamController._State_UnFollowUnit] Target Unit이 존재하지 않습니다.");
                return;
            }

            _co_CurrentState = StartCoroutine(_Co_UnFollowUnit());
        }
        */
        // --------------------------------------------------
        // Functions - State Coroutine
        // --------------------------------------------------
        private IEnumerator _Co_BattleStart(float duration, Action doneCallBack)
        {
            float      sec      = 0.0f;

            Vector3    startPos = _targetCam.transform.position;
            Quaternion startQuat= _targetCam.transform.rotation;

            Vector3    endPos   = _movePointer.position;
            Quaternion endQuat  = _movePointer.transform.rotation;

            while (sec < duration)
            {
                sec += Time.deltaTime;
                _targetCam.transform.position = Vector3   .Lerp(startPos , endPos , sec / duration);
                _targetCam.transform.rotation = Quaternion.Lerp(startQuat, endQuat, sec / duration);
                yield return null;
            }

            _targetCam.transform.position = endPos;
            _targetCam.transform.rotation = endQuat;

            _positionOffset = _targetCam.transform.position;

            doneCallBack?.Invoke();
        }

        private IEnumerator _Co_Battle()
        {
            while (_camState == ECamState.Battle)
            {
                if (_targetTransform != null)
                {
                    _targetCam.transform.position = Vector3.Lerp(_targetCam.transform.position, _targetTransform.transform.position + _positionOffset, 0.5f);
                }
                yield return null;
            }
        }

        private IEnumerator _Co_UnFollowUnit()
        {
            yield return null;
        }
    }
}
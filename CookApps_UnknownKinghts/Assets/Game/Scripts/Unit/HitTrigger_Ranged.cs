using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForUnit
{
    public class HitTrigger_Ranged : HitTrigger_Base
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private ParticleSystem _endHitFx    = null;
        [SerializeField] private Transform      _startTrans  = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private const float SHOOT_BASIC_VALUE = 0.2f;

        private Coroutine _co_Hit = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public override void PlayHitFx()
        {
            if (_targetUnit != null && _originFx != null)
            {
                if (_co_Hit == null)
                    _co_Hit = StartCoroutine(_Co_Shoot());
            }
        }

        // --------------------------------------------------
        // Functions - Coroutine
        // --------------------------------------------------
        private IEnumerator _Co_Shoot()
        {
            if (_targetUnit != null)
            {
                _originFx.gameObject.SetActive(true);
                var sec      = 0.0f;
                var startPos = _startTrans.transform.position;
                var endPos   = _targetUnit.transform.position;

                _originFx.transform.position = startPos;

                while (sec < SHOOT_BASIC_VALUE)
                {
                    sec += Time.deltaTime;

                    _originFx.transform.position = Vector3.Lerp(startPos, endPos, sec / SHOOT_BASIC_VALUE);
                    yield return null;
                }

                _originFx.Stop();
                _endHitFx.transform.position = endPos;
                _endHitFx.Play();

                HitToUnit(_targetUnit);
                _targetUnit.Hit((int)_power);
                _hitCallBack?.Invoke();

                _originFx.gameObject.SetActive(false);
                _co_Hit = null;
            }

        }
    }
}
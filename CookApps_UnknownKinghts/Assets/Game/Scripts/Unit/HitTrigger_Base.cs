using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForUnit
{
    public class HitTrigger_Base : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] protected ParticleSystem _originFx = null;

        // --------------------------------------------------
        // Event
        // --------------------------------------------------
        public event Action<Unit> onHit = null;
        public void HitToUnit(Unit unit)
        {
            if (onHit != null)
                onHit(unit);
        }

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        protected Unit   _targetUnit  = null;
        protected float  _power       = 0.0f;
        protected Action _hitCallBack = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void Set(float power, Unit targetUnit, Action doneCallBack)
        {
            _power       = power;
            _targetUnit  = targetUnit;
            _hitCallBack = doneCallBack;
        }

        public virtual void PlayHitFx()
        {
            if (_targetUnit != null)
            {
                if (_originFx != null)
                {
                    HitToUnit(_targetUnit);
                    _targetUnit.Hit((int)_power);
                    _originFx.Play();
                    _originFx.transform.position = _targetUnit.transform.position;
                    _hitCallBack?.Invoke();
                }
            }
        }
    }
}
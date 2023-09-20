using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForUnit
{
    public class SkillTrigger_Base : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] protected ParticleSystem _originFx = null;

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

        public virtual void PlaySkillFx()
        {
            if (_targetUnit != null)
            {
                _targetUnit.Hit((int)_power);
                _originFx.Play();
                _originFx.transform.position = _targetUnit.transform.position;

                _hitCallBack?.Invoke();
            }
        }
    }
}
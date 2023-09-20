using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForUnit
{
    public class SkillTrigger_Archur : SkillTrigger_Ranged
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private Transform            _startTrans = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private const float SHOOT_BASIC_VALUE = 0.1f;

        private Coroutine _co_Arrows = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public override void PlaySkillFx()
        {
            if (_co_Arrows == null)
                _co_Arrows = StartCoroutine(_Co_Shoot());
            
        }

        // --------------------------------------------------
        // Functions - Coroutine
        // --------------------------------------------------
        private IEnumerator _Co_Shoot()
        {
            _originFx.Play();

            yield return new WaitForSeconds(SHOOT_BASIC_VALUE);

            _hitCallBack?.Invoke();

            _originFx.gameObject.SetActive(false);
            _co_Arrows = null;
        }
    }
}
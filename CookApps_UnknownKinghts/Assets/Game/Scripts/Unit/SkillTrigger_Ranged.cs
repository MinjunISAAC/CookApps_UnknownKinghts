using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForUnit
{
    public class SkillTrigger_Ranged : SkillTrigger_Base
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private Transform            _startTrans    = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public override void PlaySkillFx()
        {
            _originFx.Play();
            _hitCallBack?.Invoke();
        }
    }
}
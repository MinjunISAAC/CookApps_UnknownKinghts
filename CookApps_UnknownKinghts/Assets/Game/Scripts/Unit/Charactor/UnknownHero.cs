using InGame.ForUnit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownHero : Unit
{
    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    private const float SKILL_FACTOR = 4f;

    // --------------------------------------------------
    // Functions - State
    // --------------------------------------------------
    protected override IEnumerator _Co_Skill()
    {
        if (_targetUnit != null)
            _animator.SetTrigger($"{EState.Skill}");

        transform.transform.LookAt(_targetUnit.transform);

        var power = _power * SKILL_FACTOR;
        _targetUnit.Hit((int)power);

        var delay   = _skillClip.length;
        yield return new WaitForSeconds(delay);
        
        ChangeToUnitState(EState.Run);
        yield return null;
    }

    protected override void _SkillToCallBack()
    {
        if (_targetUnit.Health <= 0)
        {
            var power = _power * SKILL_FACTOR;
            _targetUnit.Hit((int)power);

            _targetUnit.ChangeToUnitState(EState.Die);
            _targetUnit = null;

            var newTarget = _SearchToTargetUnit();
            _targetUnit = newTarget;

            if (_skillTrigger != null)
            {
                _skillTrigger.Set(_power, _targetUnit, _SkillToCallBack);
                _hitTrigger.Set(_power, _targetUnit, _HitToCallBack);
            }

            if (newTarget != null) ChangeToUnitState(EState.Run);
            else ChangeToUnitState(EState.Idle);
        }
    }
}

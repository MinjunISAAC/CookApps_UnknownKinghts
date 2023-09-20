using InGame.ForUnit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalArchur : Unit
{
    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    [SerializeField] private List<ParticleSystem> _particleGroup = null;

    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    private const float SKILL_FACTOR = 2f;

    // --------------------------------------------------
    // Functions - State
    // --------------------------------------------------
    protected override IEnumerator _Co_Skill()
    {
        if (_targetUnit != null)
            _animator.SetTrigger($"{EState.Skill}");

        var delay = _skillClip.length;
        yield return new WaitForSeconds(delay);

        ChangeToUnitState(EState.Run);
        yield return null;
    }

    protected override void _HitToCallBack()
    {
        if (_targetUnit.Health <= 0)
        {
            var power        = _power;
            var criticalRate = _criticalRate / PERCENT_VALUE;
            var randomValue  = UnityEngine.Random.value;

            if (randomValue < criticalRate)
                power = _criticalDamage;

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
            else                   ChangeToUnitState(EState.Idle);
        }
    }

    protected override void _SkillToCallBack()
    {
        var power = _power * (int)SKILL_FACTOR;

        for (int i = 0; i < _enemyUnitList.Count; i++)
        {
            var unit = _enemyUnitList[i];
            if (unit.UnitState != EState.Die)
            {
                if (unit.Health > 0)
                {
                    _particleGroup[i].transform.position = new Vector3(unit.transform.position.x, _particleGroup[i].transform.position.y, unit.transform.position.z);
                    _particleGroup[i].Play();
                    unit.Hit(power);
                }
            }
        }

        if (_skillTrigger != null)
        {
            _skillTrigger.Set(_power, _targetUnit, _SkillToCallBack);
            _hitTrigger  .Set(_power, _targetUnit, _HitToCallBack);
        }

        if (_targetUnit != null) ChangeToUnitState(EState.Run);
        else                     ChangeToUnitState(EState.Idle);
       
        ChangeToUnitState(EState.Run);
    }
}

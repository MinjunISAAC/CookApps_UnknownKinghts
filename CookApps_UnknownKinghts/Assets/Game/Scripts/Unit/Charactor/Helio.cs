using InGame.ForUnit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helio : Unit
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
        _targetUnit = _SearchToFarthestUnit();

        transform.transform.LookAt(_targetUnit.transform);

        _animator.SetTrigger($"{EState.Run}");

        while (Vector3.Distance(transform.position, _targetUnit.transform.position) > 2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetUnit.transform.position, Time.deltaTime * 2f);
            yield return null;
        }

        _animator.SetTrigger($"{EState.Skill}");

        var delayTIme = _skillClip.length;
        yield return new WaitForSeconds(delayTIme);

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

    private Unit _SearchToFarthestUnit()
    {
        float farthestDistance = 0f;
        Unit  farthestEnemy = null;

        for (int i = 0; i < _enemyUnitList.Count; i++)
        {
            var enemy = _enemyUnitList[i];
            if (enemy.UnitState != EState.Die)
            {
                float sqrDistance = (transform.position - enemy.transform.position).sqrMagnitude;

                if (sqrDistance > farthestDistance)
                {
                    farthestDistance = sqrDistance;
                    farthestEnemy    = enemy;
                }
            }
        }

        return farthestEnemy;
    }
}

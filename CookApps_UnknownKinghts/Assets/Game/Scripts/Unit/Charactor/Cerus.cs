using InGame.ForUnit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cerus : Unit
{
    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    [SerializeField] private List<ParticleSystem> _particleGroup = new List<ParticleSystem>();

    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    private const float SKILL_FACTOR = 3.5f;

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
            _targetUnit   = newTarget;

            if (_skillTrigger != null)
            {
                _skillTrigger.Set(_power, _targetUnit, _SkillToCallBack);
                _hitTrigger  .Set(_power, _targetUnit, _HitToCallBack);
            }

            if (newTarget != null) ChangeToUnitState(EState.Run);
            else                   ChangeToUnitState(EState.Idle);
        }
    }

    protected override void _SkillToCallBack()
    {
        var healValue = _power * SKILL_FACTOR;

        for (int i = 0; i < _playerUnitList.Count; i++)
        {
            var player = _playerUnitList[i];

            if (player.UnitData.UnitType == this.UnitData.UnitType)
                continue;
            else
            {
                if (player.UnitState != EState.Die)
                {
                    player.Heal((int)healValue);
                    _particleGroup[i].transform.SetParent(player.transform);
                    _particleGroup[i].transform.position = player.transform.position;
                    _particleGroup[i].Play();
                }
            }
        }

        ChangeToUnitState(EState.Run);
    }
}

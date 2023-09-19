using InGame.ForBattle.ForTime;
using InGame.ForUnit.ForData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForUnit
{
    public class Unit : MonoBehaviour
    {
        // --------------------------------------------------
        // Unit State
        // --------------------------------------------------
        public enum EState
        {
            Unknown = 0,
            Intro   = 1,
            Run     = 2,
            Idle    = 3,
            Attack  = 4,
            Hit     = 5,
            Skill   = 6,
            Die     = 7,
        }

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
        // Components
        // --------------------------------------------------
        [Header("Unit Data Group")]
        [SerializeField] private UnitData _unitData = null;

        [Header("Animate Group")]
        [SerializeField] private Animator      _animator   = null;
        [SerializeField] private AnimationClip _attackClip = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Private
        [SerializeField] private EState    _unitState       = EState.Unknown;
        
        private Coroutine _co_CurrentState = null;
        [SerializeField] private Unit      _targetUnit      =  null;

        private List<Unit> _enemyUnitList = new List<Unit>();

        public float _attackDistane  = 0f;
        public int   _power          = 0;
        public int   _health         = 0;
        public int   _defense        = 0;
        public int   _penetratePower = 0;
        public int   _criticalDamage = 0;
        public int   _criticalRate   = 0;
        public float _attackSpeed    = 0f;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public UnitData UnitData   => _unitData;
        public EState   UnitState   => _unitState;
        public Unit     TargetUnit => _targetUnit;

        public float AttackDistane  => _attackDistane;
        public float Power          => _power;
        public float Health         => _health; 
        public float Defense        => _defense;
        public float PenetratePower => _penetratePower;
        public float CriticalDamage => _criticalDamage;
        public float CritlcalRate   => _criticalRate;
        public float AttackSpeed    => _attackSpeed;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void ChangeToUnitData(UnitData unitData) 
        {
            _unitData = unitData;

            _attackDistane  = _unitData.AttackDistane;
            _power          = _unitData.Ability.Power;
            _health         = _unitData.Ability.Health;
            _defense        = _unitData.Ability.Defense;
            _penetratePower = _unitData.Ability.PenetratePower;
            _criticalDamage = _unitData.Ability.CriticalDamage;
            _criticalRate   = _unitData.Ability.CriticalRate;
            _attackSpeed    = _unitData.Ability.AttackSpeed;
        }

        public void SetToTargetUnit(Unit unit)
        {
            if (_targetUnit != null)
                return;

            _targetUnit = unit;
        }

        public void SetToEnemyGroup(List<Unit> unitGroup)
        {
            for (int i = 0; i < unitGroup.Count; i++)
            {
                var unit = unitGroup[i];
                _enemyUnitList.Add(unit);
            }
        }

        public void SetToTimeScale() 
        {
            _animator.speed = TimeScaler.GetValue();
        } 

        public void Hit(int hitValue)
        {
            _health -= hitValue;
        }

        private Unit _SearchToTargetUnit()
        {
            if (_targetUnit != null)
                return null;

            float closestDistance = float.MaxValue;
            Unit  closestEnemy    = null;

            for (int i = 0; i < _enemyUnitList.Count; i++)
            {
                var enemy = _enemyUnitList[i];
                if (enemy.UnitState != EState.Die)
                {
                    float sqrDistance = (transform.position - enemy.transform.position).sqrMagnitude;

                    if (sqrDistance < closestDistance)
                    {
                        closestDistance = sqrDistance;
                        closestEnemy    = enemy;
                    }
                }
            }

            return closestEnemy;
        }

        // --------------------------------------------------
        // Functions - State
        // --------------------------------------------------
        // ----- Public
        public void ChangeToUnitState(EState state, float duration = 0.0f, Action doneCallBack = null)
        => _ChangeToState(state, duration, doneCallBack);

        // ----- Private
        private void _ChangeToState(EState state, float duration, Action doneCallBack)
        {
            if (!Enum.IsDefined(typeof(EState), state))
            {
                Debug.LogError($"<color=red>[Unit._ChangeToState] {Enum.GetName(typeof(EState), state)}은 정의되어있지 않은 Enum 값입니다.</color>");
                return;
            }

            if (_unitState == state)
                return;

            _unitState = state;

            if (_co_CurrentState != null)
                StopCoroutine(_co_CurrentState);

            switch (_unitState)
            {
                case EState.Intro  : _State_Intro (duration, doneCallBack); break;
                case EState.Run    : _State_Run   (); break;
                case EState.Idle   : _State_Idle  (); break;
                case EState.Attack : _State_Attack(); break;
                case EState.Hit    : _State_Hit   (); break;
                case EState.Skill  : _State_Skill (); break;
                case EState.Die    : _State_Die   (); break;
            }
        }

        private void _State_Intro (float duration, Action doneCallBack) => _co_CurrentState = StartCoroutine(_Co_Intro (duration, doneCallBack));
        private void _State_Run   () => _co_CurrentState = StartCoroutine(_Co_Run   ());
        private void _State_Idle  () => _co_CurrentState = StartCoroutine(_Co_Idle  ());
        private void _State_Attack() => _co_CurrentState = StartCoroutine(_Co_Attack());
        private void _State_Hit   () => _co_CurrentState = StartCoroutine(_Co_Hit   ());
        private void _State_Skill () => _co_CurrentState = StartCoroutine(_Co_Skill ());
        private void _State_Die   () => _co_CurrentState = StartCoroutine(_Co_Die   ());

        private IEnumerator _Co_Intro(float duration, Action doneCallBack)
        {
            var sec      = 0.0f;
            var startPos = transform.position;
            var endPos   = transform.position + transform.forward * 3f;

            _animator.speed = _animator.speed * TimeScaler.GetValue();

            _animator.SetTrigger($"{EState.Run}");
            while (sec < duration)
            {
                sec += Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, endPos, sec / duration);
                yield return null;
            }

            _animator.SetTrigger($"{EState.Idle}");

            transform.position = endPos;

            doneCallBack?.Invoke();
        }

        private IEnumerator _Co_Run()
        {
            if (_targetUnit != null)
            {
                _animator.SetTrigger($"{EState.Run}");
                while (Vector3.Distance(transform.position, _targetUnit.transform.position) > _unitData.AttackDistane)
                {
                    transform.transform.LookAt(_targetUnit.transform);
                    transform.position = Vector3.MoveTowards(transform.position, _targetUnit.transform.position, Time.deltaTime * TimeScaler.GetValue());
                    yield return null;
                }

                ChangeToUnitState(EState.Attack);
            }
            else
            {

            }

            yield return null;
        }

        private IEnumerator _Co_Idle()
        {
            _animator.SetTrigger($"{EState.Idle}");
            yield return null;
        }

        private IEnumerator _Co_Attack()
        {
            if (_targetUnit != null) 
                _animator.SetTrigger($"{EState.Attack}");

            var sec = 0.0f;

            transform.transform.LookAt(_targetUnit.transform);
            
            while (_unitState == EState.Attack)
            {
                if (sec < _attackClip.length * 0.75f * _attackSpeed * TimeScaler.GetValue())
                    sec += Time.deltaTime * TimeScaler.GetValue();
                else
                {
                    var power        = _power;
                    var criticalRate = _criticalRate / 100f;
                    var randomValue  = UnityEngine.Random.value;

                    if (randomValue < criticalRate)
                        power = _criticalDamage;

                    _targetUnit.Hit(power);

                    HitToUnit(_targetUnit);

                    if (_targetUnit.Health <= 0)
                    {
                        _targetUnit.ChangeToUnitState(EState.Die);
                        _targetUnit = null;

                        var newTarget = _SearchToTargetUnit();
                        _targetUnit = newTarget;

                        if (newTarget != null) ChangeToUnitState(EState.Run);
                        else                   ChangeToUnitState(EState.Idle);
                    }

                    sec = 0.0f;
                }

                
                yield return null;
            }
            
            
            yield return null;
        }

        private IEnumerator _Co_Hit()
        {

            yield return null;
        }

        private IEnumerator _Co_Skill()
        {

            yield return null;
        }

        private IEnumerator _Co_Die()
        {
            _animator.SetTrigger($"{EState.Die}");
            yield return null;
        }
    }
}
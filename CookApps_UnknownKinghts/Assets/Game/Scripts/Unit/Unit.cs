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
        // Components
        // --------------------------------------------------
        [Header("Unit Data Group")]
        [SerializeField] protected UnitData _unitData = null;

        [Header("Animate Group")]
        [SerializeField] protected Animator      _animator   = null;
        [SerializeField] protected AnimationClip _attackClip = null;
        [SerializeField] protected AnimationClip _skillClip  = null;

        [Header("Hit  Group")]
        [SerializeField] protected SkillTrigger_Base _skillTrigger = null;
        [SerializeField] protected HitTrigger_Base   _hitTrigger   = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Const
        protected const float INTRO_WALK_VALUE = 3f;
        protected const float PERCENT_VALUE    = 100f;

        // ----- Private
        [SerializeField] protected EState     _unitState       = EState.Unknown;
        protected Coroutine  _co_CurrentState = null;
        protected Unit       _targetUnit      = null;
        protected List<Unit> _playerUnitList  = new List<Unit>();
        protected List<Unit> _enemyUnitList   = new List<Unit>();

        protected float _attackDistane  = 0f;
        protected int   _power          = 0;
        protected int   _health         = 0;
        protected int   _defense        = 0;
        protected int   _penetratePower = 0;
        protected int   _criticalDamage = 0;
        protected int   _criticalRate   = 0;
        protected float _attackSpeed    = 0f;

        protected string _skillName     = "";
        protected float  _skillCoolTime = 0f;
        protected float  _skillTime     = 0f;
        protected bool   _isStart       = false;
        
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
        // Functions - Event
        // --------------------------------------------------
        private void Update()
        {
            if (!_isStart)
                return;

            if (_skillTime < _skillCoolTime) 
            {
                _skillTime += Time.deltaTime;
            }
            else
            {
                ChangeToUnitState(EState.Skill);
                _skillTime = 0;
            }

            Debug.Log($"UNIT {gameObject.name} | Cool Time {_skillTime}");
        }

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

            _skillName      = _unitData.SkillGroup[0].Name;
            _skillCoolTime  = _unitData.SkillGroup[0].CoolTime;
        }

        public void SetToTargetUnit(Unit unit)
        {
            if (_targetUnit != null)
                return;

            _targetUnit = unit;

            if (_skillTrigger != null)
            {
                _skillTrigger.Set(_power, _targetUnit, _SkillToCallBack);
                _hitTrigger  .Set(_power, _targetUnit, _HitToCallBack);
            }
        }

        public void SetToPlayerGroup(List<Unit> unitGroup)
        {
            for (int i = 0; i < unitGroup.Count; i++)
            {
                var unit = unitGroup[i];
                _playerUnitList.Add(unit);
            }
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

        public void Heal(int healValue)
        {
            _health += healValue;
        }

        public void SetToHitEvent(Action<Unit> hitAction)
        {
            _hitTrigger.onHit += (hitAction);
        }

        protected Unit _SearchToTargetUnit()
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

        protected virtual void _HitToCallBack()
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
                    _hitTrigger  .Set(_power, _targetUnit, _HitToCallBack);
                }

                if (newTarget != null) ChangeToUnitState(EState.Run);
                else ChangeToUnitState(EState.Idle);
            }
        }

        protected virtual void _SkillToCallBack()
        {
            
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
            var endPos   = transform.position + transform.forward * INTRO_WALK_VALUE;

            _isStart   = false;
            _skillTime = 0.0f;

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
            _isStart = true;
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

        protected virtual IEnumerator _Co_Attack()
        {
            _animator.ResetTrigger($"{EState.Run}");

            if (_targetUnit != null) 
                _animator.SetTrigger($"{EState.Attack}");

            var sec = 0.0f;

            transform.transform.LookAt(_targetUnit.transform);
            
            while (_unitState == EState.Attack)
            {
                if (sec < _attackClip.length * _attackSpeed * TimeScaler.GetValue())
                    sec += Time.deltaTime * TimeScaler.GetValue();
                else
                    sec = 0.0f;
                
                yield return null;
            }
            
            
            yield return null;
        }

        private IEnumerator _Co_Hit()
        {

            yield return null;
        }

        protected virtual IEnumerator _Co_Skill()
        {

            yield return null;
        }

        private IEnumerator _Co_Die()
        {
            _animator.SetTrigger($"{EState.Die}");
            
            _isStart   = false;
            _skillTime = 0.0f;
            
            yield return null;
        }
    }
}
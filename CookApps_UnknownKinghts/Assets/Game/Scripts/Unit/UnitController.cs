// ----- C#
using InGame.ForState.ForBuildDeck;
using InGame.ForUnit.ForData;
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.ForObjectPool;

namespace InGame.ForUnit
{
    public class UnitController : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. Unit(Player) Group")]
        [SerializeField] private List<Unit>      _playerUniList    = null;

        [Header("2. Unit(Enemy) Group")]
        [SerializeField] private List<Unit>      _enemyUniList     = null;

        [Header("3. Position Group")]
        [SerializeField] private BuildDeckSetter _buildDeckSetter  = null;
        [SerializeField] private BuildDeckSetter _battleDeckSetter = null;

        [SerializeField] private Transform       _deckTrans        = null;

        [SerializeField] private UnitHit         _originUnitHit    = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Const
        private const int MAX_BUILDUNIT = 9;

        // ----- Private
        private Dictionary<EUnitType, Unit> _playerUnitPools = new Dictionary<EUnitType, Unit>();
        private Dictionary<EUnitType, Unit> _enemyUnitPools  = new Dictionary<EUnitType, Unit>();

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Start()
        {
            for (int i = 0; i < _playerUniList.Count; i++)
            {
                var unit       = _playerUniList[i];
                var unitPrefab = Instantiate(unit, transform);
                    
                if (!_playerUnitPools.ContainsKey(unit.UnitData.UnitType))
                {
                    _playerUnitPools.Add(unitPrefab.UnitData.UnitType, unitPrefab);
                    unitPrefab.gameObject.SetActive(false);
                }
            }

            for (int i = 0; i < _enemyUniList.Count; i++)
            {
                var unit = _enemyUniList[i];

                if (!_enemyUnitPools.ContainsKey(unit.UnitData.UnitType))
                {
                    _enemyUnitPools.Add(unit.UnitData.UnitType, unit);
                }
            }
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public Unit GetToPlayerUnit(UnitData unitData)
        {
            if (_playerUnitPools.TryGetValue(unitData.UnitType, out var targetUnit))
            {
                targetUnit.gameObject.SetActive(true);
                targetUnit.ChangeToUnitData(unitData);
                return targetUnit;
            }
            else
            {
                Debug.LogError($"<color=red>[UnitController.GetToPlayerUnit] {unitData.UnitType}에 맞는 Unit이 Pool에 존재하지 않습니다.</color>");
                return null;
            }
        }

        public void ReturnToPlayerUnit(UnitData unitData)
        {
            if (_playerUnitPools.TryGetValue(unitData.UnitType, out var targetUnit))
            {
                targetUnit.gameObject.SetActive(false);
                targetUnit.ChangeToUnitData(unitData);
            }
            else
                Debug.LogError($"<color=red>[UnitController.GetToPlayerUnit] {unitData.UnitType}에 맞는 Unit이 Pool에 존재하지 않습니다.</color>");
        }

        public List<Unit> GetToEnemyUnit(List<UnitData> unitDatas)
        {
            List<Unit> unitList = new List<Unit>();

            for (int i = 0; i < unitDatas.Count; i++)
            {
                var unitData = unitDatas[i];

                if (_enemyUnitPools.TryGetValue(unitData.UnitType, out var targetUnit))
                {
                    var unitPrefab = Instantiate(targetUnit, transform);
                    unitPrefab.ChangeToUnitData(unitData);
                    unitList.Add(unitPrefab);
                }
                else
                {
                    Debug.LogError($"<color=red>[UnitController.GetToEnemyUnit] {unitData.UnitType}에 맞는 Unit이 Pool에 존재하지 않습니다.</color>");
                }
            }

            return unitList;
        }
        public void SetToPlayerUnit_BattleDeck(List<Unit> unitList) => _battleDeckSetter.SetToBuildPlayerDeck(unitList);
        public void SetToEnemyUnit_BattleDeck(List<Unit> unitList)  => _battleDeckSetter.SetToBuildEnemyDeck(unitList);

        public void SetToPlayerUnit_BuildDeck(List<Unit> unitList) => _buildDeckSetter.SetToBuildPlayerDeck(unitList);
        public void SetToEnemyUnit_BuildDeck(List<Unit> unitList) => _buildDeckSetter.SetToBuildEnemyDeck(unitList);
        
        public void SetToTargetUnit(List<Unit> unitList, Unit targetUnit)
        {
            for (int i = 0; i < unitList.Count; i++)
            {
                var unit = unitList[i];

                unit.SetToTargetUnit(targetUnit);
            }
        }
        public Unit SearchToTargetUnit(List<Unit> unitList)
        {
            for (int i = 0; i <unitList.Count; i++)
            {
                var unit = unitList[i];
                if (unit.UnitState != Unit.EState.Die)
                    return unit;
                else
                    continue;
            }

            return null;
        }
        public void SetToEnemyGroup(List<Unit> playerUnitList, List<Unit> enemyUnitList) 
        {
            void Set(List<Unit> listA, List<Unit> listB)
            {
                for(int i = 0; i < listA.Count; i++)
                {
                    var unit = listA[i];
                    unit.SetToEnemyGroup(listB);
                    unit.SetToPlayerGroup(listA);
                }
            }

            Set(playerUnitList, enemyUnitList);
            Set(enemyUnitList, playerUnitList);
        }
        public void SetToTimeScale(List<Unit> playerUnitList, List<Unit> enemyUnitList)
        {
            for (int i = 0; i < playerUnitList.Count; i++)
            {
                var unit = playerUnitList[i];
                unit.SetToTimeScale();
            }

            for (int i = 0; i < enemyUnitList.Count; i++)
            {
                var unit = enemyUnitList[i];
                unit.SetToTimeScale();
            }
        }


        public void SetToUnitHitEvent(List<Unit> unitList, Action<Unit> hitAction)
        {
            for (int i = 0; i < unitList.Count; i++)
            {
                var unit = unitList[i];
                unit.SetToHitEvent(hitAction);
            }
        }

        public void ResetToUnitHitEvent(List<Unit> unitList)
        {
        }
        /*
        public void ResetToUnit()
        {
            void Reset(List<Unit> unitList)
            {
                for (int i = unitList.Count - 1; i >= 0; i--)
                {
                    var unit = unitList[i];
                    Destroy(unit.gameObject);
                }
            }
            Reset(_currEnemyUnit);
            _buildDeckSetter.ResetToBuildDeck();
        }

        public void RefreshToPlayerUnitDeck(bool isAdd, EUnitType unitType)
        {
            if (isAdd) 
            {
                if (_playerUnitPools.TryGetValue(unitType, out var targetUnit))
                {
                    targetUnit.gameObject.SetActive(true);
                    _buildDeckSetter.RefreshToBuildDeck(isAdd, targetUnit);
                }
            }
            else
            {
                for (int i = 0; i < _currPlayerUnit.Count; i++)
                {
                    var unit = _currPlayerUnit[i];
                    if (unit.UnitData.UnitType == unitType)
                    {
                        _buildDeckSetter.RefreshToBuildDeck(isAdd, unit);
                        _currPlayerUnit.Remove(unit);
                        break;
                    }
                }
            }
        }

        public void SetToBuildDeck(List<Unit> playerUnitList, List<Unit> enemyUnitList)
        {
            _currPlayerUnit = playerUnitList;
            _currEnemyUnit  = enemyUnitList;

            _buildDeckSetter.SetToBuildDeck(playerUnitList, enemyUnitList);
        }

        public void SetToBuildDeck()
        => _buildDeckSetter.SetToBuildDeck(_currPlayerUnit);
        */
    }
}
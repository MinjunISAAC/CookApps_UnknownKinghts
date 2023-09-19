using InGame.ForUnit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForState.ForBuildDeck
{
    public class BuildDeck : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private BuildDeckArea _frontDeckArea  = null;
        [SerializeField] private BuildDeckArea _centerDeckArea = null;
        [SerializeField] private BuildDeckArea _rearDeckArea   = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Dictionary<EAttackPosType, BuildDeckArea> _areaSet = new Dictionary<EAttackPosType, BuildDeckArea>();
        private Dictionary<EAttackPosType, List<Unit>>    _unitSet = new Dictionary<EAttackPosType, List<Unit>>();

        [SerializeField] private List<Unit> _frontUnitList  = new List<Unit>();
        [SerializeField] private List<Unit> _centerUnitList = new List<Unit>();
        [SerializeField] private List<Unit> _rearUnitList   = new List<Unit>();

        private bool       _isInit         = false;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Start()
        {
            if (_isInit)
                return;

            _areaSet.Add(EAttackPosType.Front,  _frontDeckArea);
            _areaSet.Add(EAttackPosType.Center, _centerDeckArea);
            _areaSet.Add(EAttackPosType.Rear,   _rearDeckArea);

            _unitSet.Add(EAttackPosType.Front,  _frontUnitList);
            _unitSet.Add(EAttackPosType.Center, _centerUnitList);
            _unitSet.Add(EAttackPosType.Rear,   _rearUnitList);
            
            _isInit = true;
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void SetToUnit(List<Unit> unitList)
        {
            ResetToUnit();

            for(int i = 0; i < unitList.Count; i++)
            {
                var unit        = unitList[i];
                var unitData    = unit.UnitData;
                var unitLevel   = unitData.Level;
                var unitSpec    = unitData.SpecType;
                var unitName    = unitData.Name;
                var unitJob     = unitData.JobType;
                var unitPosType = unitData.AttackPosType;

                if (_unitSet.TryGetValue(unitPosType, out var unitGroup))
                {
                    if (unitGroup.Count < 3) { unitGroup.Add(unit); }
                }
            }

            if (_areaSet.TryGetValue(EAttackPosType.Front, out var frontArea))
            {
                var positionList = frontArea.GetToAreaList(_frontUnitList.Count);

                for (int i = 0; i < _frontUnitList.Count; i++)
                {
                    var unit = _frontUnitList[i];
                    var pos  = positionList[i];

                    unit.transform.position = pos.position;
                    unit.transform.rotation = pos.rotation;
                }
            }
            
            if (_areaSet.TryGetValue(EAttackPosType.Center, out var centerArea))
            {
                var positionList = centerArea.GetToAreaList(_centerUnitList.Count);

                for (int i = 0; i < _centerUnitList.Count; i++)
                {
                    var unit = _centerUnitList[i];
                    var pos = positionList[i];

                    unit.transform.position = pos.position;
                    unit.transform.rotation = pos.rotation;
                }
            }

            if (_areaSet.TryGetValue(EAttackPosType.Rear, out var rearArea))
            {
                var positionList = rearArea.GetToAreaList(_rearUnitList.Count);

                for (int i = 0; i < _rearUnitList.Count; i++)
                {
                    var unit = _rearUnitList[i];
                    var pos = positionList[i];

                    unit.transform.position = pos.position;
                    unit.transform.rotation = pos.rotation;
                }
            }
        }

        public void ResetToUnit()
        {
            _frontUnitList .Clear();
            _centerUnitList.Clear();
            _rearUnitList  .Clear();
        }

        public void ExcludeToUnit(Unit unit)
        {
            var attackType = unit.UnitData.AttackPosType;
            Debug.Log($"Exclude 1 {attackType} | {_frontUnitList.Count} | {_centerUnitList.Count} | {_rearUnitList.Count}");
            if (_unitSet.TryGetValue(attackType, out var unitList))
            {
                for (int i = 0; i < unitList.Count; i++) 
                { 
                    var targetUnit = unitList[i];

                    if (targetUnit.UnitData.UnitType == unit.UnitData.UnitType)
                    {
                        Debug.Log($"Exclude 2 {attackType} | {_frontUnitList.Count} | {_centerUnitList.Count} | {_rearUnitList.Count}");

                        //_TryToTargetPos(false, targetUnit);
                        break;
                    }
                }
            }
        }

        public void IncludeToUnit(Unit unit)
        {
            Debug.Log($"Include {_frontUnitList.Count} | {_centerUnitList.Count} | {_rearUnitList.Count}");
            var  attackType = unit.UnitData.AttackPosType;
            var  isInclude  = false;
            Unit targetUnit = null;

            if (_unitSet.TryGetValue(attackType, out var unitList))
            {
                for (int i = 0; i < unitList.Count; i++)
                {
                    targetUnit = unitList[i];

                    if (targetUnit.UnitData.UnitType == unit.UnitData.UnitType)
                    {
                        isInclude = true;
                        break;
                    }
                }
            }
            
            //if (!isInclude) _TryToTargetPos(true, targetUnit);
        }

        // ----- Private
        /*
        private bool _TryToTargetPos(Unit unit)
        {
            var unitData    = unit.UnitData;
            var unitPosType = unitData.AttackPosType;

            if (!_unitSet.TryGetValue(unitPosType, out var unitList))
            {
                Debug.LogError($"<color=red>[BuildDeck._TryToTargetPos] {unitPosType}에 해당하는 Unit Set이 존재하지 않습니다.</color>");
                return false;
            }
        }
        */







/*        private bool _TryToTargetPos(Unit unit)
        {
            var unitData    = unit.UnitData;
            var unitPosType = unitData.AttackPosType;

            if (!_unitSet.TryGetValue(unitPosType, out var unitList))
            {
                Debug.LogError($"<color=red>[BuildDeck._TryToTargetPos] {unitPosType}에 해당하는 Unit Set이 존재하지 않습니다.</color>");
                return false;
            }

            if (!_areaSet.TryGetValue(unitPosType, out var area))
            {
                Debug.LogError($"<color=red>[BuildDeck._TryToTargetPos] {unitPosType}에 해당하는 Area 정보가 존재하지 않습니다.</color>");
                return false;
            }

            if (unitList.Count < area.MaxCopacity)
            {
                var count     = unitList.Count;
                var transList = area.GetToAreaList(count);

                for (int i = 0; i < transList.Count; i++)
                {
                    if (unitList.Count > 0)
                    {
                        var targetUnit = unitList[i];
                        var transform  = transList[i];
                 
                        targetUnit.transform.position = transform.position;
                        targetUnit.transform.rotation = transform.rotation;
                    }
                }

                return true;
            }
            else
                return false;
        }
*/
    }
}
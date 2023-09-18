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

        private List<Unit> _frontUnitList  = new List<Unit>();
        private List<Unit> _centerUnitList = new List<Unit>();
        private List<Unit> _rearUnitList   = new List<Unit>();

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
        public void SetToUnit(Unit targetUnit)
        {
            var unitData    = targetUnit.UnitData;
            var unitLevel   = unitData.Level;
            var unitSpec    = unitData.SpecType;
            var unitName    = unitData.Name;
            var unitJob     = unitData.JobType;
            var unitPosType = unitData.AttackPosType;

            // ���� �� ������ �ִ°�?
            if (_TryToTargetPos(true, targetUnit))
            {

            }
            else
            {
                // [TODO] Toast Message �ʿ�
            }

        }

        public void ResetToUnit()
        {
            _frontUnitList.Clear();
            _centerUnitList.Clear();
            _rearUnitList.Clear();
        }

        public void ExcludeToUnit(Unit unit)
        {
            var attackType = unit.UnitData.AttackPosType;
            if (_unitSet.TryGetValue(attackType, out var unitList))
            {
                for (int i = 0; i < unitList.Count; i++) 
                { 
                    var targetUnit = unitList[i];

                    if (targetUnit.UnitData.UnitType == unit.UnitData.UnitType)
                    {
                        _TryToTargetPos(false, targetUnit);
                        break;
                    }
                }
            }
        }

        // ----- Private
        private bool _TryToTargetPos(bool isAdd, Unit unit)
        {
            var unitData    = unit.UnitData;
            var unitPosType = unitData.AttackPosType;

            if (!_unitSet.TryGetValue(unitPosType, out var unitList))
            {
                Debug.LogError($"<color=red>[BuildDeck._TryToTargetPos] {unitPosType}�� �ش��ϴ� Unit Set�� �������� �ʽ��ϴ�.</color>");
                return false;
            }

            if (!_areaSet.TryGetValue(unitPosType, out var area))
            {
                Debug.LogError($"<color=red>[BuildDeck._TryToTargetPos] {unitPosType}�� �ش��ϴ� Area ������ �������� �ʽ��ϴ�.</color>");
                return false;
            }

            if (unitList.Count < area.MaxCopacity)
            {
                if (isAdd) unitList.Add(unit);
                else       unitList.Remove(unit);
                
                unit.gameObject.SetActive(isAdd);

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

    }
}
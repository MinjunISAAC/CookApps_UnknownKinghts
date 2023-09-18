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
        private Dictionary<EAttackPosType, BuildDeckArea> _areaSet = null;
        private Dictionary<EAttackPosType, List<Unit>>    _unitSet = null;

        private List<Unit> _frontUnitList  = new List<Unit>();
        private List<Unit> _centerUnitList = new List<Unit>();
        private List<Unit> _rearUnitList   = new List<Unit>();

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Start()
        {
            _areaSet.Add(EAttackPosType.Front,  _frontDeckArea);
            _areaSet.Add(EAttackPosType.Center, _centerDeckArea);
            _areaSet.Add(EAttackPosType.Rear,   _rearDeckArea);

            _unitSet.Add(EAttackPosType.Front,  _frontUnitList);
            _unitSet.Add(EAttackPosType.Center, _centerUnitList);
            _unitSet.Add(EAttackPosType.Rear,   _rearUnitList);
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

            // 현재 들어갈 공간이 있는가?
            if (_TryToTargetPos(targetUnit))
            {

            }
            else
            {
                // [TODO] Toast Message 필요
            }

        }

        // ----- Private
        private bool _TryToTargetPos(Unit unit)
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
                unitList.Add(unit);

                var count     = unitList.Count;
                var transList = area.GetToAreaList(count);

                for (int i = 0; i < transList.Count; i++)
                {
                    var targetUnit = unitList[i];
                    var transform  = transList[i];
                    
                    targetUnit.transform.position = transform.position;
                }

                return true;
            }
            else
                return false;
        }
    }
}
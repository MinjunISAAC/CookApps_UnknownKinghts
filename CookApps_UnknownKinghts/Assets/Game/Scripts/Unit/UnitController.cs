// ----- C#
using InGame.ForUnit.ForData;
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
        [SerializeField] private List<Unit> _unitList = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Dictionary<EUnitType, Unit> _unitPools = new Dictionary<EUnitType, Unit>();

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Start()
        {
            for (int i = 0; i < _unitList.Count; i++)
            {
                var unit       = _unitList[i];
                var unitPrefab = Instantiate(unit, transform);
                    
                if (!_unitPools.ContainsKey(unit.UnitData.UnitType))
                {
                    _unitPools.Add(unitPrefab.UnitData.UnitType, unitPrefab);
                }
                unitPrefab.gameObject.SetActive(false);
            }
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public Unit GetToUnit(UnitData unitData)
        {
            if (_unitPools.TryGetValue(unitData.UnitType, out var targetUnit))
            {
                targetUnit.gameObject.SetActive(true);
                targetUnit.ChangeToUnitData(unitData);
                Debug.Log($"왜그러지 {targetUnit}");
                return targetUnit;
            }
            else
            {
                Debug.LogError($"<color=red>[UnitController.GetToUnit] {unitData.UnitType}에 맞는 Unit이 Pool에 존재하지 않습니다.</color>");
                return null;
            }
        }
    }
}
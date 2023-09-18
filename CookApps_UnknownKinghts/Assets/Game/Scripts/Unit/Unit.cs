using InGame.ForUnit.ForData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForUnit
{
    public class Unit : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private UnitData _unitData = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public UnitData UnitData => _unitData;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void ChangeToUnitData(UnitData unitData) 
        {
            _unitData = unitData;
        }
    }
}
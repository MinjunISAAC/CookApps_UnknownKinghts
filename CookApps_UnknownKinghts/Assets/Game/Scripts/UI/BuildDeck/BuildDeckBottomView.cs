using InGame.ForUnit;
using InGame.ForUnit.ForData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForState.ForBuildDeck 
{ 
    public class BuildDeckBottomView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private UnitDeckCard _originUnitDeckCard = null;
        [SerializeField] private Transform    _unitDeckParents    = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private List<UnitData> _ownedUnitDataList = new List<UnitData>();

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void OnInit(List<UnitData> unitDataList, Action<bool, EUnitType> onCliCkCardBtn) 
        {
            for (int i = _unitDeckParents.childCount - 1; i >= 0; i--)
            {
                var unitDeck = _unitDeckParents.GetChild(i);
                Destroy(unitDeck.gameObject);
            }

            _ownedUnitDataList.Clear();

            for (int i = 0; i < unitDataList.Count; i++) 
            {
                var unitData = unitDataList[i];
                var unitDeck = Instantiate(_originUnitDeckCard, _unitDeckParents);

                unitDeck.SetToUnitCard(unitData, onCliCkCardBtn);
                _ownedUnitDataList.Add(unitData);
            }
        }
    }
}
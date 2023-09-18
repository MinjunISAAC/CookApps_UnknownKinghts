using InGame.ForUnit.ForData;
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
        public void OnInit(List<UnitData> unitDataList) 
        { 
            for (int i = 0; i < unitDataList.Count; i++) 
            {
                var unitData = unitDataList[i];
                var unitDeck = Instantiate(_originUnitDeckCard, _unitDeckParents);

                unitDeck.SetToUnitCard(unitData);
                _ownedUnitDataList.Add(unitData);
            }
        }
    }
}
using InGame.ForUnit;
using InGame.ForUnit.ForData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.ForState.ForBuildDeck 
{ 
    public class BuildDeckBottomView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private UnitDeckCard _originUnitDeckCard = null;
        [SerializeField] private Transform    _unitDeckParents    = null;
        [SerializeField] private Button       _BTN_BattleStart    = null; 

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private List<UnitDeckCard> _unitDeckCardList = new List<UnitDeckCard>();

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToBattleBtn(Action onClickBattle)
        {
            _BTN_BattleStart.onClick.AddListener
            (
                () =>
                {
                    onClickBattle();
                    _BTN_BattleStart.onClick.RemoveAllListeners();
                }
            );
        }

        public void SetToBuildDeck(List<UnitData> unitDataList)
        {
            for (int i = _unitDeckParents.childCount - 1; i >= 0; i--)
            {
                var unitDeck = _unitDeckParents.GetChild(i);
                Destroy(unitDeck.gameObject);
            }

            for (int i = 0; i < unitDataList.Count; i++)
            {
                var unitData = unitDataList[i];
                var unitDeck = Instantiate(_originUnitDeckCard, _unitDeckParents);

                _unitDeckCardList.Add(unitDeck);
                unitDeck.SetToDeckCardUI(unitData);
            }
        }

        public void SetTopBuildDeckBtn(Action<bool, EUnitType> onClickBtn)
        {
            for (int i = 0; i < _unitDeckCardList.Count; i++)
            {
                var deckCard = _unitDeckCardList[i];
                deckCard.SetToDeckCartOnClickEvent(onClickBtn);
            }
        }

        public void ResetToDeckCard()
        {
            for (int i = _unitDeckCardList.Count - 1; i >= 0; i--)
            {
                var deckCard = _unitDeckCardList[i];
                Destroy(deckCard.gameObject); 
            }

            _unitDeckCardList.Clear();
        }































        public void OnInit(List<UnitData> unitDataList, Action<bool, EUnitType> onCliCkCardBtn) 
        {
        }
    }
}
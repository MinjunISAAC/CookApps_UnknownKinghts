// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;

// ----- User Defined
using InGame.ForState.ForBuildDeck;
using InGame.ForUnit.ForData;
using InGame.ForUnit;

namespace InGame.ForState.ForUI
{
    public class BuildDeckView : StateView
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private BuildDeckBottomView _bottomView    = null;
        [SerializeField] private TextMeshProUGUI     _TMP_StageName = null; 
        [SerializeField] private Button              _BTN_Return    = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public(Override)
        public override void OnInit()   { }
        public override void OnFinish() { }

        // ----- public
        public void SetToReturnButton(Action onClickReturnBtn)
        => _BTN_Return.onClick.AddListener(() => { onClickReturnBtn(); });

        public void SetToBuildDeck_UI(List<UnitData> unitDataList)
        => _bottomView.SetToBuildDeck(unitDataList);

        public void SetToBuildDeck_OnClickEvent(Action<bool, EUnitType> onClickBtn)
        => _bottomView.SetTopBuildDeckBtn(onClickBtn);

        public void SetToBattleStart(Action onClickBattle)
        => _bottomView.SetToBattleBtn(onClickBattle);

        public void ResetToBuildDeckView()
        {
            _BTN_Return.onClick.RemoveAllListeners();
            _bottomView        .ResetToDeckCard();
        } 






















        public void SetToStageInfo(string chapterName, int chapterStep, int stageStep)
        => _TMP_StageName.text = $"{chapterName} {chapterStep}-{stageStep}";

        public void SetToBottomView(List<UnitData> ownedUnitDataList, Action<bool, EUnitType> onClickCardView)
        => _bottomView.OnInit(ownedUnitDataList, onClickCardView);
    }
}
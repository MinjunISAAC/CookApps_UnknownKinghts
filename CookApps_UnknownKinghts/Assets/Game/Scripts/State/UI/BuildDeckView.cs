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
        public override void OnInit() { }
        public override void OnFinish() { }

        // ----- public
        public void SetToReturnButton(Action onClickReturnBtn)
        => _BTN_Return.onClick.AddListener(() => { onClickReturnBtn(); });

        public void SetToStageInfo(string chapterName, int chapterStep, int stageStep)
        => _TMP_StageName.text = $"{chapterName} {chapterStep}-{stageStep}";

        public void SetToBottomView(List<UnitData> ownedUnitDataList)
        => _bottomView.OnInit(ownedUnitDataList);
    }
}
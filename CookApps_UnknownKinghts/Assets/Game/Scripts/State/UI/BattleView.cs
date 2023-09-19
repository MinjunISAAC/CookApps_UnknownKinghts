// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForBattle.ForUI;
using System;

namespace InGame.ForState.ForUI
{
    public class BattleView : StateView
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private TimerView        _timerView  = null;
        [SerializeField] private BattleBottomView _bottomView = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public override void OnInit()   { }
        public override void OnFinish() { }

        public void SetToBottomView(Action onClickTimeScale)
        {
            _bottomView.SetToTimeScaleBtn(onClickTimeScale);
        }
        public void VisiableToTimeScaleBtn(bool visiable) => _bottomView.VisiableTimeScaleBtn(visiable);
        public void PlayToTimer(float duration, Action doneCallBack) => _timerView.PlayToTimer(duration, doneCallBack);

        // --------------------------------------------------
        // Functions - 
        // --------------------------------------------------

    }
}
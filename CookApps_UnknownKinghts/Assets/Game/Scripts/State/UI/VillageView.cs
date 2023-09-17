// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForState.ForVillage;
using System;
using InGame.ForBattle;

namespace InGame.ForState.ForUI
{
    public class VillageView : StateView
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private ProfileView _profileView = null;
        [SerializeField] private BottomView  _bottomView  = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private bool _isInit = false;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public override void OnInit()   { }
        public override void OnFinish() { }

        public void SetToProfileView(int level, int exp, int levelUpExp, string userName)
        => _profileView.OnInit(level, exp, levelUpExp, userName);

        public void SetToBottomView(Action<EBattleType> onClickToBattleItem)
        { 
            if (!_isInit)
            {
                _bottomView.OnInit(onClickToBattleItem);
                _isInit = true;
            }
        } 
    }
}
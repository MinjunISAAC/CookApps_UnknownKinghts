// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUI;

namespace InGame.ForState.ForUI
{
    public class VillageView : StateView
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private ProfileView _profileView = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public override void OnInit()   { }
        public override void OnFinish() { }

        public void SetToProfileView(int level, int exp, int levelUpExp, string userName)
        {
            // Profile View Init
            _profileView.OnInit(level, exp, levelUpExp, userName);
        }
    }
}
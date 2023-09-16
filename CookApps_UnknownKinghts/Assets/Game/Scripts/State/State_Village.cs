// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.SimpleFSM;
using InGame.ForState.ForUI;
using Utility.ForData.ForUser;
using CoreData;

namespace InGame.ForState
{
    public class State_Village : SimpleState<EStateType>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Owner _owner = null;
        
        // ----- UI
        private VillageView _villageView = null;

        // --------------------------------------------------
        // Property
        // --------------------------------------------------
        public override EStateType State => EStateType.Village;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Protected Game Flow
        protected override void _Start(EStateType preStateKey, object startParam)
        {
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State�� �����Ͽ����ϴ�.</color>");

            #region <Manage Group>
            _owner = Owner.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] Owner�� Null �����Դϴ�.</color>");
                return;
            }

            _villageView = (VillageView)_owner.UIOwner.GetStateUI();
            if (_villageView == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] Village View�� Null �����Դϴ�.</color>");
                return;

            }
            #endregion

            // UI Init
            _SetToUI();
        }
        protected override void _Update()
        {

        }

        protected override void _Finish(EStateType nextStateKey)
        {
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State�� ��Ż�Ͽ����ϴ�.</color>");
        }

        // ----- Private
        private void _SetToUI()
        {
            // User Data Set
            var userLevel  = UserDataSystem.GetToLevel     ();
            var userExp    = UserDataSystem.GetToExp       ();
            var userName   = UserDataSystem.GetToUserName  ();
            var levelUpExp = CoreDataHelper.GetToLevelUpExp(userLevel + 1);
            
            // UI Init
            _villageView.OnInit();
            _villageView.SetToProfileView(userLevel, userExp, levelUpExp, userName);
        }
    }
}
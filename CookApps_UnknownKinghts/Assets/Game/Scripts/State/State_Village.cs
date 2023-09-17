// ----- C#
using System;

// ----- Unity
using UnityEngine;

// ----- User Defined
using CoreData;
using Utility.SimpleFSM;
using Utility.ForData.ForUser;
using Utiltiy.ForLoader;
using InGame.ForState.ForUI;
using InGame.ForBattle;

namespace InGame.ForState
{
    public class State_Village : SimpleState<EStateType>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Owner       _owner       = null;
        
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
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State에 진입하였습니다.</color>");

            #region <Manage Group>
            _owner = Owner.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] Owner가 Null 상태입니다.</color>");
                return;
            }

            _villageView = (VillageView)_owner.UIOwner.GetStateUI();
            if (_villageView == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] {State} View가 Null 상태입니다.</color>");
                return;
            }
            #endregion

            // Loader Hide
            Loader.Instance.Hide
            (
                () => { _villageView.gameObject.SetActive(true); }
                , null
            );

            // UI Init
            _SetToUI();
        }

        protected override void _Finish(EStateType nextStateKey)
        {
            _villageView.gameObject.SetActive(false);
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State에 이탈하였습니다.</color>");
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
            _villageView.SetToBottomView(_SetToBattleItem);
        }

        private void _SetToBattleItem(EBattleType battleType)
        {
            if (!Enum.IsDefined(typeof(EBattleType), battleType))
                return;

            switch (battleType)
            {
                case EBattleType.AcnientTower:
                    // [Toast Message]
                    break;
                case EBattleType.Expedition:
                    // [Toast Message]
                    break;
                case EBattleType.Arena:
                    // [Toast Message]
                    break;
                case EBattleType.Dungeon:
                    // [Toast Message]
                    break;
                case EBattleType.Adventure:
                    Loader.Instance.Show
                    (
                        null,
                        () => { StateMachine.Instance.ChangeState(EStateType.ChapterSelect); }
                    );
                    break;
            }
        }
    }
}
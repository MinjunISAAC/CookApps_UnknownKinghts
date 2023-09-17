// ----- C#
using InGame.ForState.ForUI;
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.SimpleFSM;
using Utiltiy.ForLoader;

namespace InGame.ForState
{
    public class State_BattleReady : SimpleState<EStateType>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Owner           _owner           = null;

        // ----- UI
        private BattleReadyView _battleReadyView = null;

        // --------------------------------------------------
        // Property
        // --------------------------------------------------
        public override EStateType State => EStateType.BattleReady;

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

            _battleReadyView = (BattleReadyView)_owner.UIOwner.GetStateUI();
            if (_battleReadyView == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] {State} View�� Null �����Դϴ�.</color>");
                return;
            }

            // Loader Hide
            Loader.Instance.Hide
            (
                () => { _battleReadyView.gameObject.SetActive(true); }
                , null
            );

            // UI Init
            _SetToUI();

            #endregion
        }
        protected override void _Update()
        {

        }

        protected override void _Finish(EStateType nextStateKey)
        {
            _battleReadyView.gameObject.SetActive(false);
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State�� ��Ż�Ͽ����ϴ�.</color>");
        }

        // ----- Private
        private void _SetToUI() 
        {
            void OnClickAction() { Loader.Instance.Show(null, () => StateMachine.Instance.ChangeState(EStateType.Village)); }
            _battleReadyView.SetToReturnButton(OnClickAction);
        }
    }
}
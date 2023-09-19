// ----- C#
using InGame.ForBattle;
using InGame.ForBattle.ForTime;
using InGame.ForCam;
using InGame.ForState.ForUI;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.SimpleFSM;
using Utiltiy.ForLoader;

namespace InGame.ForState
{
    public class State_Battle : SimpleState<EStateType>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Owner _owner = null;

        // ----- Manage
        private CamController _camController = null;

        // ----- UI
        private BattleView _battleView = null;

        // ----- Game Option
        private const float TIMESCALEUP_VALUE = 1.5f;
        private bool _isTimeScaleUp = false;

        // --------------------------------------------------
        // Property
        // --------------------------------------------------
        public override EStateType State => EStateType.Battle;

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

            _camController = _owner.CamController;
            if (_camController == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] Cam Controller�� Null �����Դϴ�.</color>");
                return;
            }

            _battleView = (BattleView)_owner.UIOwner.GetStateUI();
            if (_battleView == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] {State} View�� Null �����Դϴ�.</color>");
                return;
            }
            #endregion

            // Loader Hide
            Loader.Instance.Hide
            (
                () => { _battleView.gameObject.SetActive(true); },
                () => { }
            );

            // Battle Info Set
            var battleInfo = (BattleData)startParam;
            var stage      = battleInfo.StageInfo;
            var playTime   = stage.PlayTime;

            _camController.ChangeToCamState
            (
                CamController.ECamState.BattleStart, 0.5f, 
                () =>
                {
                    // Battle Timer Start
                    _battleView.PlayToTimer(playTime, () => { Debug.Log($"����!!"); });
                    _battleView.SetToBottomView
                    (
                        () =>
                        {
                            if (_isTimeScaleUp)
                            {
                                TimeScaler.RevertValue();
                                _battleView.VisiableToTimeScaleBtn(false);
                            }
                            else
                            {
                                TimeScaler.SetValue(TIMESCALEUP_VALUE);
                                _battleView.VisiableToTimeScaleBtn(true);
                            }

                            _isTimeScaleUp = !_isTimeScaleUp;
                        }
                    );
                }
            );

        }
        protected override void _Update()
        {
            Debug.Log($"Time Sacle = {TimeScaler.GetValue()}");
        }

        protected override void _Finish(EStateType nextStateKey)
        {
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State�� ��Ż�Ͽ����ϴ�.</color>");
        }
    }
}
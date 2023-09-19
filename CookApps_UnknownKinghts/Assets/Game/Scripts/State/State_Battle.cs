// ----- C#
using InGame.ForBattle;
using InGame.ForBattle.ForTime;
using InGame.ForCam;
using InGame.ForState.ForUI;
using InGame.ForUnit;
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
        private UnitController _unitController = null;
        private CamController  _camController  = null;

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
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State에 진입하였습니다.</color>");

            #region <Manage Group>
            _owner = Owner.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] Owner가 Null 상태입니다.</color>");
                return;
            }

            _unitController = _owner.UnitController;
            if (_unitController == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] Unit Controller가 Null 상태입니다.</color>");
                return;
            }

            _camController = _owner.CamController;
            if (_camController == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] Cam Controller가 Null 상태입니다.</color>");
                return;
            }

            _battleView = (BattleView)_owner.UIOwner.GetStateUI();
            if (_battleView == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] {State} View가 Null 상태입니다.</color>");
                return;
            }
            #endregion

            // Battle Info Set
            var battleInfo  = (BattleData)startParam;
            var stage       = battleInfo.StageInfo;
            var playTime    = stage.PlayTime;
            var playerUnits = battleInfo.PlayerUnit;
            var enemyUnits  = battleInfo.EnemyUnit;

            Debug.Log($"ENemy {enemyUnits.Count}");
            // Loader Hide
            Loader.Instance.Hide
            (
                () => 
                { 
                    _battleView.gameObject.SetActive(true);
                    _SetToUnit(playerUnits, enemyUnits);
                },
                () => 
                {
                }
            );

            _IntroToUnit(playerUnits, enemyUnits);

            // Camera Move
            _camController.ChangeToCamState
            (
                CamController.ECamState.BattleStart, 0.5f, 
                () =>
                {
                    _SetToUI      ();
                    _StartToBattle(playTime);
                }
            );
        }

        protected override void _Update()
        {
            Debug.Log($"Time Sacle = {TimeScaler.GetValue()}");
        }

        protected override void _Finish(EStateType nextStateKey)
        {
            _battleView.gameObject.SetActive(false);
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State에 이탈하였습니다.</color>");
        }

        // ----- Private
        private void _StartToBattle(float playTime)
        {
            // Battle Timer Start
            _battleView.PlayToTimer
            (
                playTime, 
                () => { Debug.Log($"끝남!!"); }
            );
        }

        private void _SetToUnit(List<Unit> playerUnitList, List<Unit> enemyUnitList)
        {
            _unitController.SetToPlayerUnit_BattleDeck(playerUnitList);
            _unitController.SetToEnemyUnit_BattleDeck(enemyUnitList);
        }

        private void _IntroToUnit(List<Unit> playerUnitList, List<Unit> enemyUnitList)
        {
            void Intro(List<Unit> list)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    var unit = list[i];
                    unit.ChangeToUnitState(Unit.EState.Intro, 1.5f);
                }
            }

            Intro(playerUnitList);
            Intro(enemyUnitList);
        }

        private void _SetToUI()
        {
            // Battle UI Init
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
    }
}
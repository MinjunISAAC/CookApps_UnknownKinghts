// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.SimpleFSM;
using Utiltiy.ForLoader;
using Utility.ForData.ForUser;
using InGame.ForState.ForUI;
using InGame.ForChapterGroup.ForChapter;
using InGame.ForChapterGroup.ForStage;
using InGame.ForChapterGroup;
using InGame.ForUnit.ForData;
using InGame.ForUnit;
using InGame.ForBattle;
using System;

namespace InGame.ForState
{
    public class State_BuildDeck : SimpleState<EStateType>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Owner         _owner            = null;

        // ----- Manage
        private UnitController _unitController  = null;

        // ----- UI
        private BuildDeckView _buildDeckView    = null;

        // ----- Battle Info
        private Chapter       _chapter          = null;
        private Stage         _stage            = null;
        private List<Unit>    _onPlayerUnitList = new List<Unit>();
        private List<Unit>    _onEnemyUnitList  = new List<Unit>();

        // --------------------------------------------------
        // Property
        // --------------------------------------------------
        public override EStateType State => EStateType.BuildDeck;

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

            _buildDeckView = (BuildDeckView)_owner.UIOwner.GetStateUI();
            if (_buildDeckView == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] {State} View가 Null 상태입니다.</color>");
                return;
            }
            #endregion

            // Loader Hide
            Loader.Instance.Hide
            (
                () => { _buildDeckView.gameObject.SetActive(true); }
                , null
            );

            // Return OnClick Action
            void OnClickAction() { Loader.Instance.Show ( null, () => StateMachine.Instance.ChangeState(EStateType.ChapterSelect)); }

            // Last Chapter Info Load
            var chapterStageInfo = (ChapterStageInfo)startParam;
            _chapter = chapterStageInfo.ChapterData;
            _stage   = chapterStageInfo.StageData;

            // User가 가지고 있는 Unit 데이터가 필요
            var ownedUnitDataList = UserDataSystem.GetToOwnedUnitDataList();
            var enemyUnitDataList = _stage.UnitList;

            _SetToReturn       (OnClickAction);
            _SetToBattleStart  ();
            _SetToBottomUI     (ownedUnitDataList);

            var enemyUnitList = _unitController.GetToEnemyUnit(enemyUnitDataList);
            _unitController.SetToEnemyUnit_BuildDeck(enemyUnitList);

            for (int i = 0; i < enemyUnitList.Count; i++)
            {
                var enemyUnit = enemyUnitList[i];
                _onEnemyUnitList.Add(enemyUnit);
            }

            _SetToUnitDeckEvent
            (
                (isInclude, type) => 
                {
                    UnitData resultUnitData = null;
                    for (int i = 0; i < ownedUnitDataList.Count; i++)
                    {
                        var searchUnitData = ownedUnitDataList[i];
                        if (searchUnitData.UnitType == type)
                        {
                            resultUnitData = searchUnitData;
                            break;
                        }
                    }

                    if (resultUnitData == null)
                        return;

                    Unit targetUnit = null;

                    if (!isInclude)
                    {
                        targetUnit = _unitController.GetToPlayerUnit(resultUnitData);
                        _onPlayerUnitList.Add(targetUnit);
                    }
                    else
                    {
                        targetUnit = _unitController.GetToPlayerUnit(resultUnitData);
                        _onPlayerUnitList.Remove(targetUnit);
                        _unitController.ReturnToPlayerUnit(resultUnitData);
                    }
                    
                    _unitController.SetToPlayerUnit_BuildDeck(_onPlayerUnitList);
                }
            );
        }

        protected override void _Update()
        {

        }

        protected override void _Finish(EStateType nextStateKey)
        {
            _buildDeckView           .ResetToBuildDeckView();
            _buildDeckView.gameObject.SetActive(false);

            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State에 이탈하였습니다.</color>");
        }

        // ----- Private
        private void _SetToBottomUI(List<UnitData> ownedUnitDataList)
        => _buildDeckView.SetToBuildDeck_UI(ownedUnitDataList);

        private void _SetToReturn(Action onClickReturn)
        => _buildDeckView.SetToReturnButton(onClickReturn);

        private void _SetToUnitDeckEvent(Action<bool, EUnitType> onClickEvent)
        => _buildDeckView.SetToBuildDeck_OnClickEvent(onClickEvent);

        private void _SetToBattleStart()
        {
            BattleData battleData = new BattleData(_chapter, _stage, _onPlayerUnitList, _onEnemyUnitList);
            _buildDeckView.SetToBattleStart
            (
                () =>
                {

                    Loader.Instance.Show
                    (
                        null,
                        () => StateMachine.Instance.ChangeState(EStateType.Battle, battleData),
                        1f
                    );
                }
            );
        }

        /*
        public void _SetToUI(List<UnitData> ownedUnitDataList)
        {
            BattleData battleData = new BattleData(_onPlayerUnitList, _onEnemyUnitList);
            void OnClickAction()
            {
                Loader.Instance.Show
                (
                    null,
                    () => StateMachine.Instance.ChangeState(EStateType.ChapterSelect)
                );
            }
            _buildDeckView.SetToReturnButton(OnClickAction);
            _buildDeckView.SetToBattleStart (() => { StateMachine.Instance.ChangeState(EStateType.Battle, battleData);});
            _buildDeckView.SetToBuildDeckUI (ownedUnitDataList);
        }
        private void _SetToUI(Chapter chapterData, Stage stageData, List<UnitData> ownedUnitData)
        {

            _buildDeckView.SetToStageInfo(chapterData.Name, chapterData.Step, stageData.StageStep);
            
            _buildDeckView.SetToBottomView
            (
                ownedUnitData,
                (isInclude, type) => 
                {
                    _unitController.RefreshToPlayerUnitDeck(isInclude, type);
                    _unitController.SetToBuildDeck();
                }
            );
        }

        private void _SetToDeckUnit(List<UnitData> ownedUnitDataList, List<UnitData> enemyUnitDataList)
        {
            var unitList = new List<Unit>();
            
            for (int i = 0; i < ownedUnitDataList.Count; i++)
            {
                var unitData   = ownedUnitDataList[i];
                var unitPrefab = _unitController.GetToPlayerUnit(unitData);

                unitList.Add(unitPrefab);
            }

            var enemyUnitList = _unitController.GetToEnemyUnit(enemyUnitDataList);
            _unitController.SetToBuildDeck(unitList, enemyUnitList);
        }
        */
    }
}
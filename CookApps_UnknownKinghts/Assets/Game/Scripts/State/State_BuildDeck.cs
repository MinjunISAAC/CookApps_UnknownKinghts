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

namespace InGame.ForState
{
    public class State_BuildDeck : SimpleState<EStateType>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Owner         _owner           = null;

        // ----- Manage
        private UnitController _unitController = null;

        // ----- UI
        private BuildDeckView _buildDeckView   = null;

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

            // Last Chapter Info Load
            var chapterStageInfo = (ChapterStageInfo)startParam;
            var chapterData      = chapterStageInfo.ChapterData;
            var stageData        = chapterStageInfo.StageData;

            // User가 가지고 있는 Unit 데이터가 필요
            var ownedUnitDataList = UserDataSystem.GetToOwnedUnitDataList();
            var enemyUnitDataList = stageData.UnitList;

            // UI Init
            _SetToUI(chapterData, stageData, ownedUnitDataList);

            // Build Deck에 맞는 Unit 생성 및 포지션 수정
            _SetToDeckUnit(ownedUnitDataList, enemyUnitDataList);
        }

        protected override void _Update()
        {

        }

        protected override void _Finish(EStateType nextStateKey)
        {
            _unitController.ResetToUnit();
            _buildDeckView.gameObject.SetActive(false);
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State에 이탈하였습니다.</color>");
        }

        // ----- Private
        private void _SetToUI(Chapter chapterData, Stage stageData, List<UnitData> ownedUnitData)
        {
            void OnClickAction()
            {
                Loader.Instance.Show
                (
                    null,
                    () => StateMachine.Instance.ChangeState(EStateType.ChapterSelect)
                );
            }

            _buildDeckView.SetToReturnButton(OnClickAction);
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
    }
}
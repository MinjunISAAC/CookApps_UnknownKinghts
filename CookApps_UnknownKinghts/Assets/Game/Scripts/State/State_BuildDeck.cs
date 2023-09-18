// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.SimpleFSM;
using Utiltiy.ForLoader;
using InGame.ForState.ForUI;
using Utility.ForData.ForUser;
using InGame.ForChapterGroup.ForChapter;
using InGame.ForChapterGroup.ForStage;
using InGame.ForChapterGroup;
using InGame.ForUnit.ForData;

namespace InGame.ForState
{
    public class State_BuildDeck : SimpleState<EStateType>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Owner         _owner         = null;

        // ----- UI
        private BuildDeckView _buildDeckView = null;

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
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State�� �����Ͽ����ϴ�.</color>");

            #region <Manage Group>
            _owner = Owner.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] Owner�� Null �����Դϴ�.</color>");
                return;
            }

            _buildDeckView = (BuildDeckView)_owner.UIOwner.GetStateUI();
            if (_buildDeckView == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] {State} View�� Null �����Դϴ�.</color>");
                return;
            }
            #endregion

            // Loader Hide
            Loader.Instance.Hide
            (
                () =>
                {
                    _buildDeckView.gameObject.SetActive(true);
                }
                , null
            );

            // Last Chapter Info Load
            var chapterStageInfo = (ChapterStageInfo)startParam;
            var chapterData      = chapterStageInfo.ChapterData;
            var stageData        = chapterStageInfo.StageData;

            // User �� ������ �ִ� Unit �����Ͱ� �ʿ�
            var ownedUnitDataList = UserDataSystem.GetToOwnedUnitDataList();

            Debug.Log($"Owned Unit Data List {ownedUnitDataList.Count}");

            // UI Init
            _SetToUI(chapterData, stageData, ownedUnitDataList);
        }

        protected override void _Update()
        {

        }

        protected override void _Finish(EStateType nextStateKey)
        {
            _buildDeckView.gameObject.SetActive(false);
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State�� ��Ż�Ͽ����ϴ�.</color>");
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
            _buildDeckView.SetToBottomView(ownedUnitData);
        }
    }
}
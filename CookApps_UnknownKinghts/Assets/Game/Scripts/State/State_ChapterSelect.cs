// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.SimpleFSM;
using Utiltiy.ForLoader;
using Utility.ForData.ForUser;
using InGame.ForState.ForChapterSelect;
using InGame.ForChapterGroup.ForChapter;

namespace InGame.ForState
{
    public class State_ChapterSelect : SimpleState<EStateType>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Owner           _owner           = null;

        // ----- UI
        private ChapterSelectView _chapterSelectView = null;

        // --------------------------------------------------
        // Property
        // --------------------------------------------------
        public override EStateType State => EStateType.ChapterSelect;

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

            _chapterSelectView = (ChapterSelectView)_owner.UIOwner.GetStateUI();
            if (_chapterSelectView == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] {State} View가 Null 상태입니다.</color>");
                return;
            }
            #endregion

            // Loader Hide
            Loader.Instance.Hide
            (
                () => 
                { 
                    _chapterSelectView.gameObject.SetActive(true);
                    _chapterSelectView.ResetToMap();
                }
                , null
            );

            // Last Chapter Info Load
            var userLastChapterStep = UserDataSystem.GetToLastChapter  ();
            var userLastStageStep   = UserDataSystem.GetToLastStage    ();
            var userClearData       = UserDataSystem.GetToClearDataList(userLastChapterStep);
            var chapterData         = _owner.GetToChapter(userLastChapterStep);
            var stageData           = _owner.GetToStage  (userLastChapterStep, userLastStageStep);

            // UI Init
            _SetToUI(chapterData, userClearData, userLastStageStep);

        }
        protected override void _Update()
        {

        }

        protected override void _Finish(EStateType nextStateKey)
        {
            _chapterSelectView.gameObject.SetActive(false);
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State에 이탈하였습니다.</color>");
        }

        // ----- Private
        private void _SetToUI(Chapter targetChapterData, List<UserData.ClearData> chapterClearDataList, int userLastStageStep)
        {
            void OnClickAction() 
            { 
                Loader.Instance.Show
                (
                    null, 
                    () => StateMachine.Instance.ChangeState(EStateType.Village)
                );
            }

            _chapterSelectView.SetToReturnButton    (OnClickAction);
            _chapterSelectView.SetToChapterInfoView (targetChapterData);
            _chapterSelectView.SetToChapterGroupView(chapterClearDataList, targetChapterData.Step);
            _chapterSelectView.MoveToMap            (userLastStageStep);
        }
    }
}
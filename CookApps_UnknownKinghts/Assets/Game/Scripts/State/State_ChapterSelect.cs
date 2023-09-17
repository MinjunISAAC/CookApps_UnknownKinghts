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
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State�� �����Ͽ����ϴ�.</color>");

            #region <Manage Group>
            _owner = Owner.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] Owner�� Null �����Դϴ�.</color>");
                return;
            }

            _chapterSelectView = (ChapterSelectView)_owner.UIOwner.GetStateUI();
            if (_chapterSelectView == null)
            {
                Debug.LogError($"<color=red>[State_{State}._Start] {State} View�� Null �����Դϴ�.</color>");
                return;
            }

            // Last Chapter Info Load
            var userLastChapterStep = UserDataSystem.GetToLastChapter();
            var userLastStageStep   = UserDataSystem.GetToLastStage  ();
            var chapter             = _owner.GetToChapter(userLastChapterStep);
            var stage               = _owner.GetToStage  (userLastChapterStep, userLastStageStep);
            
            // Loader Hide
            Loader.Instance.Hide
            (
                () => { _chapterSelectView.gameObject.SetActive(true); }
                , null
            );

            // UI Init
            _SetToUI(userLastChapterStep, chapter.Name);

            #endregion
        }
        protected override void _Update()
        {

        }

        protected override void _Finish(EStateType nextStateKey)
        {
            _chapterSelectView.gameObject.SetActive(false);
            Debug.Log($"<color=yellow>[State_{State}._Start] {State} State�� ��Ż�Ͽ����ϴ�.</color>");
        }

        // ----- Private
        private void _SetToUI(int chapterStep, string chapterName)
        {
            void OnClickAction() 
            { 
                Loader.Instance.Show
                (
                    null, 
                    () => StateMachine.Instance.ChangeState(EStateType.Village)
                );
            }

            _chapterSelectView.SetToReturnButton(OnClickAction);
            _chapterSelectView.SetToChapterInfoView(chapterStep, chapterName);
        }
    }
}
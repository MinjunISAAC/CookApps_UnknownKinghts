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
using InGame.ForItem.ForReward;
using InGame.ForChapterGroup.ForStage;

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

        // ----- Target Group
        private List<UserData.ClearData> _userClearData     = new List<UserData.ClearData>();
        private int                      _targetChapterStep = 0;
        private int                      _targetStageStep   = 0;

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
                _userClearData      = UserDataSystem.GetToClearDataList(userLastChapterStep);
            var chapterData         = _owner.GetToChapter(userLastChapterStep);
            var stageData           = _owner.GetToStage  (userLastChapterStep, userLastStageStep);

            _targetChapterStep = userLastChapterStep;
            _targetStageStep   = userLastStageStep;
            
            // Reward Item Data Init
            var rewardItemDatas = stageData.RewardList;

            // UI Init
            _SetToUI(chapterData, stageData, _userClearData, rewardItemDatas, userLastStageStep);

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
        private void _SetToUI(Chapter targetChapterData, Stage targetStageData, List<UserData.ClearData> chapterClearDataList, List<RewardItemData> rewardItemList, int userLastStageStep)
        {
            void OnClickAction() 
            { 
                Loader.Instance.Show
                (
                    null, 
                    () => StateMachine.Instance.ChangeState(EStateType.Village)
                );
            }

            var targetChapterClearData = UserDataSystem.GetToClearData(_targetChapterStep, _targetStageStep);

            _chapterSelectView.SetToReturnButton     (OnClickAction);
            _chapterSelectView.SetToChapterInfoView  (targetChapterData);
            _chapterSelectView.SetToChapterGroupView (chapterClearDataList, targetChapterData.Step);
            _chapterSelectView.MoveToMap             (userLastStageStep);
            _chapterSelectView.OnInitToChapterRewardView(_PrevStage, _NextStage, targetChapterData, targetStageData, targetChapterClearData, rewardItemList);
        }

        private void _PrevStage()
        {
            var userLastChapterStep = UserDataSystem.GetToLastChapter();
            var userLastStageStep   = UserDataSystem.GetToLastStage  ();
            var chapterData         = _owner.GetToChapter(userLastChapterStep);
            var stageData           = _owner.GetToStage(userLastChapterStep, userLastStageStep);
            var rewardItemList      = stageData.RewardList;
            
            if (_targetStageStep > 1)
            {
                _targetStageStep--;

                var currStageData = _owner.GetToStage(userLastChapterStep, _targetStageStep);
                var currClearData = UserDataSystem.GetToClearData(userLastChapterStep, currStageData.Step);
                
                _chapterSelectView.RefreshChapterRewardView(chapterData, currStageData, currClearData, rewardItemList);
            }
        }

        private void _NextStage()
        {
            var userLastChapterStep = UserDataSystem.GetToLastChapter();
            var userLastStageStep   = UserDataSystem.GetToLastStage();
            var chapterData         = _owner.GetToChapter(userLastChapterStep);
            var stageData           = _owner.GetToStage(userLastChapterStep, userLastStageStep);
            var rewardItemList      = stageData.RewardList;
            var stageQuantity       = chapterData.StageQuantity;

            if (_targetStageStep < stageQuantity )
            {
                _targetStageStep++;

                var currStageData = _owner.GetToStage(userLastChapterStep, _targetStageStep);
                var currClearData = UserDataSystem.GetToClearData(userLastChapterStep, currStageData.Step);

                _chapterSelectView.RefreshChapterRewardView(chapterData, currStageData, currClearData, rewardItemList);
            }
        }
    }
}
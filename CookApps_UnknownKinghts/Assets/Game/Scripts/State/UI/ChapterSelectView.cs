// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;

// ----- User Defined
using Utility.ForData.ForUser;
using InGame.ForState.ForUI;
using InGame.ForChapterGroup.ForChapter;
using InGame.ForMap;
using InGame.ForItem.ForReward;
using InGame.ForChapterGroup.ForStage;

namespace InGame.ForState.ForChapterSelect
{
    public class ChapterSelectView : StateView
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. UI Group")]
        [SerializeField] private Button            _BTN_Return        = null;
        [SerializeField] private ChapterInfoView   _chapterInfoView   = null;
        [SerializeField] private ChapterGroupView  _chapterGroupView  = null;
        [SerializeField] private ChapterRewardView _chapterRewardView = null;
        
        [Header("2. Controller")]
        [SerializeField] private MapMoveController _mapMoveController = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public(Override)
        public override void OnInit()   { }
        public override void OnFinish() { }

        // ----- public
        public void SetToReturnButton(Action onClickReturnBtn)
        => _BTN_Return.onClick.AddListener(() => { onClickReturnBtn(); } );
        public void SetToChapterGroupView(List<UserData.ClearData> userClearDataList, int chapterStep)
        => _chapterGroupView.SetToChapterItems(userClearDataList, chapterStep);

        public void SetToChapterInfoView(Chapter targetChapterData)
        {
            var chapterStep = targetChapterData.Step; 
            var chapterName = targetChapterData.Name;
            
            _chapterInfoView.SetToChapterInfo(chapterStep, chapterName);
        }

        public void OnInitToChapterRewardView(Action onClickPrevStage, Action onClickNextStage, Chapter targetchapter, Stage targetStage, UserData.ClearData clearData, List<RewardItemData> rewardList)
        {
            _chapterRewardView.OnInit          (targetchapter   , targetStage, clearData, rewardList);
            _chapterRewardView.SetToButtonEvent(onClickPrevStage, onClickNextStage);
        }

        public void RefreshChapterRewardView(Chapter targetchapter, Stage targetStage, UserData.ClearData clearData, List<RewardItemData> rewardList)
        {
            _chapterRewardView.OnInit          (targetchapter, targetStage, clearData, rewardList);
            _chapterRewardView.SetToStageText(targetchapter.Step, targetStage.Step);
        }

        public void MoveToMap(int lastStageStep) 
        {
            var pos = _chapterGroupView.GetToTargetChapterItemPos(lastStageStep);
            _mapMoveController.MoveToMap(pos);
        } 

        public void MoveToMap(int stageStep, float duration, Action doneCallBack = null)
        {
            var pos = _chapterGroupView.GetToTargetChapterItemPos(stageStep);
            _mapMoveController.MoveToMap(pos, duration, doneCallBack);
        }

        public void ResetToMap() => _mapMoveController.ResetToMap();
    }
}
// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;

// ----- User Defined
using InGame.ForState.ForUI;
using InGame.ForChapterGroup.ForChapter;
using Utility.ForData.ForUser;

namespace InGame.ForState.ForChapterSelect
{
    public class ChapterSelectView : StateView
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. UI Group")]
        [SerializeField] private Button            _BTN_Return       = null;
        [SerializeField] private ChapterInfoView   _chapterInfoView  = null;
        [SerializeField] private ChapterGroupView  _chapterGroupView = null;

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

        public void SetToChapterInfoView(Chapter targetChapterData)
        {
            var chapterStep = targetChapterData.Step; 
            var chapterName = targetChapterData.Name;
            
            _chapterInfoView.SetToChapterInfo(chapterStep, chapterName);
        }

        public void SetToChapterGroupView(List<UserData.ClearData> userClearDataList, int chapterStep)
        {
            _chapterGroupView.SetToChapterItems(userClearDataList, chapterStep);
        }
    }
}
// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;

// ----- User Defined
using InGame.ForState.ForUI;

namespace InGame.ForState.ForChapterSelect
{
    public class ChapterSelectView : StateView
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("1. UI Group")]
        [SerializeField] private Button            _BTN_Return      = null;
        [SerializeField] private ChapterInfoView   _chapterInfoView = null;

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

        public void SetToChapterInfoView(int chapter, string chapterName)
        => _chapterInfoView.SetToChapterInfo(chapter, chapterName);
    }
}
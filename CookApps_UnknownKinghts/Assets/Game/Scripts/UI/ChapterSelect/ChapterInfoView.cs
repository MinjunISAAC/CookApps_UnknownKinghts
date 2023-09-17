using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace InGame.ForState.ForChapterSelect
{
    public class ChapterInfoView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private TextMeshProUGUI _TMP_ChapterCount = null;
        [SerializeField] private TextMeshProUGUI _TMP_ChapterName  = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToChapterInfo(int chapter, string chapterName)
        {
            _TMP_ChapterCount.text = $"Chapter {chapter}";
            _TMP_ChapterName .text = chapterName;    
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utility.ForData.ForUser;

namespace InGame.ForState.ForChapterSelect
{
    public class ChapterGroupView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private List<ChapterItem> _chapterItem = new List<ChapterItem>();

        // --------------------------------------------------
        // Functinos - Nomal
        // --------------------------------------------------
        public void SetToChapterItems(List<UserData.ClearData> userClearData, int chapterStep)
        {
            for (int i = 0; i < _chapterItem.Count; i++)
            {
                var chapterItem = _chapterItem[i];
                chapterItem.SetToChapterInfo(chapterStep, i + 1);
                
                if (i < userClearData.Count)
                {
                    var clearData   = userClearData[i];
                    if (clearData != null) chapterItem.SetToClearStar(clearData.ClearStar);
                }
                else chapterItem.SetToClearStar(0);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.ForState.ForChapterSelect
{
    public class ChapterItem : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private RectTransform   _RECT_This     = null;
        [SerializeField] private TextMeshProUGUI _TMP_Count     = null;
        [SerializeField] private List<Image>     _IMG_StarGroup = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public RectTransform RectTrans => _RECT_This;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToChapterInfo(int chapter, int stage)
        => _TMP_Count.text = $"{chapter}-{stage}";

        public void SetToClearStar(int clearStar)
        {
            for (int i = 0; i < clearStar; i++)
            {
                var starImg = _IMG_StarGroup[i];
                starImg.gameObject.SetActive(true);
            }

            for (int i = clearStar; i < _IMG_StarGroup.Count; i++)
            {
                var starImg = _IMG_StarGroup[i];
                starImg.gameObject.SetActive(false);
            }
        }
    }
}
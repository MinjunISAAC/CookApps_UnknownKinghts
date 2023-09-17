using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForChapterGroup
{
    [CreateAssetMenu(menuName = "Chapter Group/Create To Chapter Group", fileName = "ChapterGroup")]
    public class ChapterGroup : ScriptableObject
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private List<Chapter> _chapterList = new List<Chapter>();

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Dictionary<int, Chapter> _chapterSet = new Dictionary<int, Chapter>();

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void OnInit()
        {
            for (int i = 0; i < _chapterList.Count; i++)
            {
                var chapter     = _chapterList[i];
                var chapterStep = chapter.Step;
                chapter.OnInit();
                
                _chapterSet.Add(chapterStep, chapter);
            }

            // [TODO] Test 종료 시, 주석처리 로직
            Debug.Log($"<color=yellow>[ChaterGroup.OnInit] 현재 게임은 {_chapterSet.Count}개의 Chapter로 초기화되었습니다.</color>");
        }
    }
}
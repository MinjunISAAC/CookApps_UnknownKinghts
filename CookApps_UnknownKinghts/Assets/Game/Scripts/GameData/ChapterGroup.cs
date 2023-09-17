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

            // [TODO] Test ���� ��, �ּ�ó�� ����
            Debug.Log($"<color=yellow>[ChaterGroup.OnInit] ���� ������ {_chapterSet.Count}���� Chapter�� �ʱ�ȭ�Ǿ����ϴ�.</color>");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.ForChapterGroup
{
    [CreateAssetMenu(menuName = "Chapter Group/Create To Chapter", fileName = "Chapter")]
    public class Chapter : ScriptableObject
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Chapter Info")]
        [SerializeField] private string      _name = "";
        [SerializeField] private int         _step = 0;

        [Header("Stage Group")]
        [SerializeField] private List<Stage> _stageList = new List<Stage>();

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Dictionary<int, Stage> _stageSet = new Dictionary<int, Stage>();

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public int Step => _step;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void OnInit()
        {
            for (int i = 0; i < _stageList.Count; i++)
            {
                var stage     = _stageList[i];
                var stageStep = stage.Step;

                _stageSet.Add(stageStep, stage);
            }

            // [TODO] Test 종료 시, 주석처리 로직
            Debug.Log($"<color=yellow>[Chater.OnInit] Chapter {_step}은 총 {_stageSet.Count}의 Stage로 구성되어있으며, 초기화 되었습니다.</color>");
        }
    }
}
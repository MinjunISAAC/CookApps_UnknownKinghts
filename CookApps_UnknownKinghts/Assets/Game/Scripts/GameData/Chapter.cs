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

            // [TODO] Test ���� ��, �ּ�ó�� ����
            Debug.Log($"<color=yellow>[Chater.OnInit] Chapter {_step}�� �� {_stageSet.Count}�� Stage�� �����Ǿ�������, �ʱ�ȭ �Ǿ����ϴ�.</color>");
        }
    }
}
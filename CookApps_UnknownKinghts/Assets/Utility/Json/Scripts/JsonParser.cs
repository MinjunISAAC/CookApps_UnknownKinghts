// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using CoreData.ForLevel;

namespace JsonUtil
{
    public static class JsonParser
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Const
        private const string JSONFILE_NAME = "LevelSheetData";

        // ----- Private
        private static LevelSheetDatas _dataSet = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public static void LoadJson()
        {
            var loadedJsonFile = Resources.Load<TextAsset>($"{JSONFILE_NAME}");

            if (loadedJsonFile == null)
            {
                Debug.LogError("<color=red>[JsonParser.LoadJson] Json File이 존재하지 않습니다.</color>");
                return;
            }

            _dataSet = JsonUtility.FromJson<LevelSheetDatas>(loadedJsonFile.text);

            if (_dataSet == null)
            {
                Debug.LogError("<color=red>[JsonParser.LoadJson] JSON 파싱에 실패하였습니다.</color>");
                return;
            }
        }

        public static List<LevelSheetData> GetLevelDataSet()
        {
            if (_dataSet == null)
                return null;

            return _dataSet.DataSet;
        }
    }
}
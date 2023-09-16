using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreData.ForLevel
{
    [System.Serializable]
    public class LevelSheetData
    {
        public int Level;
        public int Exp;
        public int MaxHealth;
    }

    [System.Serializable]
    public class LevelSheetDatas
    {
        public List<LevelSheetData> DataSet = new List<LevelSheetData>();
    }
}
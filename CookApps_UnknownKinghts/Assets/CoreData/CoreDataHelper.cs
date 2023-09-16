// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using CoreData.ForLevel;
using JsonUtil;

namespace CoreData
{
    public static class CoreDataHelper
    {
        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public static int GetToLevelUpExp(int level)
        {
            var dataSet = JsonParser.GetLevelDataSet();
            var levelUpExp = 0;

            for (int i = 0; i < dataSet.Count; i++)
            {
                var data = dataSet[i];

                if (data.Level == level)
                {
                    levelUpExp = data.Exp;
                    break;
                }
            }

            if (levelUpExp == 0)
            {
                Debug.LogError($"<color=red>[CoreDataHelper.GetToLevelUpExp] {level}에 대한 경험치 데이터가 존재하지 않습니다.</color>");
                return -1;
            }

            return levelUpExp;
        }
        public static int GetToMaxHealth(int level)
        {
            var dataSet = JsonParser.GetLevelDataSet();
            var maxHealth = 0;

            for (int i = 0; i < dataSet.Count; i++)
            {
                var data = dataSet[i];

                if (data.Level == level)
                {
                    maxHealth = data.MaxHealth;
                    break;
                }
            }

            if (maxHealth == 0)
            {
                Debug.LogError($"<color=red>[CoreDataHelper.GetToLevelUpExp] {level}에 대한 경험치 데이터가 존재하지 않습니다.</color>");
                return -1;
            }

            return maxHealth;
        }
    }
}
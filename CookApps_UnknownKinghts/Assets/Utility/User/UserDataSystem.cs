// ----- C#
using System;
using System.IO;
using System.Text;

// ----- Unity
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Utility.ForData.ForUser
{
    public static class UserDataSystem
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Const
        private const string FILE_NAME = "UserData.json";

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public static UserData UserData
        {
            get;
            private set;
        } = new UserData();

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public static void CreateToUserSaveData(bool ignoreSaveData = false)
        {
            UserData = new UserData();

            if (!ignoreSaveData)
                Save();
        }

        public static int    GetToLevel       () => UserData.Level;
        public static int    GetToExp         () => UserData.Experience;
        public static string GetToUserName    () => UserData.UserName;

        public static int    GetToCoin        () => UserData.CurrencyCoin;
        public static int    GetToGem         () => UserData.CurrencyGem;
        public static int    GetToHealth      () => UserData.CurrencyHealth;

        public static int    GetToLastChapter () => UserData.LastChapter;
        public static int    GetToLastStage   () => UserData.LastStage;

        public static int TestData() => UserData.ClearDataList.Count;
        public static UserData.ClearData GetToClearData(int chapter, int stage)
        {
            List<UserData.ClearData> dataSet   = UserData.ClearDataList;
            UserData.ClearData       clearData = null; 

            for (int i = 0; i < dataSet.Count; i++)
            {
                var data = dataSet[i];
                if (data.Chapter == chapter && data.Stage == stage)
                {
                    clearData = data;
                    Debug.Log($"���� 1? {clearData}");
                    return clearData;
                }
            }

            Debug.Log($"���� 2?");
            return null;
        }

        public static void SetToClearData(int chapter, int stage, int clearStar)
        {
            var data = GetToClearData(chapter, stage);

            if (data != null)
            {
                if (data.ClearStar < clearStar)
                {
                    var clearData = new UserData.ClearData(chapter, stage, clearStar);
                
                    UserData.ClearDataList.Remove(data);
                    UserData.ClearDataList.Add(clearData);
                }
            }
            else
            {
                var clearData = new UserData.ClearData(chapter, stage, clearStar);
                UserData.ClearDataList.Add(clearData);
            }

            Save();
        }

        public static void Load()
        {
            if (!_TryLoad(FILE_NAME, out string fileContents))
            {
                UserData = new UserData();
                return;
            }
            
            try
            {
                var pendData = JsonUtility.FromJson<UserData>(fileContents);
                if (pendData == null)
                {
                    Debug.LogError($"<color=red>[UserSystem.Load] {FILE_NAME} ������ �ε��ϴµ� �����߽��ϴ�.</color>");
                    return;
                }

                UserData = pendData;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return;
            }

            return;
        }

        public static void Save()
        {
            var jsonContents = JsonUtility.ToJson(UserData);

            if (!_TrySave(FILE_NAME, jsonContents))
            {
                Debug.LogError($"<color=red>[UserDataSystem.Save] File�� �������� ���߽��ϴ�.</color>");
                return;
            }
        }

        // ----- Private
        private static bool _TryLoad(string fileName, out string fileContents)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileContents = string.Empty;
                return false;
            }

            var filePath = $"{Application.persistentDataPath}/{fileName}";

            if (!File.Exists(filePath))
            {
                fileContents = string.Empty;
                return false;
            }

            try
            {
                fileContents = File.ReadAllText(filePath);
                return true;
            }
            catch (Exception e)
            {
                fileContents = $"{e}";
                return false;
            }
        }
        private static bool _TrySave(string fileName, string saveDataContents, bool useEncodeFileName = true, bool useEncodeData = true)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                Debug.LogError($"<color=red>UserDataSystem.Save] ���ϸ��� ����ֽ��ϴ�.</color>");
                return false;
            }

            if (UserData == null)
            {
                Debug.LogError($"<color=red>[UserDataSystem.Save] User Save Data�� �������� �ʾҽ��ϴ�.</color>");
                return false;
            }

            if (string.IsNullOrEmpty(saveDataContents))
            {
                Debug.LogWarning($"<color=red>[UserDataSystem.Save] ������ �������� ����ֽ��ϴ�.</color>");
                return false;
            }

            try
            {
                var fileContents = JsonUtility.ToJson(UserData);

                var filePath = $"{Application.persistentDataPath}/{fileName}";

                try
                {
                    fileContents = saveDataContents;
                    File.WriteAllText(filePath, fileContents);

                    return true;
                }
                catch (Exception e)
                {
                    Debug.LogError($"<color=red>[UserSaveDataManager._TrySave] {e}</color>");
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.Log($"<color=red>[UserSaveDataManager._TrySave] {e}</color>");
                return false;
            }
        }

#if UNITY_EDITOR
        [MenuItem("UserData/Delete User Data")]
        private static void ClearUserSaveData()
        {
            string filePath = $"{Application.persistentDataPath}/{FILE_NAME}";

            if (File.Exists(filePath)) File.Delete(filePath);
        }
#endif
    }
}
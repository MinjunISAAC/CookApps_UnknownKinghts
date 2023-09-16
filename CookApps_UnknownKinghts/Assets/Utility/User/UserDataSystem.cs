// ----- C#
using System;
using System.IO;
using System.Text;

// ----- Unity
using UnityEngine;
using UnityEditor;

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

        public static int    GetToLevel   () => UserData.Level;
        public static int    GetToExp     () => UserData.Experience;
        public static string GetToUserName() => UserData.UserName;

        public static int GetToCoin   () => UserData.CurrencyCoin;
        public static int GetToGem    () => UserData.CurrencyGem;
        public static int GetToHealth () => UserData.CurrencyHealth;

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
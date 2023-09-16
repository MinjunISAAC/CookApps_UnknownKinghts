// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUI;
using InGame.ForState;
using Utility.ForCurrency;
using Utility.ForData.ForUser;
using JsonUtil;

namespace InGame
{
    public class Owner : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("0. UI Group")]
        [SerializeField] private UIOwner _uiOwner = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public static Owner NullableInstance
        {
            get;
            private set;
        } = null;

        public UIOwner UIOwner => _uiOwner;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Awake() { NullableInstance = this; }

        private IEnumerator Start()
        {
            // [TODO] �⺻ ���ÿ� �ʿ��� ���� ����
            // 1. Json Fil Load
            // 2. User Data Load
            // 3. Currency System Init
            JsonParser             .LoadJson();
            UserDataSystem         .Load  ();
            CurrencySystem.Instance.OnInit();









            // State �ʱ�ȭ
            StateMachine.Instance.ChangeState(EStateType.Village);
           
            yield return null;
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Private
        
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InGame.ForState.ForBuildDeck
{
    public class UnitDeckInfo : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private Image           _IMG_SpecIcon = null;
        [SerializeField] private Image           _IMG_JobIcon  = null;
        [SerializeField] private TextMeshProUGUI _TMP_Name     = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToInfo(Sprite specIcon, Sprite jobIcon, string name)
        {
            _IMG_SpecIcon.sprite = specIcon;
            _IMG_JobIcon .sprite = jobIcon;    
            _TMP_Name    .text   = name;
        }
    }
}
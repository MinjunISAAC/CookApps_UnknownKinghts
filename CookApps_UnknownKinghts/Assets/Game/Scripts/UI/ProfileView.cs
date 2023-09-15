// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace InGame.ForUI
{
    public class ProfileView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Image Group")]
        [SerializeField] private Image           _IMG_UserPhoto = null;
        [SerializeField] private Image           _IMG_ExpGuage  = null;

        [Header("Text Group")]
        [SerializeField] private TextMeshProUGUI _TMP_UserLevel = null;
        [SerializeField] private TextMeshProUGUI _TMP_UserId    = null;
        [SerializeField] private TextMeshProUGUI _TMP_Exp       = null;
    }
}
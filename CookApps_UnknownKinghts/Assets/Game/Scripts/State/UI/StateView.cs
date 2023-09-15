// ----- Unity
using UnityEngine;

namespace InGame.ForState.ForUI
{
    public abstract class StateView : MonoBehaviour
    {
        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public abstract void OnInit  ();
        public abstract void OnFinish();
    }
}
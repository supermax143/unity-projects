using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Knights
{
    public class GUIManager : MonoBehaviour
    {
        public delegate void TriggerEvent();
        public event TriggerEvent TriggerStart;
        public event TriggerEvent TriggerReset;

        private Scrollbar mPlayerBar;
        private Scrollbar mEnemyBar;

        void Awake()
        {
            InitBars();
        }

        void InitBars()
        {
            mPlayerBar = transform.Find("PlayerBar").GetComponentInChildren<Scrollbar>();
            mEnemyBar = transform.Find("EnemyBar").GetComponentInChildren<Scrollbar>();
        }

        public void SetPlayerHealth(float curValue, float maxValue)
        {
            mPlayerBar.size = curValue / maxValue;
        }

        public void SetEnemyHealth(float curValue, float maxValue)
        {
            mPlayerBar.size = curValue / maxValue;
            mEnemyBar.size = curValue / maxValue;
        }

        public void StartHandler()
        {
            TriggerStart();
        }

        public void ResetHandler()
        {
            TriggerReset();
        }
    }
}


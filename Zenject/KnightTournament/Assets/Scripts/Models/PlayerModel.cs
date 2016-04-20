using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class PlayerModel
    {

        public delegate void UpdateEventDelegate(PlayerModel playerModel);
        public event UpdateEventDelegate updateEvent;

        public float healthCurrent;
        public float healthMax;
        public PlayerModel() {
            healthMax = healthCurrent = 300;
        }

        /// <summary>
        ////////////////////////////////////////////
        //  getters setters
        /// </summary>
        public float HealthMax
        {
            get { return healthMax; }
        }
        public void UpdateHealth(float value)
        {
            healthCurrent = value;
            updateEvent(this);
        }


    }
}

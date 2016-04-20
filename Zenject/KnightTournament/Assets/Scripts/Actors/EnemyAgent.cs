using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Knights
{
    class EnemyAgent: ActorAgent
    {

        void Awake()
        {
            Debug.Log("EnemyAgent:Awake");
            Init();
        }

    }
}

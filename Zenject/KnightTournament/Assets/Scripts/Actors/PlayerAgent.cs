using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Knights
{
    class PlayerAgent: ActorAgent
    {

        void Awake()
        {
            Debug.Log("PlayerAgent:Awake");
            Init();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.SceneManagement;
using Zenject;

namespace Knights
{
    class GameController : IInitializable,ITickable
    {
        enum State { Game }

        private State mState;
        private GUIManager mGUI;
        private RoundController mRoundController;
        public GameController(RoundController roundController,GUIManager GUI)
        {
            mRoundController = roundController;
            mGUI = GUI;
            mGUI.TriggerStart += OnStartClick;
            mGUI.TriggerReset += OnResetClick;
            mState = State.Game;
        }

        private void OnResetClick()
        {
            SceneManager.LoadScene("Level_1");
        }

        private void OnStartClick()
        {
            mRoundController.Start();
        }


        public void Initialize()
        {

        }

        public void Tick()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Zenject;

namespace Knights
{
    class RoundController:IInitializable,ITickable
    {
        enum State { Idle, Acceleration, SlowMotion, WaitHit };

        private IActorAgent mPlayer;
        private IActorAgent mEnemy;
        private LevelHelper mLevel;

        private State mState;

        public RoundController(PlayerAgent player, EnemyAgent enemy, LevelHelper level)
        {
            mPlayer = player;
            mEnemy = enemy;
            mLevel = level;
        }


        private float CameraWidth
        {
            get 
            {
                return mLevel.ScreenWidth;
            }
        }

        public void SetTimeScale(float value)
        {
            Time.timeScale = value;
            Time.fixedDeltaTime = value * 0.02f;
        }


        private void onEnemyHit(GameObject target)
        {
           
        }

        private void OnPlayerHit(GameObject target)
        {
            
        }
        public void Tick()
        {
            switch(mState)
            {
                case State.Idle:
                    OnStateIdle();
                    break;
                case State.Acceleration:
                    OnStateAcceleration();
                    break;
                case State.SlowMotion:
                    OnStateSlowMotion();
                    break;
                case State.WaitHit:
                    OnStateWaitHit();
                    break;
            }

            if (mState != State.Acceleration)
                return;
            if (mLevel.DistanceBetweenCameras > mLevel.ScreenWidth + mLevel.ScreenWidth / 2)
                return;
            SetTimeScale(0.1f);
        }


        public void Initialize()
        {
            InitLayersCollision();
            mPlayer.TriggerHit += OnPlayerHit;
            mEnemy.TriggerHit += onEnemyHit;
            mState = State.Idle;
            SetTimeScale(1);
        }

        public void Start()
        {
            mState = State.Acceleration;
            mPlayer.StartMove();
            mEnemy.StartMove();
        }


        /// <summary>
        /// States Handler
        /// </summary>

        private void SetState(State state)
        {
            if (mState == state)
                return;
            mState = state;
            switch (mState)
            {
                case State.Idle:
                    break;
                case State.Acceleration:
                    break;
                case State.SlowMotion:
                    SetTimeScale(0.1f);
                    break;
                case State.WaitHit:
                    mLevel.MaximizeLeftCamera();
                    break;
            }
            Tick();
        }
        
        private void OnStateIdle()
        {

        }
        
        private void OnStateAcceleration()
        {
            if (mLevel.DistanceBetweenCameras > mLevel.ScreenWidth + mLevel.ScreenWidth / 2)
                return;
            SetState(State.SlowMotion);
        }

        private void OnStateSlowMotion()
        {
            if (mLevel.DistanceBetweenCameras > mLevel.ScreenWidth)
                return;
            SetState(State.WaitHit);
        }

        private void OnStateWaitHit()
        {

        }
        ////////////////////////////////////////////////////////////////////////////////////////////
        
        private void InitLayersCollision()
        {
            SetCollisionIgnore(Layer.Default, Layer.Void, true);
            SetCollisionIgnore(Layer.Motorcicle, Layer.Motorcicle, true);
            SetCollisionIgnore(Layer.Rider, Layer.Rider, true);
            SetCollisionIgnore(Layer.Rider, Layer.Motorcicle, true);
            SetCollisionIgnore(Layer.Spear, Layer.Motorcicle, true);
            SetCollisionIgnore(Layer.Spear, Layer.Void, true);
            SetCollisionIgnore(Layer.Spear, Layer.Spear, true);
            SetCollisionIgnore(Layer.Void, Layer.Void, true);
            SetCollisionIgnore(Layer.Rider, Layer.Void, true);
            SetCollisionIgnore(Layer.Motorcicle, Layer.Void, true);
        }

        private void SetCollisionIgnore(string layer1, string layer2, bool ignore)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(layer1), LayerMask.NameToLayer(layer2), ignore);
        }
    }
}

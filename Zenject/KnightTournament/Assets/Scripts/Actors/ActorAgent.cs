using UnityEngine;
using System.Collections;
namespace Knights
{
    public class ActorAgent : MonoBehaviour, IActorAgent {

        public delegate void CollisionEvent(GameObject target);
        public event CollisionEvent TriggerHit;

        private BikerController mBiker;
        private MotorcycleController mMoto;

        private void OnBikerHit(GameObject target)
        {
            TriggerHit(target);
        }

        protected void Init()
        {
            mBiker = GetComponentInChildren<BikerController>();
            mBiker.TriggerHit += OnBikerHit;
            mMoto = GetComponentInChildren<MotorcycleController>();
        }

        public void StartMove()
        {
            mMoto.moving = true;
        }


    }
}

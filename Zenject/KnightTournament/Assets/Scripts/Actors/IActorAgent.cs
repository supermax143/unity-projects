using System;

namespace Knights
{
    interface IActorAgent
    {
        void StartMove();
        event Knights.ActorAgent.CollisionEvent TriggerHit;
    }
}

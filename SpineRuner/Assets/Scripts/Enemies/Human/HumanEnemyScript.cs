using UnityEngine;
using System.Collections;

public class HumanEnemyScript : MonoBehaviour {


    SkeletonAnimation skeletonAnimation;
    ColiderScript[] colliders;
    Spine.Bone weaponPlace;
    Transform playerTransfor;
    enum State  { Idle, Atack, DeathStart, Death};
    private State state = State.Idle;

	void Start () {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonAnimation.state.SetAnimation(1,"stand",true);
        colliders = GetComponentsInChildren<ColiderScript>();
        foreach (ColiderScript c in colliders)
            c.collisionEvent += OnCollisionEvent;
       
        weaponPlace = skeletonAnimation.skeleton.FindBone("weapon");
        playerTransfor = GameObject.FindGameObjectWithTag("Player").transform;
	}


    void Update()
    {
        if (state == State.Death)
            transform.Translate(new Vector3(0.1f,-0.5f));
        if(state==State.DeathStart)
            transform.Translate(new Vector3(0, 0.12f));
        if (state == State.Idle)
            CheckPlayerClose();
        
    }

    private void CheckPlayerClose()
    {
        if (state == State.Atack)
            return;
        Vector3 weaponPosition = new Vector3(skeletonAnimation.transform.position.x + transform.localScale.x * weaponPlace.worldX, skeletonAnimation.transform.position.y + weaponPlace.worldY);
      
        float minDistance = 8f;
        if (Vector3.Distance(weaponPosition, playerTransfor.position) < minDistance)
        {
            state = State.Atack;
            skeletonAnimation.state.SetAnimation(2, "atack", false);
        }
    }




    private void OnCollisionEvent(string sender, Collider2D collider)
    {
        if (collider.gameObject.tag == "Weapon")
            Death();
        if (collider.gameObject.tag == "Player")
        {
            ColiderScript coliderScript = collider.gameObject.GetComponent<ColiderScript>();
            if (coliderScript && coliderScript.colliderName == "bottom" && sender == "head")
                Death();
        }

        if (collider.gameObject.tag == "Destroyer")
            Destroy(gameObject);
    }

    private void Death()
    {
        foreach (ColiderScript cs in colliders)
            cs.gameObject.SetActive(false);

        skeletonAnimation.state.ClearTracks();
        skeletonAnimation.state.SetAnimation(0,"death",false);
        state = State.DeathStart;
        skeletonAnimation.state.End += OnDeathAnimationComplete;
    }

    void OnDeathAnimationComplete(Spine.AnimationState s, int trackIndex)
    {
        skeletonAnimation.state.End -= OnDeathAnimationComplete;
        state = State.Death;
    }
    
}

using UnityEngine;
using System.Collections;

public class HumanEnemyScript : MonoBehaviour {


    SkeletonAnimation skeletonAnimation;
    ColiderScript[] colliders;
    Spine.Bone weaponPlace;
    Transform playerTransfor;
    enum State  { Idle, Atack, Death};
    private State state = State.Idle;

	void Start () {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonAnimation.state.SetAnimation(1,"stand",true);
        colliders = GetComponentsInChildren<ColiderScript>();
        foreach (ColiderScript c in colliders)
        {
            c.triggerEnterEvent += OnCollisionEvent;
            c.collisionEnterEvent += OnCollisionEvent;        
        }
        weaponPlace = skeletonAnimation.skeleton.FindBone("weapon");
        playerTransfor = GameObject.FindGameObjectWithTag("Player").transform;
	}

   

    void Update()
    {
      
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
        HandleCollision(sender, collider.gameObject);
    }

    private void OnCollisionEvent(string sender, Collision2D collision)
    {
        HandleCollision(sender, collision.gameObject);
    }

    private void HandleCollision(string sender, GameObject gameObject)
    {
        if (state == State.Death)
            return;
        if (gameObject.tag == TagEnum.Weapon && sender!="weapon")
            Death();
        if (gameObject.tag == TagEnum.Player)
        {
            ColiderScript coliderScript = gameObject.GetComponent<ColiderScript>();
            if (coliderScript && coliderScript.colliderName == "bottom" && sender == "head")
                Death();
        }
    }

    private void Death()
    {
        state = State.Death;
        skeletonAnimation.state.ClearTracks();
        skeletonAnimation.state.SetAnimation(0,"death",false);
        gameObject.layer = LayerMask.NameToLayer(LayerEnum.Void);
        foreach (Transform child in transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer(LayerEnum.Void);
        }
        
        rigidbody2D.fixedAngle = false;
        rigidbody2D.AddForce(new Vector2(0, 1000));
        rigidbody2D.AddTorque(Random.Range(-500,500));
    }
}

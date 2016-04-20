using UnityEngine;
using System.Collections;

public class DragonControllerScript : MonoBehaviour {

    public float smoothness = 1;
    enum State { MovigToTargetPoint,  Death };
    private State state = State.MovigToTargetPoint;
    private SkeletonAnimation skeletonAnimation;

	void Start () {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        ColiderScript cs = GetComponentInChildren<ColiderScript>();
        cs.TriggerEnter += OnTriggerEnter;
        cs.CollisionEnter += OnCollisionEnter;
	}

    void OnCollisionEnter(string sender, Collision2D collision)
    {
        if(collision.gameObject.tag == TagEnum.Weapon)
            SetState(State.Death);
    }

   

    void OnTriggerEnter(string sender, Collider2D collider)
    {
        if (collider.gameObject.tag == TagEnum.Weapon)
            SetState(State.Death);
    }

    private void SetState(State value)
    {
        if(state==value)
            return;
        switch (value)
        {
            case State.Death:
                Death();
                break;
        }
        state = value;
    }

    private void Death()
    {
       
        skeletonAnimation.state.ClearTracks();
        gameObject.layer = LayerMask.NameToLayer(LayerEnum.Void);
        foreach (Transform child in transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer(LayerEnum.Void);
        }
        rigidbody2D.isKinematic = false;
        rigidbody2D.fixedAngle = false;
        rigidbody2D.AddForce(new Vector2(0, 1000));
        rigidbody2D.AddTorque(Random.Range(-500, 500));
        GetComponent<BonesBrakerScript>().BreakeBones();
    }


	// Update is called once per frame
	void Update () {

        switch(state)
        {
            case State.MovigToTargetPoint:
                MoveToTargetPoint();
                break;
        }
	
    }

    private void MoveToTargetPoint()
    {
        GetTargetPosition();
        transform.position = Vector2.Lerp(transform.position , GetTargetPosition(), Time.deltaTime*smoothness);
        
    }



    private Vector2 GetTargetPosition()
    {
        Vector2 position = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));
        position.x-=10;
        position.y-=10;
        return position;
    }
}

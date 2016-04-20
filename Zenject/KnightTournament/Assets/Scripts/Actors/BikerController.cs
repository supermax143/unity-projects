using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class BikerController : MonoBehaviour {

    public delegate void CollisionEvent(GameObject target);
    public event CollisionEvent TriggerHit;

    public Transform motocycleBase;
    public GameObject ownSpear;
    public PlayerModel playerModel;

    private HingeJoint2D joint;
    private JointMotor2D motor;
    private int speed = 0;
    private bool mDead = false;

    public bool Dead
    {
        get { return mDead; }
        set { mDead = value; }
    }
	void Start () {
        joint = transform.Find("biker_arm_1").GetComponent<HingeJoint2D>();
        motor = new JointMotor2D();
        motor.maxMotorTorque = 1000;
        ColliderDetector[] detectors = GetComponentsInChildren<ColliderDetector>();
        foreach(ColliderDetector detector in detectors)
        {
            detector.TriggerEnter += onTriggerEnter;
        }
    }

    private void onTriggerEnter(GameObject sender, Collider2D collider)
    {
        //if(GameManager.Instance.IsOponentDead(gameObject.tag))
        //    return;
        //if (mDead)
        //    return;
        if (collider.gameObject.Equals(ownSpear))
            return;
        if (collider.gameObject.tag != Tags.Spear)
            return;
        TriggerHit(sender);
        return;//to do: сделать нормально
        //Vector2 spearVelocity = collider.gameObject.rigidbody2D.velocity;
        //Debug.Log(gameObject.tag + " " + collider.gameObject.rigidbody2D.velocity);
        (sender.GetComponent<Renderer>() as SpriteRenderer).color = Color.red;
        
        Die(sender);
    }

    public void Die(GameObject sender)
    {
        ownSpear.GetComponent<Rigidbody2D>().isKinematic = false;
        ownSpear.GetComponent<Collider2D>().isTrigger = false;
        ownSpear.layer = LayerMask.NameToLayer(Layer.Void);
        ownSpear.GetComponent<Rigidbody2D>().velocity = ownSpear.transform.parent.GetComponent<Rigidbody2D>().velocity;
        ownSpear.transform.parent = null;

        mDead = true;

        sender.GetComponent<Rigidbody2D>().velocity *= -4; 

        HingeJoint2D[] motoJoints = motocycleBase.GetComponents<HingeJoint2D>();
        foreach (HingeJoint2D motoJoint in motoJoints)
            motoJoint.enabled = false;
        HingeJoint2D[] mJoints = GetComponents<HingeJoint2D>();
        foreach (HingeJoint2D mJoint in mJoints)
            mJoint.useMotor = false;
        foreach (Transform child in transform)
            child.gameObject.layer = LayerMask.NameToLayer(Layer.Void);
        joint.useMotor = false;
    }
	
	void Update () {
        if (mDead)
            return;
        if (InputManager.Instance.SpearUp)
        {
            speed = 20;
        }
        else if (InputManager.Instance.SpearDown)
        {
            speed = -20;
        }
        else
            speed = 0;
        motor.motorSpeed = speed;
        joint.motor = motor;
	}


}

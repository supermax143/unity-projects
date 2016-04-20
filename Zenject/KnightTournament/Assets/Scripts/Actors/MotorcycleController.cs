using UnityEngine;
using System.Collections;

public class MotorcycleController : MonoBehaviour {

    private const float MAX_FORWAD_SPEED = -3000;
    private const float MAX_BACKWARD_SPEED = 1000;

    private WheelJoint2D joint;
    private JointMotor2D motor;
    private float speeDelta = 1f;
    private float speed;
    public bool rotated = false;
    public bool moving = false;

	// Use this for initialization
	void Start () {
        joint = GetComponent<WheelJoint2D>();
        motor = new JointMotor2D();
        motor.maxMotorTorque = 10000;
        
	}
	

	// Update is called once per frame
	void Update () {
        if (!moving)
            UpdateSpeed(0);
        else
        {
            UpdateSpeed(rotated?-MAX_FORWAD_SPEED:MAX_FORWAD_SPEED);
        }
      
        motor.motorSpeed = speed;
        joint.motor = motor;
        joint.useMotor = speed!=0;
    }

    private void UpdateSpeed(float targetValue)
    {
        speed = Mathf.Lerp(speed, targetValue, Time.fixedDeltaTime*speeDelta);
    }
}

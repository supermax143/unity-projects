using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Animator aninator;
	private float maxSpeed = 10f;
	private float speed;

	// Use this for initialization
	void Start () {
		aninator = GetComponent<Animator>();
		//aninator.SetTrigger("stand");

	}
	
	// Update is called once per frame
	private void FixedUpdate()
	{

		float move = Input.GetAxis("Horizontal");
		
		//в компоненте анимаций изменяем значение параметра Speed на значение оси Х.
		//приэтом нам нужен модуль значения
		aninator.SetFloat("speed", Mathf.Abs(move));
		
		//обращаемся к компоненту персонажа RigidBody2D. задаем ему скорость по оси Х, 
		//равную значению оси Х умноженное на значение макс. скорости
		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
		

	}
}

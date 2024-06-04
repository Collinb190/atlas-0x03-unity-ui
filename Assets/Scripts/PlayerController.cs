using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed;
	public float sprint;
	public int health = 5;
	private int score = 0;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float currentSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? sprint : speed;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal * currentSpeed, rb.velocity.y, moveVertical * currentSpeed);
		rb.velocity = movement;
    }

    void OnTriggerEnter(Collider other)
	{
		// Coin Collision
		if (other.CompareTag("Pickup"))
		{
			score++;
            Debug.Log($"Score: {score}");
			Destroy(other.gameObject);
        }
		// Trap Collision
		if (other.CompareTag("Trap"))
		{
			health--;
			Debug.Log($"Health: {health}");
		}
    }
}

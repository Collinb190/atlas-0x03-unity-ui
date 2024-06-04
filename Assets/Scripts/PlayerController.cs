using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	public float speed;
	public float sprint;
	public int health;
	private int score;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		health = 5;
		score = 0;
	}
	
	// FixedUpdate is called once per frame and on physics clock
	void FixedUpdate () {
		float currentSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? sprint : speed;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal * currentSpeed, rb.velocity.y, moveVertical * currentSpeed);
		rb.velocity = movement;
    }

    // Update is called once per frame
    private void Update()
    {
        if (health == 0)
		{
			Debug.Log("Game Over!");
			ReloadScene();
		}
    }

	// Restart the scene
	void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
		// Goal Collision
		if (other.CompareTag("Goal"))
		{
			Debug.Log("You win!");
		}
    }
}

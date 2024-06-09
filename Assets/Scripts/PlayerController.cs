using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour {
	#region Fields

	// Player Stats
	public float speed;
	public float sprint;
	// UI Elements
    public TextMeshProUGUI scoreText;
	public TextMeshProUGUI healthText;
	public TextMeshProUGUI winLoseText;
	public int health;
    private int score;
	// Physics Elements
	private Rigidbody rb;
	// Teleporter Positions and push offset
	public GameObject teleporter1;
	public GameObject teleporter2;
	private Vector3 teleportPush = new Vector3 (0, 0, -3);

	#endregion

	#region Unity Methods

	// Use this for initialization
	void Start () 
	{
		InitializePlayer();
	}
	
	// FixedUpdate is called once per frame and on physics clock
	void FixedUpdate ()
	{
		MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
		CheckGameOver();
    }

	#endregion

	#region Custom Methods

	// Initialize player settings
	void InitializePlayer()
	{
		rb = GetComponent<Rigidbody>();
		health = 5;
		score = 0;
		SetScoreText();
		SetHealthText();
		winLoseText.transform.parent.gameObject.SetActive(false);
		winLoseText.text = "";
	}

	// Handle player movement
	void MovePlayer() 
	{
		float currentSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? sprint : speed;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal * currentSpeed, rb.velocity.y, moveVertical * currentSpeed);
		rb.velocity = movement;
	}

	// Restart the scene with delay
	IEnumerator LoadScene(float delay)
	{
		yield return new WaitForSeconds(delay);
		ReloadScene();
	}

	// Check if the game is over
	void CheckGameOver ()
	{
        if (health == 0)
		{
			DisplayWinLoseText(false);
			Debug.Log(" health zero ");
			StartCoroutine(LoadScene(3));
		}
	}

	// Restart the scene
	void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	// Method to update the score text
	void SetScoreText()
	{
		scoreText.text = "Score: " + score.ToString();
	}

	// Method to update the health text
	void SetHealthText()
	{
		healthText.text = "Health: " + health.ToString();
	}

	// Method to display win or loose text
	void DisplayWinLoseText(bool won)
	{
		Image bgImage = winLoseText.transform.parent.GetComponent<Image>();
		winLoseText.transform.parent.gameObject.SetActive(true);
		if (won)
		{
            bgImage.color = Color.green;
			winLoseText.color = Color.black;
            winLoseText.text = "You Win!";
		}
		else
		{
			winLoseText.text = "Game Over!";
		}
	}

 	#endregion

	#region  Collision Methods

	// Object Collisions
    void OnTriggerEnter(Collider other)
	{
		// Coin Collision
		if (other.CompareTag("Pickup"))
		{
			score++;
			SetScoreText();
			Destroy(other.gameObject);
        }
		// Trap Collision
		if (other.CompareTag("Trap"))
		{
			health--;
			SetHealthText();
		}
		// Goal Collision
		if (other.CompareTag("Goal"))
		{
			DisplayWinLoseText(true);
			StartCoroutine(LoadScene(3));
		}
		// Teleport Collision
		if (other.CompareTag("Teleport"))
		{
			if (other.gameObject.name == "Teleporter")
			{
                transform.position = teleporter2.transform.position + teleportPush;
            }
			if (other.gameObject.name == "Teleporter (1)")
			{
				transform.position = teleporter1.transform.position + teleportPush;
			}
		}
    }

	#endregion
}

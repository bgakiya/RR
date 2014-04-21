using UnityEngine;
using System.Collections;

public class ColourController : MonoBehaviour {

	enum Colour { Red = 0, Blue, Green, Yellow, Pink, Purple }

	public int colorIndex;
	public float initialAngle;
	public bool isMoving;
	public float cellWidth;
	public float cellHeight;

	CollisionDetectionDelegate collisionDelegate;
	public delegate void CollisionDetectionDelegate(GameObject colour);

	// Setters
	public CollisionDetectionDelegate CollisionDelegate
	{
		set { collisionDelegate = value; }
	}

	// ********************************************************************************
	// Method
	// ********************************************************************************
	public void MoveTo(Vector2 destination, float duration)
	{
		StartCoroutine(tweenTo(destination, duration));
	}

	// Coroutine
	IEnumerator tweenTo(Vector2 destination, float duration)
	{
		float timeThrough = 0.0f;

		Vector2 initialPosition = transform.position;

		while (Vector2.Distance(transform.position, destination) >= 0.05) // TODO, check why this value
		{
			timeThrough += Time.deltaTime;

			Vector2 target = Vector2.Lerp(initialPosition, destination, timeThrough / duration);

			transform.position = target;
			yield return null;

		}

		transform.position = destination;

		// if (this.rigidbody == null) // TODO, check what this is for
		//	Destroy(this.gameObject);

	}

	// ********************************************************************************
	// Method
	// ********************************************************************************
	// Use this for initialization
	void Start () {

		// Set the box collider
		// this.GetComponentInChildren<BoxCollider2D>().size = new Vector2(60, 60);

		colorIndex = Random.Range(0,5);
		
		// Debug.Log("Initialized");	
		
		switch (colorIndex) 
		{
			
		case (int)Colour.Red:
			this.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/red");
			break;
			
		case (int)Colour.Blue:
			this.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/blue");
			break;
			
		case (int)Colour.Green:
			this.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/green");
			break;
			
		case (int)Colour.Pink:
			this.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/pink");
			break;
			
		case (int)Colour.Purple:
			this.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/purple");
			break;
			
		default:
			this.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/yellow");
			break;
			
		}

		this.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3(cellWidth, cellHeight, 0);

	}

	// ********************************************************************************
	// Method
	// ********************************************************************************
	void OnCollision()
	{

	}

	/*void OnCollisionEnter2D(Collision2D coll)
	{
		Debug.Log("Collide");
		this.isMoving = false;
	}*/

	// ********************************************************************************
	// Method
	// ********************************************************************************
	void OnTriggerEnter2D(Collider2D other) {
		// Debug.Log("Collide with" + other.tag);

		if (this.isMoving)
		{
			this.isMoving = false;

			if (collisionDelegate != null)
			{
				collisionDelegate(this.gameObject);				
			}
			else
			{
				Debug.Log("Collision delegate is null");	
			}
		}

	}

	// Update is called once per frame
	// ********************************************************************************
	// Method
	// ********************************************************************************
	void Update () {

		if (isMoving)
		{
			// Debug.Log("IsMoving");
			// Move the object forward along its z axis 1 unit/second.
			// this.transform.Translate(Vector3.forward * (Time.deltaTime / 100));
			// Move the object upward in world space 1 unit/second.
			// transform.Translate(Vector3.up * (Time.deltaTime / 2), Space.World);
			float speedFactor = 4.0f;
			
			Vector3 newPosition = this.transform.position;

			// float newPositionX = newPosition.x + Mathf.
			
			//transform.Translate(Vector3.right * Mathf.Cos(Mathf.Deg2Rad * this.initialAngle) * (Time.deltaTime / 2));

			// Debug.Log("initialAngle: " + this.initialAngle);
			transform.Translate(Vector3.right * Mathf.Cos(Mathf.Deg2Rad * initialAngle) * (Time.deltaTime / 2) * speedFactor);

			// transform.Translate(transform.position * Mathf.Sin(Mathf.Deg2Rad * this.initialAngle) * (Time.deltaTime / 2));
			
			// transform.Translate(Vector3.up * Mathf.Sin(Mathf.Deg2Rad * this.initialAngle) * (Time.deltaTime / 2));

			// transform.Translate(Vector3.up * Mathf.Sin(Mathf.Deg2Rad * this.initialAngle) *  (Time.deltaTime / 2));

			transform.Translate(Vector3.up * Mathf.Sin(Mathf.Deg2Rad * this.initialAngle) * (Time.deltaTime / 2) * speedFactor);

			// transform.Translate(Vector3.left * Mathf.Cos(Mathf.Deg2Rad * this.initialAngle) * (Time.deltaTime));
			// transform.Translate(Vector3.down * Mathf.Sin(Mathf.Deg2Rad * this.initialAngle) * (Time.deltaTime / 2));

		}
	}

	// ********************************************************************************

}

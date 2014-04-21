using UnityEngine;
using System.Collections;
//using GameController;

public class GameController : MonoBehaviour {
	
	public int Rows;
	public int Columns;

	public int MaxRows;
	public int MaxColumns;
	
	public int LeftMargin;
	public int RightMargin;
	public int TopMargin;
	public int BottomMargin;
	
	public Vector2 ColourSize; // Same Width and Height
	
	//public GameObject colour;
	public GameObject colourObject;
	
	public ArrayList colours;
	
	public int CameraSize; // TODO, retrieve this from the main camera
	
	private GameObject currentColour;
	public Vector3 currentColourInitialPosition;

	private MatrixGeometry _geometry;
	private ColoursMatrix _matrix;
	
	// public Boundary boundary;
	
	enum Colour { Red = 0, Blue, Green, Yellow, Pink, Purple }

	#region Delegates

	void ColourCollision(GameObject colour)
	{

		Debug.Log("Collision handled by delegate");

		Debug.Log("Position X:" + colour.transform.position.x + " Y:" + colour.transform.position.y);

		ColourController colourController = colour.GetComponent<ColourController>();

		if (!colourController.isMoving)
		{
			Debug.Log("Reposition");
			Vector2 positionInMatrix = _geometry.RetrievePositionInMatrix(colour.transform.position.x, colour.transform.position.y);
			Debug.Log("positionInMatrix X:" + positionInMatrix.x + " Y:" + positionInMatrix.y);

			// Check if there's an empty color on the previous row and place it there instead
			int finalRow = _matrix.RetrievePreviousEmptyRow((int)positionInMatrix.y, (int)positionInMatrix.x);

			Vector2 positionInWorld =  _geometry.RetrievePositionInWorld(finalRow, (int)positionInMatrix.y);
			Debug.Log("positionInWorld X:" + positionInWorld.x + " Y:" + positionInWorld.y);

			colourController.MoveTo(positionInWorld, 0.5f);

			_matrix.InsertColour(colourController, finalRow, (int)positionInMatrix.y);

			this.currentColour = this.GenerateColour();
			this.currentColour.transform.position = currentColourInitialPosition;
			

		}
		// ColourController controller = currentColour.GetComponent<ColourController>();
		// controller.isMoving = true;

		// Update the matrix
		//Vector2 matrixPosition = MatrixHelper.CellForPosition(colour.transform.position, this._geometry);

		// Move the colour to the snap position smoothly
		//ColourController colourController = colour.GetComponent<ColourController>();
		//colourController.MoveTo(MatrixHelper.PositionForCell(matrixPosition, _geometry), 0.1f);

		// Create the new colour and set it up for firing
		//currentColour  = GenerateColour();

	}

	#endregion

	#region Unitymethods

	// ********************************************************************************
	// Method
	// ********************************************************************************
	// Use this for initialization
	void Start () {
		
		GenerateBattlefield ();
		
	}

	// ********************************************************************************
	// Method
	// ********************************************************************************
	void Awake()
	{
		// This gets the actual screen resolution
		// int screenHeight = Screen.currentResolution.width;
		// int screenWidth = Screen.currentResolution.height;
		
		// This gets the size of the actual game window
		// int screenHeight = Screen.width;
		// int screenWidth = Screen.height;
		
		//Debug.Log ("Screen HeightX: " + screenHeight);
		//Debug.Log ("Screen WidthX: " + screenWidth);
		
		// string[] res = UnityStats.screenRes.Split('x');
		// Debug.Log(int.Parse(res[0]) + " " + int.Parse(res[1]));
	}

	#endregion // Unity Methods

	// ********************************************************************************
	// Method
	// ********************************************************************************
	private GameObject GenerateColour()
	{
		// Debug.Log("Colour created");
		GameObject holster = (GameObject)Instantiate(Resources.Load("Prefabs/Colour")); // (GameObject)Instantiate(Resources.Load("Prefabs/Colour"), currentColourInitialPosition, Quaternion.identity);
		ColourController colourController = holster.GetComponent<ColourController>();
		colourController.cellHeight = 1.0f;
		colourController.cellWidth = 1.0f;

		//BoxCollider2D collider = holster.GetComponent<BoxCollider2D>();
		//if (collider == null)
		//	Debug.Log("Collider is null");

		//collider.size = new Vector2(1.21f, 1.21f);
		//collider.center = Vector2.zero;

		colourController.CollisionDelegate = ColourCollision;
		return holster;

	}


	// ********************************************************************************
	// Method
	// ********************************************************************************

	private Vector3 mouse_pos;
	private Vector3 object_pos;
	private float angle;
	
	void FixedUpdate () {    
		//

		GameObject  cannon = GameObject.FindGameObjectWithTag("Cannon");

		/*if (cannon == null)
			Debug.Log("Cannon is null");
		else
			Debug.Log("Cannon is not null");

		Debug.Log("CO: " + cannon.transform.position.x + ", " + cannon.transform.position.y + ", " + cannon.transform.position.z);
		*/

		mouse_pos = Input.mousePosition;
		mouse_pos.z = 0.0f;
		object_pos = Camera.main.WorldToScreenPoint(cannon.transform.position);
		mouse_pos.x = mouse_pos.x - object_pos.x;
		mouse_pos.y = mouse_pos.y - object_pos.y;

		//angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg - 90;

		angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) *  Mathf.Rad2Deg;

		// Debug.Log("ANGLE: " + angle + "Cannon Pos: " + object_pos.x + ", " + object_pos.y + ", " + object_pos.z);
		// Vector3 rotationVector = new Vector3 (0, 0, angle);
		// transform.rotation = Quaternion.Euler(rotationVector);
	}


	// ********************************************************************************
	// Method
	// ********************************************************************************
	void GenerateBattlefield()
	{
		
		colours = new ArrayList();
		_matrix = new ColoursMatrix(this.MaxRows, this.MaxColumns);
		_geometry = new MatrixGeometry(LeftMargin, RightMargin, TopMargin, 0, this.MaxRows, this.MaxColumns, this.ColourSize, new Vector2(Screen.width, Screen.height));
		
		int counter = 0;
		
		// Colours shall be draw from top to bottom, left to right
		for (int i = 0; i < Rows; i++) 
		{
			
			for (int j = 0; j < Columns; j++) 
			{

				Debug.Log("Add Colour at I:" + i + " J:" + j);

				GameObject newColour = GenerateColour(); //  (GameObject)Instantiate(Resources.Load("Prefabs/Colour"), colourPositionWorld, Quaternion.identity);
				newColour.transform.position = _geometry.RetrievePositionInWorld(i, j);

				// Debug.Log("newColour.transform.position X: " + newColour.transform.position.x + " Y:" + newColour.transform.position.y + " Z:" + newColour.transform.position.z);
				// colours.Add(newColour);

				ColourController controller = newColour.GetComponent<ColourController>();
				_matrix.InsertColour(controller, j, i);

				counter++;
			}
		}
		
		// Sprite redSprite = Resources.LoadAll<Sprite>("red");
		// Sprite redSprite = Resources.Load<Sprite>("red");

		// Add a new one in the cannon, this is the next ne to be shoot
		currentColour  = GenerateColour(); //  (GameObject)Instantiate(Resources.Load("Prefabs/Colour"), currentColourInitialPosition, Quaternion.identity);
		currentColour.transform.position = currentColourInitialPosition;
		// ColourController controller = currentColour.GetComponent<ColourController>();
		// controller.isMoving = true;

		/*if (redSprite == null)
			Debug.Log ("Sprite is null d :(");
		
		Debug.Log ("Width: " + redSprite.texture.width);*/
		// colours[3].SetActive(false);
		// colours [3].GetComponent<SpriteRenderer> ().sprite = redSprite;
		// colours [3].GetComponentInChildren<SpriteRenderer> ().sprite = redSprite;
		
		Vector3 battlefieldOriginWorld = Camera.main.ScreenToWorldPoint(new Vector3 (LeftMargin, BottomMargin, 10));
		Vector3 battlefieldMaximumWorld = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width - RightMargin, Screen.height - TopMargin, 10));

		Vector3 centerPosition = new Vector3(Screen.width / 2, Screen.height / 2, 10);
		Vector3 centerPositionWS = Camera.main.ScreenToWorldPoint(centerPosition);

		Debug.Log("Center Position X:" + centerPosition.x + " Y:" + centerPosition.y + " Z:" + centerPosition.z);
		Debug.Log("Center PositionWS X:" + centerPositionWS.x + " Y:" + centerPositionWS.y + " Z:" + centerPositionWS.z);

		//GameObject anchorColour = GenerateColour();
		//anchorColour.transform.position = centerPositionWS;

		// Instantiate (colour, battlefieldOriginWorld, Quaternion.identity);
		// Instantiate (colour, battlefieldMaximumWorld, Quaternion.identity);
		
		//float battlefieldWidthWorld = Camera.main.ScreenToWorldPoint (battlefieldWidthScreen);
		//float battlefieldHeightWorld = Camera.main.ScreenToWorldPoint (battlefieldHeightScreen);
		
		
		//Debug.Log ("Screen - X:" + battlefieldWidthScreen + " Y:" + battlefieldHeightScreen);
		//Debug.Log ("World - X:" + battlefieldWidthWorld + " Y:" + battlefieldHeightWorld);
		
		// Battlefield should be drawn from top to bottom, left to right
		
		//GameObject colourx = Instantiate (colour);
		//colourx.transform.position = new Vector3 (0, 0, 0);
		
		// Debug.Log ("PizelWidth: " + Camera.main.);
		
		// Vector3 screenPosition = new Vector3 (160, 0, 10);
		Vector3 screenPosition = new Vector3 (Screen.width, Screen.height, 10);
		Vector3 screenPositionOrigin = new Vector3 (0, 0, 10);
		Vector3 screenPositionMid = new Vector3 (Screen.width / 2, Screen.height / 2, 10);
		
		// Vector3 worldPosition = Camera.main.ScreenToWorldPoint (screenPosition);
		// Vector3 worldPosition = Camera.main.ViewportToScreenPoint(screenPosition);
		Vector3 worldPositionMax = Camera.main.ScreenToWorldPoint(screenPosition);
		Vector3 worldPositionMid = Camera.main.ScreenToWorldPoint(screenPositionMid);
		Vector3 worldPositionMin = Camera.main.ScreenToWorldPoint(screenPositionOrigin);
		
		Debug.Log ("XMax:" + worldPositionMax.x + " YMax:" + worldPositionMax.y); 
		Debug.Log ("XMin:" + worldPositionMin.x + " YMin:" + worldPositionMin.y); 
		Debug.Log ("XMid:" + worldPositionMid.x + " YMid:" + worldPositionMid.y); 
		
		
		// Instantiate(colour, new Vector3(-1.6f, -2.2f, 0), Quaternion.identity);
		//Instantiate(colour, new Vector3(-3f, 5, 0), Quaternion.identity);
		
		//Instantiate(colour, worldPositionMax, Quaternion.identity);
		//Instantiate(colour, worldPositionMin, Quaternion.identity);
		//Instantiate(colour, worldPositionMid, Quaternion.identity);
		
		
		/*for (int i = 0; i < Rows; i++) 
		{

			for (int j = 0; j < Columns; j++) 
			{

				Instantiate(colour, new Vector3(i, j, 0), Quaternion.identity);


			}
		}*/
		
		
	}

	// ********************************************************************************
	// Method
	// ********************************************************************************
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			//currentColour  = (GameObject)Instantiate(Resources.Load("Prefabs/Colour"), currentColourInitialPosition, Quaternion.identity);
			ColourController controller = currentColour.GetComponent<ColourController>();

			// controller.transform.position = new Vector3(0, 0, 0);

			controller.initialAngle = angle;
			controller.isMoving = true;

		}

	}


	// ********************************************************************************
	// Method
	// ********************************************************************************
	void GenerateBattlefieldold()
	{

		colours = new ArrayList();
		_matrix = new ColoursMatrix(this.Rows, this.Columns);
		_geometry = new MatrixGeometry(LeftMargin, RightMargin, TopMargin, 0, this.Rows, this.Columns, this.ColourSize, new Vector2(Screen.width, Screen.height));

		// Calculate the size of the battlefield removing the margins
		float battlefieldWidthScreen = Screen.width - (LeftMargin + RightMargin);
		float battlefieldHeightScreen = Screen.height - (TopMargin + BottomMargin);

		float battlefieldTop = Screen.height - TopMargin;
		float battlefieldLeft = Screen.width - TopMargin;

		// float colourSeparationWidth = battlefieldWidthScreen / colourSize;
		// float colourSeparationHeight = battlefieldHeightScreen / colourSize;
		float colourSeparationWidth = battlefieldWidthScreen / Columns;
		float colourSeparationHeight = battlefieldHeightScreen / Rows;


		// Determine the position of the first colour
		//Vector3 colourPositionScreen = new Vector3 (LeftMargin + (colourSize / 2), 100, 10);
		//Vector3 colourPositionWorld = Camera.main.ScreenToWorldPoint (colourPositionScreen);

		//if (blueSprite == null)
		//	Debug.Log("Blue Sprite is Null");

		// Instantiate (colour, colourPositionWorld, Quaternion.identity);
		int counter = 0;

		// Colours shall be draw from top to bottom, left to right
		for (int i = 0; i < Rows; i++)
		{
			float yPosition = ((battlefieldTop - (colourSeparationWidth / 2) - (colourSeparationWidth * i)));

			for (int j = 0; j < Columns; j++)
			{
				float xPosition = (LeftMargin + (colourSeparationWidth / 2) + (colourSeparationWidth * j));

				// Debug.Log("X Pos: " + xPosition + " Y Pos:" + yPosition);

				Vector3 colourPositionWorld = Camera.main.ScreenToWorldPoint(new Vector3(xPosition, yPosition, 10));
				//GameObject newColour = GenerateColour(); //  (GameObject)Instantiate(Resources.Load("Prefabs/Colour"), colourPositionWorld, Quaternion.identity);
				//newColour.transform.position = colourPositionWorld;
				//colours.Add(newColour);

				//ColourController controller = newColour.GetComponent<ColourController>();

				//_matrix.InsertColour(controller, j, i);

				counter++;

			}
		}

		// Sprite redSprite = Resources.LoadAll<Sprite>("red");
		// Sprite redSprite = Resources.Load<Sprite>("red");

		// Add a new one in the cannon, this is the next ne to be shoot
		// currentColour = GenerateColour(); //  (GameObject)Instantiate(Resources.Load("Prefabs/Colour"), currentColourInitialPosition, Quaternion.identity);
		// currentColour.transform.position = currentColourInitialPosition;
		// ColourController controller = currentColour.GetComponent<ColourController>();
		// controller.isMoving = true;

		/*if (redSprite == null)
			Debug.Log ("Sprite is null d :(");
		
		Debug.Log ("Width: " + redSprite.texture.width);*/
		// colours[3].SetActive(false);
		// colours [3].GetComponent<SpriteRenderer> ().sprite = redSprite;
		// colours [3].GetComponentInChildren<SpriteRenderer> ().sprite = redSprite;

		Vector3 battlefieldOriginWorld = Camera.main.ScreenToWorldPoint(new Vector3(LeftMargin, BottomMargin, 10));
		Vector3 battlefieldMaximumWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - RightMargin, Screen.height - TopMargin, 10));

		//GameObject anchorColour = GenerateColour();

		Vector3 centerPosition = new Vector3(Screen.width / 2, Screen.height / 2, 10);
		Vector3 centerPositionWS = Camera.main.ScreenToWorldPoint(centerPosition);

		Debug.Log("Center Position X:" + centerPosition.x + " Y:" + centerPosition.y + " Z:" + centerPosition.z);
		Debug.Log("Center PositionWS X:" + centerPositionWS.x + " Y:" + centerPositionWS.y + " Z:" + centerPositionWS.z);

		//anchorColour.transform.position = centerPositionWS;

		// Instantiate (colour, battlefieldOriginWorld, Quaternion.identity);
		// Instantiate (colour, battlefieldMaximumWorld, Quaternion.identity);

		//float battlefieldWidthWorld = Camera.main.ScreenToWorldPoint (battlefieldWidthScreen);
		//float battlefieldHeightWorld = Camera.main.ScreenToWorldPoint (battlefieldHeightScreen);


		//Debug.Log ("Screen - X:" + battlefieldWidthScreen + " Y:" + battlefieldHeightScreen);
		//Debug.Log ("World - X:" + battlefieldWidthWorld + " Y:" + battlefieldHeightWorld);

		// Battlefield should be drawn from top to bottom, left to right

		//GameObject colourx = Instantiate (colour);
		//colourx.transform.position = new Vector3 (0, 0, 0);

		// Debug.Log ("PizelWidth: " + Camera.main.);

		// Vector3 screenPosition = new Vector3 (160, 0, 10);
		Vector3 screenPosition = new Vector3(Screen.width, Screen.height, 10);
		Vector3 screenPositionOrigin = new Vector3(0, 0, 10);
		Vector3 screenPositionMid = new Vector3(Screen.width / 2, Screen.height / 2, 10);

		// Vector3 worldPosition = Camera.main.ScreenToWorldPoint (screenPosition);
		// Vector3 worldPosition = Camera.main.ViewportToScreenPoint(screenPosition);
		Vector3 worldPositionMax = Camera.main.ScreenToWorldPoint(screenPosition);
		Vector3 worldPositionMid = Camera.main.ScreenToWorldPoint(screenPositionMid);
		Vector3 worldPositionMin = Camera.main.ScreenToWorldPoint(screenPositionOrigin);

		Debug.Log("XMax:" + worldPositionMax.x + " YMax:" + worldPositionMax.y);
		Debug.Log("XMin:" + worldPositionMin.x + " YMin:" + worldPositionMin.y);
		Debug.Log("XMid:" + worldPositionMid.x + " YMid:" + worldPositionMid.y);


		// Instantiate(colour, new Vector3(-1.6f, -2.2f, 0), Quaternion.identity);
		//Instantiate(colour, new Vector3(-3f, 5, 0), Quaternion.identity);

		//Instantiate(colour, worldPositionMax, Quaternion.identity);
		//Instantiate(colour, worldPositionMin, Quaternion.identity);
		//Instantiate(colour, worldPositionMid, Quaternion.identity);


		/*for (int i = 0; i < Rows; i++) 
		{

			for (int j = 0; j < Columns; j++) 
			{

				Instantiate(colour, new Vector3(i, j, 0), Quaternion.identity);


			}
		}*/


	}

	// ********************************************************************************
}

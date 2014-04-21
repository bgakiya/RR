using System;
using UnityEngine;

public class MatrixGeometry
{
	
	
	
	public int _rows;
	public int _columns;

	private float _leftMargin;
	private float _rightMargin;	
	private float _topMargin;

	private Vector3 _vectorZeroWS;
	private Vector3 _vectorHalfWS;
	private Vector3 _vectorOneWS;

	#region Props

	private float _leftMarginWS;
	public float LeftMarginWS
	{
		get { return _leftMarginWS; }
	}

	private float _rightMarginWS;
	public float RightMarginWS
	{
		get { return _rightMarginWS; }
	}

	private float _leftMarginWidthWS;
	public float LeftMarginWidthWS
	{
		get { return _leftMarginWidthWS; }
	}

	private float _topMarginWidthWS;
	public float TopMarginWidthWS
	{
		get { return _topMarginWidthWS; }
	}

	private float _bottomMarginWidthWS;
	public float BottomMarginWidthWS
	{
		get { return _bottomMarginWidthWS; }
	}

	private float _rightMarginWidthWS;
	public float RightMarginWidthWS
	{
		get { return _rightMarginWidthWS; }
	}
	
	private float _topMarginWS;
	public float TopMarginWS
	{
		get { return _topMarginWS; }
	}

	private float _screenWidthWS;
	
	public float ScreenWidthWS
	{
		get { return _screenWidthWS; }
	}
	
	private float _screenHeightWS;
	
	public float ScreenHeightWS
	{
		get { return _screenHeightWS; }
	}

	private float _screenWidthSS;

	public float ScreenWidthSS
	{
		get { return _screenWidthSS; }
	}

	private float _screenHeightSS;
	
	public float ScreenHeightSS
	{
		get { return _screenHeightSS; }
	}

	private float _cellWidthWS;

	public float CellWidthWS
	{
		get { return _cellWidthWS; }
	}

	private float _cellHeightWS;
	
	public float CellHeightWS
	{
		get { return _cellHeightWS; }
	}

	private float _battlefieldWidthWS;

	public float BattlefieldWidthWS
	{
		get { return _battlefieldWidthWS; }
	}

	private float _battlefieldHeightWS;

	public float BattlefieldHeightWS
	{
		get { return _battlefieldHeightWS; }
	}

	#endregion

	// public Vector2 ColourSize;

	private Vector2 _colourSizeWS;

	public Vector2 ColourSizeWS
	{
		get { return _colourSizeWS; }
	}


	private Vector2[,] _matrixPositionsWS;

	// ********************************************************************************
	// Constructor
	// ********************************************************************************
	public MatrixGeometry (float LeftMargin, float RightMargin, float TopMargin, float BottomMargin,
		int Rows, int Columns, Vector2 ColourSize, Vector2 ScreenSize)
	{

		this._columns = Columns;
		this._rows = Rows;

		Debug.Log("Matrix Dimensions :: X: " + _columns + " Y: " + _rows);

		_matrixPositionsWS = new Vector2[Rows, Columns];

		_screenHeightSS = ScreenSize.y;
		_screenWidthSS = ScreenSize.x;

		Debug.Log("Full Screen :: Screen - Height: " + _screenHeightSS + " Width: " + _screenWidthSS);

		_vectorZeroWS = Camera.main.ScreenToWorldPoint(Vector3.zero);
		Debug.Log("Vector Zero WS X: " + _vectorZeroWS.x + " Y: " + _vectorZeroWS.y);

		_vectorHalfWS = Camera.main.ScreenToWorldPoint(new Vector3(ScreenSize.x / 2, ScreenSize.y / 2, 0));
		Debug.Log("Vector Half WS X: " + _vectorHalfWS.x + " Y: " + _vectorHalfWS.y);

		_vectorOneWS = Camera.main.ScreenToWorldPoint(new Vector3(ScreenSize.x, ScreenSize.y, 0));
		Debug.Log("Vector One WS X: " + _vectorOneWS.x + " Y: " + _vectorOneWS.y);

		Vector3 screenSizeWS = _vectorOneWS - _vectorZeroWS;
		Debug.Log("Screen Size WS X: " + screenSizeWS.x + " Y: " + screenSizeWS.y);

		_screenWidthWS = screenSizeWS.x;
		_screenHeightWS = screenSizeWS.y;

		Debug.Log("Full Screen :: World - Height: " + _screenHeightWS + " Width: " + _screenWidthWS);

		// Determine ColourSize in WS
		// TODO, find a way to convert screen toworld pos to use V2
		Vector3 colourSizeWS = Camera.main.ScreenToWorldPoint(new Vector3(ColourSize.x, ColourSize.y, 0)) - _vectorZeroWS;
		_colourSizeWS = new Vector2(colourSizeWS.x, colourSizeWS.y);

		Debug.Log("Colour Size SS X:" + ColourSize.x + " Y:" + ColourSize.y + " - WS X:" + _colourSizeWS.x + " Y:" + _colourSizeWS.y);

		Vector3 debugPos = Camera.main.ScreenToWorldPoint(new Vector3(10, 10, 10));
		Debug.Log("Debug Pos SS X:" + debugPos.x + " Y:" + debugPos.y);

		Vector3 debugPos2 = Camera.main.ScreenToWorldPoint(new Vector3(10, 10, 50));
		Debug.Log("Debug Pos 2 SS X:" + debugPos2.x + " Y:" + debugPos2.y);

		// Calculate the margins in WS

		// Left and Right
		Vector3 marginHorizontalWS = Camera.main.ScreenToWorldPoint(new Vector3(LeftMargin, RightMargin, 0)) - _vectorZeroWS;

		_leftMarginWidthWS = marginHorizontalWS.x;
		_rightMarginWidthWS = marginHorizontalWS.y;

		// top and bottom (x = Top, y = Bottom)
		Vector3 marginVerticalWS = Camera.main.ScreenToWorldPoint(new Vector3(TopMargin, BottomMargin, 0)) - _vectorZeroWS;

		_topMarginWidthWS = marginVerticalWS.x;
		_bottomMarginWidthWS = marginVerticalWS.y;

		Debug.Log("Left Margin Width WS: " + _leftMarginWidthWS + " - Right Margin Width WS:" + _rightMarginWidthWS);

		// Determine the left margin position for the battlefield
		_leftMarginWS = _vectorZeroWS.x + _leftMarginWidthWS;

		Debug.Log("Left Margin WS: " + _leftMarginWS);

		_battlefieldWidthWS = _screenWidthWS - (_leftMarginWidthWS + _rightMarginWidthWS);

		Debug.Log("WS battlefieldWidth: " + _battlefieldWidthWS + " - Left Margin: " + _leftMarginWidthWS + " - Right Margin: " + _rightMarginWidthWS + " - Screen Width: " + _screenWidthWS);

		// Currently we're gonna use square cells, so no need to calculate the height of it, since the width of the screen is 
		// smaller than the height
		float cellSizeWS = _battlefieldWidthWS / Columns;

		this._cellHeightWS = cellSizeWS;
		this._cellWidthWS = cellSizeWS;

		Debug.Log("Matrix Positions =======================================================================");

		Debug.Log("_vectorOneWS.y: " + _vectorOneWS.y + " _topMarginWidthWS:" + _topMarginWidthWS + " _cellHeightWS:" + _cellHeightWS);

		// Generate matrix positions
		for (int i = 0; i < _rows; i++)
		{

			float rowPositionWS = _vectorOneWS.y - _topMarginWidthWS - (_cellHeightWS / 2) - (_cellHeightWS * i);

			rowPositionWS = (float)Math.Round(rowPositionWS, 3);

			// Debug.Log("rowPositionWS: " + rowPositionWS + " _screenHeightWS:" + _screenHeightWS + " _topMarginWS:" + _topMarginWS + " _cellHeightWS: " + _cellHeightWS + " _cellHeightWS:" + _cellHeightWS + " i: " + i);

			for (int j = 0; j < _columns; j++)
			{
				float columnPositionWS = _vectorZeroWS.x +  _leftMarginWidthWS + (_cellWidthWS / 2) + (_cellWidthWS * j);
				columnPositionWS = (float)Math.Round(columnPositionWS, 3);

				Debug.Log("I: " + i + " J:" + j + "RowPos:" + rowPositionWS + " ColPos: " + columnPositionWS);

				_matrixPositionsWS[i, j] = new Vector2(columnPositionWS, rowPositionWS);
			}
		}

		Debug.Log("Geometry Created =======================================================================");

	}


	// ********************************************************************************
	// Method
	// ********************************************************************************
	public Vector3 RetrievePositionInWorld(int row, int column)
	{
		// Debug.Log("RetrievePositionWS at " + row + ", " + column);
		Vector2 holster = _matrixPositionsWS[row, column];
		// Debug.Log("PositionRetrieved");
		return new Vector3(holster.x, holster.y, 50);
	}

	// ********************************************************************************
	// Method
	// ********************************************************************************
	/*public int RetrievePreviousEmptyRow(int column, int row)
	{
		for (int i = row; i > 0; i++)
		{
			if this.
		}
	}*/

	// ********************************************************************************
	// Method
	// ********************************************************************************
	public Vector2 RetrievePositionInMatrix(float PositionXWS, float PositionYWS)
	{
		int column = 0;
		int row = 0;

		Debug.Log("Columns: " + _columns);
		Debug.Log("Rows: " + _rows);

		for (int i = 0; i <= _columns; i++)
		{
			// Debug.Log("i: " + i + " VectorZero.X " + _vectorZeroWS.x + " _leftMarginWidthWS: " + _leftMarginWidthWS + " cellWidthWS:" + _cellWidthWS);
			if (PositionXWS < (_vectorZeroWS.x + _leftMarginWidthWS) + (_cellWidthWS * i))
			{
				column = i - 1; // Because of the zero base
				break;
			}
		}

		// Debug.Log("_cellHeightWS:" + _cellHeightWS);
		// Debug.Log("PositionYWS: " + PositionYWS + " - VectorOne Y:" + _vectorOneWS.y + " - _topMarginWidthWS: " + _topMarginWidthWS + " - _cellHeightWS:" + _cellHeightWS);
		// Debug.Log("PositionYWS:" + PositionYWS + " InitialPos: " + (_vectorOneWS.y - _topMarginWidthWS) + (_cellHeightWS * (1 + 0)));

		//Debug.Log("VectorOneWS:" + _vectorOneWS.y);
		//Debug.Log("TopMarginWidth:" + _topMarginWidthWS);
		//Debug.Log("CellHeightWS:" + _cellHeightWS);

		for (int i = 0; i <= _rows; i++)
		{	
			//Debug.Log("XXX I: " + i + " - PositionYWS:" + PositionYWS + " InitialPos: " + (_vectorOneWS.y - (_topMarginWidthWS + (_cellHeightWS * i))));

			//Debug.Log("PositionYWS: " + PositionYWS + " Current Position: " + (_vectorOneWS.y - (_topMarginWidthWS + (_cellHeightWS * i))));
			
			if (PositionYWS > (_vectorOneWS.y - (_topMarginWidthWS + (_cellHeightWS * i))))
			{
				//Debug.Log("I: " + i + "PositionYWS: " + PositionYWS + " - VectorOne Y:" + _vectorOneWS.y + " - _topMarginWidthWS: " + _topMarginWidthWS + " - _cellHeightWS:" + _cellHeightWS);
				row = i - 1; // TODO, check why this
				break;
			}
		}

		return new Vector2(row, column);
	}

	// ********************************************************************************
	// Method
	// ********************************************************************************
}

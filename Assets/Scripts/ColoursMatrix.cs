using UnityEngine;
using System.Collections;

public class ColoursMatrix
{

	private int _rows;
	private int _columns;

	// Rows / Columns
	private ColourController[,] _coloursMatrix;

	// ********************************************************************************
	// Method
	// ********************************************************************************
	public ColoursMatrix (int Rows, int Columns)
	{
		_rows = Rows;
		_columns = Columns;

		Debug.Log("ColoursMatrix (int Rows, int Columns): " + Rows + ", " + Columns);

		this._coloursMatrix = new ColourController[_rows, _columns];
	}

	// ********************************************************************************
	// Method
	// ********************************************************************************
	public void InsertColour(ColourController colour, int atRow, int atColumn)
	{
		if (atColumn < 0 || atColumn > this._columns - 1 || atRow < 0 || atRow > this._rows -1)
			throw new System.ArgumentException("Ilegal Coordinates");

		Debug.Log("Colour Inserted At Row:" + atRow + " AtCol:" + atColumn);
		this._coloursMatrix[atRow, atColumn] = colour;

	}

	// ********************************************************************************
	// Method
	// ********************************************************************************
	public int RetrievePreviousEmptyRow(int column, int row)
	{

		Debug.Log("RetrievePreviousEmptyRow Column: " + column + " Row:" + row);

		int emptyRow = row;

		for (int i = row; i > 0; i--)
		{
			// Debug.Log("Row: " + row);
			Debug.Log("this._coloursMatrix[i - 1, column]: " + this._coloursMatrix[i - 1, column] + " i-1:" + (i -1) + " column:" + column);

			if (this._coloursMatrix[i - 1, column] != null)
			{
				Debug.Log("emptyRow:" + i);
				return i;
			}

		}

		Debug.Log("emptyRow:" + emptyRow);

		return emptyRow;
	}

	// ********************************************************************************
	// Method
	// ********************************************************************************
	public void RemoveColour(int xPosition, int yPosition)
	{
		if (xPosition < 0 || xPosition > this._columns - 1 || yPosition < 0 || yPosition > this._rows -1)
			throw new System.ArgumentException("Ilegal Coordinates");
		
		this._coloursMatrix[xPosition, yPosition] = null;
		
	}

	// ********************************************************************************
	// Method
	// ********************************************************************************
}

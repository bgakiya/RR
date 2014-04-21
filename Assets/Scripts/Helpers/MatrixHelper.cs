using UnityEngine;
using System.Collections;

public class MatrixHelper
{

	/* Returns the position in the matrix given a position and a matrix geometry
	 * Params
	 * Vecto3 : Position of the object
	 * MatrixGeometry : Geometry of the matrix (to determine limits and stuff)
	 * Return : Vector2 : Position of the object in the matrix
	 */
	public static  Vector2 CellForPosition(Vector2 screenPosition, MatrixGeometry geometry)
	{
		//int row =  geometry.Rows = Mathf.FloorToInt(screenPosition.y) - 1;

		//int column = Mathf.FloorToInt(screenPosition.x);

		return Vector2.zero; // new Vector2(row, column);
	}

	// ********************************************************************************
	// Method
	// ********************************************************************************
	public static Vector2 PositionForCell(Vector2 cell, MatrixGeometry geometry)
	{
		// X = Column
		// Y = Row

		return new Vector2(0, 0);

	}

	// ********************************************************************************
	// Method
	// ********************************************************************************

}

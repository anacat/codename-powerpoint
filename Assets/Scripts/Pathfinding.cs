using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour 
{
	private GridGrid _grid;
	public Transform player;

	private void Start()
	{
		_grid = GetComponent<GridGrid>();
	}

	public void FindPath(Node finalDestination)
	{
		Debug.Log(_grid.WorldPositionToGrid(player.transform.position).x + " " + _grid.WorldPositionToGrid(player.transform.position).y);
		Debug.Log(finalDestination.x + " " + finalDestination.y);
	}

}

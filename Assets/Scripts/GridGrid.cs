using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGrid : MonoBehaviour 
{
	public Vector2 gridSize;
	public int nrCells;
	public LayerMask unwalkableMask;
	public GameObject walkableNodePrefab;

	private Node[,] _grid;
	private float _cellSize;

	private void Awake()
	{
		_cellSize = gridSize.x / nrCells;

		DrawGrid();
	}

	private void DrawGrid()
	{
		_grid = new Node[nrCells, nrCells];

		Vector2 worldBottomLeft = new Vector2(transform.position.x - (_cellSize/2 * (nrCells + 1)), transform.position.y - (_cellSize/2 * (nrCells + 1)));

		for(int x = 1; x <= nrCells; x++)
		{
			for(int y = 1; y <= nrCells; y++)
			{
				Vector2 worldPosition = new Vector2(worldBottomLeft.x + (_cellSize * x), worldBottomLeft.y + (_cellSize * y));
				bool walkable = Physics2D.OverlapCircle(worldPosition, _cellSize/2 * 0.9f, unwalkableMask) == null;

				_grid[x - 1, y - 1] = new Node(x - 1, y - 1, worldPosition, _cellSize * 0.9f, walkable);

				if(walkable)
				{
					GameObject walkableNode = (GameObject) Instantiate(walkableNodePrefab, worldPosition, Quaternion.identity);
					walkableNode.transform.SetParent(transform);
					walkableNode.GetComponent<WalkableNode>().CreateNode(_grid[x - 1, y - 1]);
				}
			}
		}
	}

	public void UpdateGrid()
	{
		foreach(Node n in _grid)
		{
			bool walkable = Physics2D.OverlapCircle(n.GetWorldPosition(), _cellSize/2 * 0.9f, unwalkableMask) == null;
			n.IsWalkable = walkable;
		}
	}

	public Node WorldPositionToGrid(Vector2 worldPosition)
	{
		RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

		if(hit.collider != null && hit.collider.CompareTag("Walkable"))
		{
			Debug.Log(hit.collider.tag);
			return hit.collider.gameObject.GetComponent<WalkableNode>().GetNode();
		}

		return null;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, gridSize.y, 0));

		_cellSize = gridSize.x / nrCells;
		Vector2 worldBottomLeft = new Vector2(transform.position.x - (_cellSize/2 * (nrCells + 1)), transform.position.y - (_cellSize/2 * (nrCells + 1)));

		for (int x = 1; x <= nrCells; x++)
		{
			for (int y = 1; y <= nrCells; y++)
			{
				Gizmos.DrawWireCube(new Vector3(worldBottomLeft.x + (_cellSize * x), worldBottomLeft.y + (_cellSize * y), 1), new Vector3(_cellSize, _cellSize, 1));
			}
		}

		if(_grid != null) 
		{
			foreach(Node n in _grid)
			{
				Gizmos.color = n.IsWalkable ? Color.green : Color.red;

				Gizmos.DrawWireSphere(n.GetWorldPosition(), _cellSize/2 * 0.9f); 
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableNode : MonoBehaviour 
{
	private Node _node;
	private BoxCollider2D _boxCollider;

	public void CreateNode(Node node)
	{
		_node = node;

		_boxCollider = gameObject.AddComponent<BoxCollider2D>();
		_boxCollider.size = new Vector2(_node.GetCellSize(), _node.GetCellSize());
	}

	public Node GetNode()
	{
		return _node;
	}
}

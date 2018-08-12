using UnityEngine;

public class Node
{
    public int x;
    public int y;
    private bool _isWalkable;
    private Vector2 _worldPosition;
    private float _cellSize;

    public Node(int x, int y, Vector2 worldPosition, float cellSize, bool isWakable)
    {
        this.x = x;
        this.x = y;
        _isWalkable = isWakable;
        _cellSize = cellSize;
        _worldPosition = worldPosition;
    }

    public bool IsWalkable
    {
        get 
        {
            return _isWalkable;
        }
        set
        {
            _isWalkable = value;
        }
    }

    public Vector2 GetWorldPosition()
    {
        return _worldPosition;
    }

    public float GetCellSize()
    {
        return _cellSize;
    }
}
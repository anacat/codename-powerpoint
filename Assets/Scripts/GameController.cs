using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public List<GameObject> gridList;	
	public int maxNumberOfSteps;
	public Text counter;
	private int _stepCount;
	private int _currentGrid = 0;

	private void Awake()
	{
		_stepCount = 0;
		counter.text = "[" + Mathf.Max(0,(maxNumberOfSteps - _stepCount)) + "]";
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space) && gridList.Count > 1)
		{
			ChangeGrid();
			AddToStepCount();
		}
	}

	private void ChangeGrid()
	{
		if(_currentGrid + 1 > gridList.Count - 1)
		{
			_currentGrid = 0;
		}
		else 
		{
			_currentGrid++;
		}
		
		gridList.ForEach(g => g.SetActive(false));

		gridList[_currentGrid].SetActive(true);
	}

	public bool ReachedStepLimit()
	{
		return _stepCount > maxNumberOfSteps;
	}

	public void AddToStepCount()
	{
		_stepCount++;
		counter.text = "[" + Mathf.Max(0,(maxNumberOfSteps - _stepCount)) + "]";

		if(ReachedStepLimit())
		{
			PlayerMovement.LoadPreviousLevel();
		}
	}

	public int GetStepCount()
	{
		return _stepCount;
	}
}

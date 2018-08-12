using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
	public GameController gameController;
	public float step = 1f;
	public AudioController audioController;
	public LayerMask layerMask;
	public LoadScene loadScene;

	private Vector3 _newPosition;
	private bool _canMove = true;
	private AudioSource _audioSource;
	private hourglass _hourglass;

	public static PlayerMovement instance;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
		_hourglass = GetComponent<hourglass>();

		instance = this;
	}

	private void Start()
	{
		_newPosition = transform.position;
	}

	private void Update()
	{
		if(_canMove && !gameController.ReachedStepLimit())
		{
			Move();
			_hourglass.fill = gameController.GetStepCount() * 0.99f / gameController.maxNumberOfSteps;
		}
	}

	private void Move()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow) && CanMoveToDirection(Vector3.right)) 
		{
        	_newPosition += Vector3.right * step;
			StartCoroutine(MoveFancy());
     	}
     	else if (Input.GetKeyDown(KeyCode.LeftArrow) && CanMoveToDirection(Vector3.left)) 
		{
        	_newPosition += Vector3.left * step;
			StartCoroutine(MoveFancy());	
     	}
     	else if (Input.GetKeyDown(KeyCode.UpArrow) && CanMoveToDirection(Vector3.up)) 
		{
        	_newPosition += Vector3.up * step;
			StartCoroutine(MoveFancy());	
     	}
     	else if (Input.GetKeyDown(KeyCode.DownArrow) && CanMoveToDirection(Vector3.down)) 
		{
        	_newPosition += Vector3.down * step;
			StartCoroutine(MoveFancy());
     	}
	}

	private bool CanMoveToDirection(Vector3 direction)
	{
		Vector2 origin = transform.position;
		RaycastHit2D hit = Physics2D.Raycast(origin, direction, step, layerMask);

		return hit.collider != null? false : true;
	}

	private IEnumerator MoveFancy()
	{
		_canMove = false;
		gameController.AddToStepCount();

		_audioSource.Play();

		while(Vector2.Distance(transform.position, _newPosition) > 0.05f)
		{
			transform.position = Vector2.Lerp(transform.position, _newPosition, Time.deltaTime * 10);

			yield return null;
		}

		transform.position = _newPosition;

		if(gameController.ReachedStepLimit())
		{
			LoadPreviousLevel();
		}
		else 
		{
			CheckIfReachedGoal();			
		}
	}

	public static void LoadPreviousLevel()
	{
		instance.audioController.PlayLevelDown();
		instance.loadScene.StartPreviousTransition();
	}

	private void CheckIfReachedGoal()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);

		if(hit.collider != null )
		{
			if(hit.collider.CompareTag("Goal"))
			{
				audioController.PlayLevelUp();
				loadScene.StartTransition();
			}
			else if(hit.collider.CompareTag("BadGoal"))
			{
				audioController.PlayLevelUp();
				loadScene.StartBadTransition(1);
			}
			else if(hit.collider.CompareTag("BadGoal1"))
			{
				audioController.PlayLevelUp();
				loadScene.StartBadTransition(2);
			}
			else if(hit.collider.CompareTag("BadGoal2"))
			{
				audioController.PlayLevelUp();
				loadScene.StartBadTransition(3);
			}
		}
		else 
		{		
			_canMove = true;
		}
	}
}

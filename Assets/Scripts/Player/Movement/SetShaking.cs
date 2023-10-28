using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(CameraShaker))]
public class SetShaking : MonoBehaviour
{
	[SerializeField] private ShakeSettings _crouch;
	[SerializeField] private ShakeSettings _walk;
	[SerializeField] private ShakeSettings _run;
	[SerializeField] private ShakeSettings _fall;
	[SerializeField] private ShakeSettings _land;

	private PlayerMovement _playerMovement;
	private PlayerControll _playerControll;
	private CameraShaker _cameraShaker;

	private void Awake()
	{
		_playerMovement = GetComponent<PlayerMovement>();
		_playerControll = GetComponent<PlayerControll>();
		_cameraShaker = GetComponent<CameraShaker>();
	}

	private void OnEnable()
	{
		_playerMovement.OnMove += OnMove;
		_playerMovement.OnGetGrounded += OnLand;
		_playerMovement.OnGetOfTheGround += OnFall;
	}

	private void OnDisable()
	{
		_playerMovement.OnMove -= OnMove;
		_playerMovement.OnGetGrounded -= OnLand;
		_playerMovement.OnGetOfTheGround -= OnFall;
	}

	private void OnMove()
	{
		if (_playerControll.LastMove == PlayerControll.HorizontalMoveType.Crouch)
			_cameraShaker.SetShakePriority(_crouch);
		else if(_playerControll.LastMove == PlayerControll.HorizontalMoveType.Walk)        
			_cameraShaker.SetShakePriority(_walk);
		else if (_playerControll.LastMove == PlayerControll.HorizontalMoveType.Run)
			_cameraShaker.SetShakePriority(_run);
	}

	private void OnFall()
	{
		_cameraShaker.SetShakePriority(_fall);
	}

	private void OnLand()
	{
		_cameraShaker.SetShakePriority(_land);
	}
}

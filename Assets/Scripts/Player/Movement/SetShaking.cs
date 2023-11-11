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
	[SerializeField, Min(1)] private float _landVelocityDevider;
	[SerializeField, Min(0)] private float _maxLandAplitude;

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

	private void OnLand(float velocity)
	{
		var land = Instantiate(_land);
		land.Amplitude *= Mathf.Abs(velocity / _landVelocityDevider);
		land.Amplitude = Mathf.Min(land.Amplitude, _maxLandAplitude);
		_cameraShaker.SetShakePriority(land);
	}
}

using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCrouching))]
[RequireComponent(typeof(Stamina))]
public class PlayerControll : MonoBehaviour
{
	public HorizontalMoveType LastMove { get; private set; }

	[SerializeField, Min(0)] private float _speedInAir;
    [SerializeField, Min(0)] private float _crouchSpeed;
    [SerializeField, Min(0)] private float _moveSpeed;
    [SerializeField, Min(0)] private float _runSpeed;
    [SerializeField, Min(0)] private float _runStaminaUsagePerSecond;

    [Space(5)]
    [SerializeField, Min(1)] private int _jumpCount = 1;
    [SerializeField, Min(0)] private float _jumpHeight;

    private PlayerMovement _playerMovement;
    private PlayerCrouching _playerCrouching;
	private Stamina _stamina;

	private int _leftJumps;
	private float _jumpForce;

    private void Awake()
    {
		_playerMovement = GetComponent<PlayerMovement>();
		_playerCrouching = GetComponent<PlayerCrouching>();
		_stamina = GetComponent<Stamina>();
	}

	private void Start()
	{
		ResetJumps();
		_jumpForce = Mathf.Sqrt(2 * Physics.gravity.magnitude * _jumpHeight);
	}

    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space) && _leftJumps --> 0)
			_playerMovement.Jump(_jumpForce);

		Vector3 hMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
		hMove = transform.TransformDirection(hMove);

		if (!_playerMovement.IsGrounded)
		{
			hMove *= _speedInAir;
		}
		else if (_playerCrouching.IsCrouched)
		{
			hMove *= _crouchSpeed;
			LastMove = HorizontalMoveType.Crouch;
		}
		else if (Input.GetKey(KeyCode.LeftShift))
		{
			float staminaUsage = _runStaminaUsagePerSecond * Time.deltaTime;
			if (_stamina.UseStamina(staminaUsage) > 0)
			{
				hMove *= _runSpeed;
				LastMove = HorizontalMoveType.Run;
			}
			else
			{
				hMove *= _moveSpeed;
				LastMove = HorizontalMoveType.Walk;
			}
		}
		else
		{
			hMove *= _moveSpeed;
			LastMove = HorizontalMoveType.Walk;
		}

		if (hMove != Vector3.zero)
			_playerMovement.HorizontalMove(hMove);
		else
			LastMove = HorizontalMoveType.None;
	}

	private void ResetJumps(float _ = default)
	{
		_leftJumps = _jumpCount;
	}

	private void OnEnable()
	{
		_playerMovement.OnGetGrounded += ResetJumps;
	}

	private void OnDisable()
	{
		_playerMovement.OnGetGrounded -= ResetJumps;
	}

#if UNITY_EDITOR

	private void OnValidate()
	{		
		if (_crouchSpeed > _moveSpeed)
			_crouchSpeed = _moveSpeed;

		if (_moveSpeed > _runSpeed)
			_moveSpeed = _runSpeed;
	}

#endif

	public enum HorizontalMoveType
	{
		None,
		Crouch,
		Walk,
		Run
	}
}

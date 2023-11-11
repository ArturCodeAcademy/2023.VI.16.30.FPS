using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    public event Action<float> OnGetGrounded;
    public event Action OnGetOfTheGround;
    public event Action OnMove;
    public event Action OnStop;

    [SerializeField] private float _gravityScale = 1;
    [SerializeField, Range(0, 1)] private float _minSpeed = 0.01f;
    [SerializeField, Range(0, 2)] private float _groundedTimer = 0.2f;
    [SerializeField, Range(0, 1)] private float _nonGroundedMoveVectorMultiplier = 0.95f;
    [SerializeField, Range(0, 1)] private float _groundedMoveVectorMultiplier = 0.35f;

    private CharacterController _characterController;
    private Vector3 _moveVector;
    private float _groundedTimerCounter = 0f;
    private float _verticalVelocity = 0f;

	private void Awake()
	{
		_characterController = GetComponent<CharacterController>();
        IsGrounded = true;
	}

	private void LateUpdate()
	{
        _verticalVelocity -= Physics.gravity.magnitude * _gravityScale * Time.deltaTime;
		Vector3 moveVector = (_moveVector + _verticalVelocity * Vector3.up) * Time.deltaTime;

        Vector3 originalMoveVector = _moveVector;
        _moveVector *= IsGrounded ? _groundedMoveVectorMultiplier : _nonGroundedMoveVectorMultiplier * (1 - Time.deltaTime);
        if (moveVector.magnitude < _minSpeed && originalMoveVector != Vector3.zero)
        {
            OnStop?.Invoke();
            _moveVector = Vector3.zero;
        }
        else if (moveVector.magnitude >= _minSpeed)
            OnMove?.Invoke();

        var collisionFlags = _characterController.Move(moveVector);
        if (collisionFlags == CollisionFlags.Below)
        {
			if (!IsGrounded)
				OnGetGrounded?.Invoke(_verticalVelocity);
			IsGrounded = true;
			_groundedTimerCounter = _groundedTimer;
            _verticalVelocity = 0f;
		}
		else if (collisionFlags == CollisionFlags.Above)
        {
			_verticalVelocity = 0f;
		}
        else if (IsGrounded)
        {
			if (_groundedTimerCounter > 0)
				_groundedTimerCounter -= Time.deltaTime;
			else
            {
				IsGrounded = false;
                OnGetOfTheGround?.Invoke();
            }
		}
	}

    public void HorizontalMove(Vector3 moveVector)
    {
		_moveVector = moveVector;
        _moveVector.y = 0;
	}

    public void Jump(float moveVector)
    {
		if (moveVector <= 0)
			return;

		IsGrounded = false;
		OnGetOfTheGround?.Invoke();
		_verticalVelocity = moveVector;
	}
}

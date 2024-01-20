using UnityEngine;

public class Move : MonoBehaviour
{
    public int StateCount => _states.Length;

    [SerializeField] private Vector3[] _states;
	[SerializeField, Min(0.01f)] private float _movementDuration;

	private float _rotationTimer;
	private int _currentState = 0;
	private Vector3 _startPosition;
	private Vector3 _targetPosition;

	private void Start()
	{
		_startPosition = transform.localPosition;
		_targetPosition = _states[_currentState];
	}

	public void NextState()
	{
		_rotationTimer = 0;
		_startPosition = transform.localPosition;
		_currentState = (_currentState + 1) % _states.Length;
		_targetPosition = _states[_currentState];
	}

	public void PreviousState()
	{
		_rotationTimer = 0;
		_startPosition = transform.localPosition;
		_currentState = (_states.Length + _currentState - 1) % _states.Length;
		_targetPosition = _states[_currentState];
	}

	public void SetState(int state)
	{
		if (state < 0 || state >= _states.Length)
		{
			Debug.LogError($"Invalid state {state} for {gameObject.name}");
			return;
		}

		_rotationTimer = 0;
		_startPosition = transform.localPosition;
		_currentState = state;
		_targetPosition = _states[_currentState];
	}

	private void Update()
	{
		if (_rotationTimer < _movementDuration)
		{
			_rotationTimer += Time.deltaTime;
			float t = Mathf.Clamp01(_rotationTimer / _movementDuration);
			transform.localPosition = Vector3.Lerp(_startPosition, _targetPosition, t);
		}
	}

#if UNITY_EDITOR

	[ContextMenu(nameof(SetCurrentPositionForLastElement))]
	private void SetCurrentPositionForLastElement()
	{
		if (_states.Length == 0)
			return;

		_states[^1] = transform.localPosition;
	}

#endif
}

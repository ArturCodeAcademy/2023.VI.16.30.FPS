using UnityEngine;

public class Rotate : MonoBehaviour
{
    public int StateCount => _states.Length;

    [SerializeField] private Quaternion[] _states;
    [SerializeField, Min(0.01f)] private float _rotationDuration;

    private float _rotationTimer;
    private int _currentState = 0;
    private Quaternion _startRotation;
    private Quaternion _targetRotation;

    private void Start()
    {
        _startRotation = transform.localRotation;
		_targetRotation = _states[_currentState];
        NormalizeQuaternions();
    }

    public void NextState()
    {
        _rotationTimer = 0;
		_startRotation = transform.localRotation;
        _currentState = (_currentState + 1) % _states.Length;
        _targetRotation = _states[_currentState];
        NormalizeQuaternions();
	}

	public void PreviousState()
    {
        _rotationTimer = 0;
        _startRotation = transform.localRotation;
        _currentState = (_states.Length + _currentState - 1) % _states.Length;
        _targetRotation = _states[_currentState];
        NormalizeQuaternions();
	}

    public void SetState(int state)
    {
        if (state < 0 || state >= _states.Length)
        {
			Debug.LogError($"Invalid state {state} for {gameObject.name}");
			return;
		}

        _rotationTimer = 0;
		_startRotation = transform.localRotation;
		_currentState = state;
        _targetRotation = _states[_currentState];
        NormalizeQuaternions();
	}

    private void NormalizeQuaternions()
    {
        _startRotation.Normalize();
        _targetRotation.Normalize();
    }

	private void Update()
    {
		if (_rotationTimer < _rotationDuration)
        {
			_rotationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(_rotationTimer / _rotationDuration);
			transform.localRotation = Quaternion.Lerp(_startRotation, _targetRotation, t);
		}
	}

#if UNITY_EDITOR

	[ContextMenu(nameof(SetCurrentRotationForLastElement))]
	private void SetCurrentRotationForLastElement()
	{
		if (_states.Length == 0)
			return;

		_states[^1] = transform.localRotation;
	}

#endif
}

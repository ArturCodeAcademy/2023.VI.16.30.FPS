using UnityEngine;
using UnityEngine.Events;

public class PlayerRange : MonoBehaviour
{
	public UnityEvent OnPlayerEnter;
	public UnityEvent OnPlayerExit;
		
    [SerializeField, Min(0)] private float _range;

	private float _sqrRange;
	private Transform _player;
	private bool _playerInRange;

	private void Awake()
	{
		OnPlayerEnter ??= new UnityEvent();
		OnPlayerExit ??= new UnityEvent();
	}

	private void Start()
	{
		_sqrRange = _range * _range;
		_player = Player.Instance.transform;
		_playerInRange = false;
	}

	private void FixedUpdate()
	{
		Vector3 toPlayer = _player.position - transform.position;
		if (_playerInRange && toPlayer.sqrMagnitude > _sqrRange)
		{
			_playerInRange = false;
			OnPlayerExit?.Invoke();
		}
		else if (!_playerInRange && toPlayer.sqrMagnitude <= _sqrRange)
		{
			_playerInRange = true;
			OnPlayerEnter?.Invoke();
		}
	}

#if UNITY_EDITOR

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, _range);
	}

#endif
}

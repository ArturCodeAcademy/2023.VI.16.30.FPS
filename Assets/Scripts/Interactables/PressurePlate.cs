using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerTriggerArea))]
public class PressurePlate : MonoBehaviour
{
	public UnityEvent Pressed;
	public UnityEvent Released;

	[SerializeField, Min(0.2f)] private float _pressTime = 1f;

    private PlayerTriggerArea _playerTriggerArea;
	private bool _pressed = false;
	private float _pressedTime = 0f;

    private void Awake()
    {
		_playerTriggerArea = GetComponent<PlayerTriggerArea>();
	}

	private void Update()
	{
		if (!(_playerTriggerArea.IsPlayerInArea ^ _pressed))
		{
			_pressedTime = _pressed? _pressTime : 0f;
			return;
		}

        if (_pressed)
        {
			_pressedTime -= Time.deltaTime;

			if (_pressedTime <= 0f)
			{
				Released.Invoke();
				_pressed = false;
				_pressedTime = 0f;
			}
        }
		else
		{
			_pressedTime += Time.deltaTime;

			if (_pressedTime >= _pressTime)
			{
				Pressed.Invoke();
				_pressed = true;
				_pressedTime = _pressTime;
			}
		}
    }
}

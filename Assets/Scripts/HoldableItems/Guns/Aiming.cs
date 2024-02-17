using UnityEngine;

public class Aiming : MonoBehaviour
{
	public float Spread { get; private set; }

    [SerializeField] private Vector3 _aimingPosition;
	[SerializeField] private Quaternion _aimingRotation;

	[SerializeField, Range(0, 30)] private float _standartSpread;
	[SerializeField, Range(0, 30)] private float _aimingSpread;

	[SerializeField, Min(0)] private float _aimingSpeed;
	[SerializeField] private AnimationCurve _aimingCurve;

	private HoldableItem _holdableItem;
	private float _state;

	private void Awake()
	{
		_holdableItem = GetComponent<HoldableItem>();
	}

	private void OnEnable()
	{
		transform.localPosition = _holdableItem.HoldPosition;
		transform.localRotation = _holdableItem.HoldRotation;
	}

	private void Update()
	{
		if (Input.GetMouseButton(1) && _state == 1
			|| !Input.GetMouseButton(1) && _state == 0)
			return;

		if (Input.GetMouseButton(1) && _state < 1)
			_state += Time.deltaTime * _aimingSpeed;
		else if (!Input.GetMouseButton(1) && _state > 0)
			_state -= Time.deltaTime * _aimingSpeed;
			
		_state = Mathf.Clamp01(_state);

		transform.localPosition = Vector3.Lerp( _holdableItem.HoldPosition,
												_aimingPosition,
												_aimingCurve.Evaluate(_state));
		transform.localRotation = Quaternion.Lerp(_holdableItem.HoldRotation,
												  _aimingRotation,
												  _aimingCurve.Evaluate(_state));
		Spread = Mathf.Lerp(_standartSpread,
							_aimingSpread,
							_aimingCurve.Evaluate(_state));
	}
}

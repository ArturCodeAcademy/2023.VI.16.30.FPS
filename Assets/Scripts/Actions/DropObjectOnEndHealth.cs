using UnityEngine;

public class DropObjectOnEndHealth : MonoBehaviour
{
    [SerializeField] private Rigidbody _objectToDropPrefab;
    [SerializeField, Min(0)] private float _impulse;
	[SerializeField] protected Vector3 _offset;

    [SerializeField] private Health _health;

	private void OnEnable()
	{
		_health.OnValueMin += DropObject;
	}

	private void OnDisable()
	{
		_health.OnValueMin -= DropObject;
	}

	private void DropObject()
	{
		Rigidbody droppedObject = Instantiate(_objectToDropPrefab, transform.position + _offset, transform.rotation);
		Vector3 direction = Random.insideUnitSphere;
		direction.y = Mathf.Abs(direction.y);
		droppedObject.AddForce(direction * _impulse, ForceMode.Impulse);
	}
}

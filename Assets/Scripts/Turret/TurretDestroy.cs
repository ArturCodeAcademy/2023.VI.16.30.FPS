using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurretDestroy : MonoBehaviour
{
	[SerializeField] private Health _health;

	[Space(5)]
	[SerializeField] private GameObject _explosionPrefab;
	[SerializeField] private List<GameObject> _detach;
	[SerializeField] private List<GameObject> _destroy;
	[SerializeField, Min(0)] private float _destroyDelay = 5f;
	[SerializeField, Min(0)] private float _explosionForce = 10f;
	[SerializeField, Min(0)] private float _explosionRadius = 0.5f;

	private void OnEnable()
	{
		_health.OnValueMin += OnHealthEnd;
	}

	private void OnDisable()
	{
		_health.OnValueMin -= OnHealthEnd;
	}

	private void OnHealthEnd()
	{
		foreach (var item in _detach)
		{
			item.transform.parent = null;
			if (item.TryGetComponent(out MeshCollider collider))
				collider.convex = true;
			if (!item.TryGetComponent(out Rigidbody rb))
				rb = item.AddComponent<Rigidbody>();

			rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
			Destroy(item, _destroyDelay);
		}

		if (_explosionPrefab is not null)
			Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

		Destroy(gameObject);
	}

#if UNITY_EDITOR

	[ContextMenu(nameof(TryGetComponents))]
	private void TryGetComponents()
	{
		_health ??= GetComponent<Health>();

		_detach = GetComponentsInChildren<Collider>().Select(c => c.gameObject).ToList();
		_destroy = GetComponentsInChildren<Transform>().Where(t => !_detach.Contains(t.gameObject))
			.Select(t => t.gameObject).ToList();
	}

#endif
}

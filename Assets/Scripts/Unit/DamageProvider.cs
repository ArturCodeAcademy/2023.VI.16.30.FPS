using UnityEngine;

public class DamageProvider : MonoBehaviour, IHittable
{
	[SerializeField, Min(0)] private float _damageMultiplier = 1;
	[SerializeField] private Health _health;

	public float Hit(float damage)
	{
		return _health.Hit(damage * _damageMultiplier);
	}
}

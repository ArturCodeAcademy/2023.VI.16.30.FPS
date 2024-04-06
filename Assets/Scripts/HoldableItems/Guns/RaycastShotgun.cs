using UnityEngine;

public class RaycastShotgun : RaycastShootingBase
{
	[field: SerializeField, Min(0)] private int _pelletsCount;

	protected override void Shoot()
	{
		_isCooldown = true;
		Invoke(nameof(Cooldown), _cooldownTime);
		MagazineAmmoCount--;

		float damage = _damage / _pelletsCount;

		Instantiate(_muzzleFlash, _muzzle.position, _muzzle.rotation).Play();

		for (int i = 0; i < _pelletsCount; i++)
		{
			Vector3 direction = GetSpreadDirrection(_cam.forward, _aiming.Spread);

			if (Physics.Raycast(_cam.position, direction, out RaycastHit hit, _range, ~Player.Instance.PlayerMask, QueryTriggerInteraction.Ignore))
			{
				if (hit.collider.TryGetComponent(out IHittable hittable))
					hittable.Hit(_damage);

				var effect = Instantiate(_hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
				effect.transform.parent = hit.collider.transform;
			}

			
		}

		InvokeOnShoot();
	}
}

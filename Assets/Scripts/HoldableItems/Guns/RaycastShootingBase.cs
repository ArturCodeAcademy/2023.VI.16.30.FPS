using System;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

public class RaycastShootingBase : MonoBehaviour
{
	public event Action OnStartReload;
	public event Action OnEndReload;
	public event Action OnShoot;

	public int MagazineAmmoCount { get; private set; }
	public float ReloadProgress { get; private set; }

	[field: Header("Params")]
	[field: SerializeField] public AmmoType AmmoType { get; private set; }
	[SerializeField] private ShootType _shootType;
	[SerializeField] private float _damage;
	[SerializeField] private float _range;
	[SerializeField, Min(0)] private float _cooldownTime;
	[SerializeField] private float _reloadTime;
	[SerializeField, Min(1)] private int _magazineCapacity;

	[Header("References")]
	[SerializeField] private Transform _muzzle;
    [SerializeField] private Aiming _aiming;
	[SerializeField] private ParticleSystem _muzzleFlash;
	[SerializeField] private ParticleSystem _hitEffect;

	private bool _isCooldown = false;
	private bool _isReloading = false;

	private Coroutine _reloadCoroutine;
	private Backpack _backpack;
	private Transform _cam;

	private void Awake()
	{
		enabled = false;
	}

	private void Start()
	{
		_cam = Camera.main.transform;
		_backpack = Player.Instance.Backpack;
	}

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_reloadCoroutine != null)
				StopCoroutine(_reloadCoroutine);
			_reloadCoroutine = StartCoroutine(Reload());
			_isReloading = true;

			OnStartReload?.Invoke();
        }

		if (Input.GetKeyUp(KeyCode.R))
		{
			if (_reloadCoroutine != null)
				StopCoroutine(_reloadCoroutine);
			ReloadProgress = 0;
			_isReloading = false;

			OnEndReload?.Invoke();
		}

		if (_isReloading)
			return;

		if (_isCooldown)
			return;

		if (MagazineAmmoCount <= 0)
			return;

        if (_shootType == ShootType.Single && Input.GetMouseButtonDown(0))
		{
			Shoot();
		}

		if (_shootType == ShootType.Auto && Input.GetMouseButton(0))
		{
			Shoot();
		}
	}

	protected virtual void Shoot()
	{
		_isCooldown = true;
		Invoke(nameof(Cooldown), _cooldownTime);
		MagazineAmmoCount--;

		Instantiate(_muzzleFlash, _muzzle.position, _muzzle.rotation);

		Vector3 direction = GetSpreadDirrection(_cam.forward, _aiming.Spread);
		if (Physics.Raycast(_cam.position, direction, out RaycastHit hit, _range, ~Player.Instance.PlayerMask, QueryTriggerInteraction.Ignore))
		{
			if (hit.collider.TryGetComponent(out IHittable hittable))
				hittable.Hit(_damage);

			var effect = Instantiate(_hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
			effect.transform.parent = hit.collider.transform;
		}

		OnShoot?.Invoke();
	}

	private void Cooldown()
	{
		_isCooldown = false;
	}

	protected Vector3 GetSpreadDirrection(Vector3 direction, float spread)
	{
		Quaternion rotation = Quaternion.Euler(Random.Range(-spread, spread), Random.Range(-spread, spread), 0);
		return rotation * direction;
	}

	private IEnumerator Reload()
	{
		if (_backpack.Ammo.GetAmmunition(AmmoType) == 0 || MagazineAmmoCount >= _magazineCapacity)
		{
			_reloadCoroutine = null;
			OnEndReload?.Invoke();
			yield break;
		}

		WaitForEndOfFrame wait = new();

		for (float t = 0; t < _reloadTime; t += Time.deltaTime)
		{
			ReloadProgress = Mathf.Clamp01(t / _reloadTime);
			yield return wait;
		}

		MagazineAmmoCount += _backpack.Ammo.TakeAmmunition(AmmoType, _magazineCapacity - MagazineAmmoCount);
		ReloadProgress = 0;
	}

	public enum ShootType
	{
		Single,
		Auto
	}
}

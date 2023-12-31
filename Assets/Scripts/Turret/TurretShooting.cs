using UnityEngine;
using UnityEngine.Events;

public class TurretShooting : MonoBehaviour
{
    public UnityEvent OnStartShooting;
    public UnityEvent OnStopShooting;

	public bool Enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;
            _pause = 0;
            if (_enabled)
            {
                _leftShots = _shotCount;
            }
            else
            {
				_leftShots = 0;
				OnStopShooting?.Invoke();
            }
        }
    }

	[SerializeField] private bool _enabled = true;

	[SerializeField] private Transform _muzzle;
    [SerializeField] private Laser _laser;
    [SerializeField] private AudioSource _audioSource;

    [Space(5)]
    [SerializeField, Min(1)] private float _fireRate;
    [SerializeField, Min(1)] private int _shotCount;
    [SerializeField, Min(0)] private float _damage;
    [SerializeField, Min(0)] private float _cooldown;
    [SerializeField, Range(0, 15)] private float _spreadAngle;

    [Space(5)]
    [SerializeField] private ParticleSystem _muzzleFlashPrefab;
    [SerializeField] private GameObject _hole;

    private float _pause;
    private int _leftShots;

	private void Awake()
	{
		OnStartShooting ??= new UnityEvent();
        OnStopShooting ??= new UnityEvent();
	}

	private void Start()
    {
		_leftShots = _shotCount;
	}

    private void Update()
    {
        if (!Enabled)
			return;

        if (_pause > 0)
        {
            _pause -= Time.deltaTime;
            if (_pause < 0 && !(_leftShots < _shotCount))
                OnStopShooting?.Invoke();
            else
                return;
        }

        if (Physics.Raycast(_muzzle.position, _muzzle.forward, out RaycastHit hit) && hit.transform == Player.Instance.transform || _leftShots < _shotCount)
        {
			OnStartShooting?.Invoke();
            if (--_leftShots <= 0)
            {
                _pause = _cooldown;
                _leftShots = _shotCount;
            }
            else
            {
                _pause = 1f / _fireRate;
            }

            Vector3 directionWithSpread = ShootingHelper.SpreadDirection(_muzzle.forward, _spreadAngle);
            if (Physics.Raycast(_muzzle.position, directionWithSpread, out RaycastHit h))
                if (h.transform.TryGetComponent(out IHittable hittable))
                    hittable.Hit(_damage);
                else
                {
                    var hole = Instantiate(_hole, h.transform, true);
                    hole.transform.position = h.point;
                    hole.transform.rotation = Quaternion.LookRotation(h.normal, Vector3.up);
				}

            if (_muzzleFlashPrefab)
				Instantiate(_muzzleFlashPrefab, _muzzle.position, _muzzle.rotation);

            _audioSource?.Play();
        }
	}
}

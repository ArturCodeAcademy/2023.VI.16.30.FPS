using UnityEngine;

public class Turret : MonoBehaviour
{
	[SerializeField] private bool _enabled = true;

    [SerializeField] private Laser _laser;
    [SerializeField] private TurretShooting _turretShooting;
    [SerializeField] private RotateToPlayer _rotateToPlayer;

	private void Start()
	{
		if (_enabled)
			TurnOn();
		else
			TurnOff();
	}

	public void TurnOn()
	{
		_turretShooting.OnStartShooting.AddListener(_laser.TurnOff);
		_turretShooting.OnStopShooting.AddListener(_laser.TurnOn);
		_turretShooting.Enabled = true;
		_rotateToPlayer.TurnOn();
		_laser.TurnOn();
	}

	public void TurnOff()
	{
		_turretShooting.OnStartShooting.RemoveListener(_laser.TurnOff);
		_turretShooting.OnStopShooting.RemoveListener(_laser.TurnOn);
		_turretShooting.Enabled = false;
		_rotateToPlayer.TurnOff();
		_laser.TurnOff();
	}

#if UNITY_EDITOR

	[ContextMenu("Get all components")]
	private void GetAllComponents()
	{
		_laser ??= GetComponentInChildren<Laser>();
		_turretShooting ??= GetComponentInChildren<TurretShooting>();
		_rotateToPlayer ??= GetComponentInChildren<RotateToPlayer>();
	}

#endif
}

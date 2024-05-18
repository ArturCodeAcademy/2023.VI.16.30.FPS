using System;
using UnityEngine;

public class AmmoRandomLoot : MonoBehaviour, IInteractable
{
	public string Name { get; private set; }
	public string Info { get; private set; }

	[SerializeField] private int _minAmount;
	[SerializeField] private int _maxAmount;

	private int _amount;
	private AmmoType _ammoType;

	private void Start()
	{
		AmmoType[] types = (AmmoType[])Enum.GetValues(typeof(AmmoType));
		_ammoType = types[UnityEngine.Random.Range(0, types.Length)];
		_amount = UnityEngine.Random.Range(_minAmount, _maxAmount + 1);

		Name = $"Ammo";
		Info = $"{_ammoType} {_amount}";
	}

	public void Interact()
	{
		Player.Instance.Backpack.Ammo.AddAmmunition(new() { Type = _ammoType, Amount = _amount });
		Destroy(gameObject);
	}
}

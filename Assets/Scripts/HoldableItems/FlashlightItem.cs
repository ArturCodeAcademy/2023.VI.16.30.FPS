using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class FlashlightItem : HoldableItem, IInteractable
{
	public bool IsOn => _isOn;
	public event Action StateChanged;
	public Color LightColor => _light.color;

	[field: SerializeField] public string Name { get; private set; }
	[field: SerializeField] public string Info { get; private set; }

	private Rigidbody _rigidbody;
	private BoxCollider _collider;
	private Light _light;
	[SerializeField] private bool _isOn;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_collider = GetComponent<BoxCollider>();
		_light = GetComponentInChildren<Light>();
		_light.enabled = _isOn;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && _rigidbody.isKinematic)
		{
			_isOn = !_isOn;
			_light.enabled = _isOn;
			StateChanged?.Invoke();
		}
	}

	public void Interact()
	{
		Player.Instance.ItemHolder.AddItem(this);
	}

	public override void OnDrop()
	{
		_collider.enabled = true;
		_rigidbody.isKinematic = false;
	}

	public override void OnHide()
	{

	}

	public override void OnPickup()
	{
		_collider.enabled = false;
		_rigidbody.isKinematic = true;

		transform.localPosition = HoldPosition;
		transform.localRotation = HoldRotation;
	}

	public override void OnShow()
	{

	}
}

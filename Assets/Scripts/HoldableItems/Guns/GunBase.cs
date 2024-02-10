using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GunBase : HoldableItem, IInteractable
{
	[field: SerializeField] public string Name { get; private set; }

	[field: SerializeField] public string Info { get; private set; }

	private Collider _collider;
	private Rigidbody _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_collider = GetComponent<Collider>();
	}

	public void Interact()
	{
		Player.Instance.ItemHolder.AddItem(this);
	}

	public override void OnDrop()
	{
		_rigidbody.isKinematic = false;
		_collider.enabled = true;
	}

	public override void OnHide()
	{
		
	}

	public override void OnPickup()
	{
		_rigidbody.isKinematic = true;
		_collider.enabled = false;

		transform.localPosition = HoldPosition;
		transform.localRotation = HoldRotation;
	}

	public override void OnShow()
	{
		
	}
}

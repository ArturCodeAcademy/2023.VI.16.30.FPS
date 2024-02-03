using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
	
	public PlayerView View { get; private set; }
	public PlayerCrouching Crouching { get; private set; }
	public PlayerMovement Movement { get; private set; }
	public Health Health { get; private set; }
	public Stamina Stamina { get; private set; }
	public ItemHolder ItemHolder { get; private set; }

	public Vector3 Center => transform.position + LocalCenter;
	public Vector3 LocalCenter => _controller.height / 2f * Vector3.up;

	private CharacterController _controller;

	private void Awake()
	{
		Instance = this;

		View = GetComponent<PlayerView>();
		Crouching = GetComponent<PlayerCrouching>();
		Movement = GetComponent<PlayerMovement>();
		Health = GetComponent<Health>();
		Stamina = GetComponent<Stamina>();
		ItemHolder = GetComponentInChildren<ItemHolder>();

		_controller = GetComponent<CharacterController>();
	}
}
